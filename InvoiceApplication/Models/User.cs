using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace InvoiceApplication.Models
{
    /// <summary>
    /// Class that defines users of the site. can be bussines customer or user customer.
    /// </summary>
    public class User
    {
        //id of the user set on the client side, and reset on the server side.
        public string ID { get; set; }
        //the name of the customer.
        public string Name { get; set; }
        // the type of the customer (business, or user) needs to be upgraded to an enum.
        public string Type { get; set; }

        public User(){
            //take the 'id' from the server
            //temporary id for demo purposes (probably new guid set by sql)
            ID = DateTime.Now.ToString("fff");
        }

        /// <summary>
        /// gets a user by the id of the user.
        /// </summary>
        /// <param name="ID">the id of the user.</param>
        /// <returns>A user object or null</returns>
        public User Get(string ID)
        {
            User user = null;
            if (!string.IsNullOrEmpty(ID))
            {
                IList<User> users = new List<User>();
                //need a try catch or some backout plan.
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
            //handle null up the stack.

            return user;
        }

    }

}