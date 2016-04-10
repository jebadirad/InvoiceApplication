using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace InvoiceApplication.Models
{
    /// <summary>
    /// Class that defines invoices
    /// </summary>
    public class Invoice
    {
        //track the invoice
        public string ID { get; set; }
        //the user it belongs to.
        public User User { get; set; }
        //charges, should be upgraded to be a list of chages.
        public string Charges { get;  set; }
        //the description for each charge. upgrade to list.
        public string ChargeDesc { get; set; }
        //the user to charge the invoice to.
        public User ChargeTo { get; set; }
      
        /// <summary>
        /// Sets the values for the invoice object, and validates the information.
        /// </summary>
        /// <param name="userID">the user id that the invoice belongs to.</param>
        /// <param name="Charges">the charge that is applied to the account.</param>
        /// <param name="ChargeDesc">The description for the charge.</param>
        /// <param name="ChargeID">the id of the user to charge to.</param>
        /// <returns></returns>
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

        /// <summary>
        /// gets an invoice by its id.
        /// </summary>
        /// <param name="ID">the id of the invoice.</param>
        /// <returns>an invoice object or null if the id does not find anything.</returns>
        public Invoice Get(string ID)
        {
            Invoice invoice = null;

            if (!string.IsNullOrEmpty(ID))
            {
                IList<Invoice> invoices = new List<Invoice>();
                //need to do some try catch for database read and backout.
                using (StreamReader sr = new StreamReader(HttpRuntime.AppDomainAppPath + "/Models/Invoices.json"))
                {


                    invoices = JsonConvert.DeserializeObject<List<Invoice>>(sr.ReadToEnd());
                }
                IEnumerable<Invoice> query = invoices.Where(u => u.ID.Equals(ID));
                //make sure we have values in our query.
                if (query.GetEnumerator().MoveNext())
                {
                    invoice = query.First();
                }
            }
            //up the stack we should check for null and handle the exception there.


            return invoice;
        }

    }
}