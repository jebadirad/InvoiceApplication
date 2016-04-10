using InvoiceApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceApplication.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// Api endpoint to add a new user
        /// </summary>
        /// <param name="param">a user object with id, name, and type filled out
        /// Generates ID on server side but should have an id when coming in from client side for 
        /// client side tracking. Similar to reactjs thinking of auto populating the user 
        /// as it gets created for use until the server catches up.</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add(User param)
        {
            JsonResult jsonresult;
            if (!string.IsNullOrEmpty(param.Name) && !string.IsNullOrEmpty(param.Type))
            {
                IList<User> users = new List<User>();
                //needs some try catch or a way to handle exceptions and backout.
                using (StreamReader sr = new StreamReader(HttpRuntime.AppDomainAppPath + "/Models/userdatabase.json"))
                {


                    users = JsonConvert.DeserializeObject<List<User>>(sr.ReadToEnd());
                }
                param.ID = (users.Count() + 1).ToString();
                users.Add(param);
                string json = JsonConvert.SerializeObject(users.ToArray());
                using (StreamWriter sw = new StreamWriter(HttpRuntime.AppDomainAppPath + "/Models/userdatabase.json"))
                {
                    sw.WriteLine(json);
                }
                //push to json file
                jsonresult = Json(param, JsonRequestBehavior.AllowGet);
            }
            else
            {
                jsonresult = Json("", JsonRequestBehavior.DenyGet);
            }

            return jsonresult;
        }

        /// <summary>
        /// API endpoint to get a single user back.
        /// </summary>
        /// <param name="ID">The id of the user</param>
        /// <returns>a user object or deny the request</returns>
        [HttpGet]
        public JsonResult Get(string ID)
        {
            JsonResult jsonresult;
            jsonresult = Json("", JsonRequestBehavior.DenyGet);
            if (!string.IsNullOrEmpty(ID))
            {
                IList<User> users = new List<User>();

                using (StreamReader sr = new StreamReader(HttpRuntime.AppDomainAppPath + "/Models/userdatabase.json"))
                {


                    users = JsonConvert.DeserializeObject<List<User>>(sr.ReadToEnd());
                }
                IEnumerable<User> query = users.Where(u => u.ID.Equals(ID));
                if (query.GetEnumerator().MoveNext())
                {
                    jsonresult = Json(query.First(), JsonRequestBehavior.AllowGet);
                }
            }
            return jsonresult;
        }
       
        /// <summary>
        /// API endpoint that gets all users 
        /// </summary>
        /// <returns>a list of all users.</returns>
        [HttpGet]
        public JsonResult GetUserList()
        {
            JsonResult jsonresult;
            IList<User> users = new List<User>();
            //needs some handling here.
            using (StreamReader sr = new StreamReader(HttpRuntime.AppDomainAppPath + "/Models/userdatabase.json"))
            {
                users = JsonConvert.DeserializeObject<List<User>>(sr.ReadToEnd());
            }
            jsonresult = Json(users, JsonRequestBehavior.AllowGet);

            return jsonresult;
        }
    }
}