using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceApplication.Models
{
    public class User
    {

        public string ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public User(){
            //take the 'id' from the server
            ID = DateTime.Now.ToString("fff");
        }

        public User Get(string ID)
        {
            User user = null;
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
                   user = query.First();
                }
            }


            return user;
        }

    }

}