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
        // add user
        [HttpPost]
        public JsonResult Add(User param)
        {
            JsonResult jsonresult;
            if (!string.IsNullOrEmpty(param.Name) && !string.IsNullOrEmpty(param.Type))
            {
                IList<User> users = new List<User>();

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

        //get single
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
       
        [HttpGet]
        public JsonResult GetUserList()
        {
            JsonResult jsonresult;
            IList<User> users = new List<User>();

            using (StreamReader sr = new StreamReader(HttpRuntime.AppDomainAppPath + "/Models/userdatabase.json"))
            {
                users = JsonConvert.DeserializeObject<List<User>>(sr.ReadToEnd());
            }
            jsonresult = Json(users, JsonRequestBehavior.AllowGet);

            return jsonresult;
        }
    }
}