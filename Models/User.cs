using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    }

}