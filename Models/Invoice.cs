using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace InvoiceApplication.Models
{
    public class Invoice
    {
        public string ID { get; set; }
        public User User { get; set; }
        public string Charges { get;  set; }
        public string ChargeDesc { get; set; }
        public User ChargeTo { get; set; }
      

        public bool SetInvoice(string userID, string Charges, string ChargeDesc, string ChargeID)
        {
            bool success = false;
            if(!string.IsNullOrEmpty(userID) 
                && !string.IsNullOrEmpty(ChargeID)
                && !string.IsNullOrEmpty(Charges)
                && !string.IsNullOrEmpty(ChargeDesc))
            {
                ChargeTo = new User().Get(ChargeID);
                User = new User().Get(userID);
                this.Charges = Charges;
                this.ChargeDesc = ChargeDesc;
                if(User != null && ChargeTo != null)
                {
                    success = true;
                }
            }
           
            return success;
        }

        public Invoice Get(string ID)
        {
            Invoice invoice = null;

            if (!string.IsNullOrEmpty(ID))
            {
                IList<Invoice> invoices = new List<Invoice>();
                using (StreamReader sr = new StreamReader(HttpRuntime.AppDomainAppPath + "/Models/Invoices.json"))
                {


                    invoices = JsonConvert.DeserializeObject<List<Invoice>>(sr.ReadToEnd());
                }
                IEnumerable<Invoice> query = invoices.Where(u => u.ID.Equals(ID));
                if (query.GetEnumerator().MoveNext())
                {
                    invoice = query.First();
                }
            }



            return invoice;
        }

    }
}