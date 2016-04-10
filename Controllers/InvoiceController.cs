using InvoiceApplication.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvoiceApplication.Controllers
{
    public class InvoiceController : Controller
    {
       
        /// <summary>
        /// Fetches an invoice from an id
        /// </summary>
        /// <param name="ID">the string ID of an invoice or "All" to get all invoices</param>
        /// <returns>one invoice, or all invoices in JSON format</returns>
        public JsonResult Get(string ID)
        {
            JsonResult jsonresult;
            jsonresult = Json("", JsonRequestBehavior.DenyGet);
            if (!string.IsNullOrEmpty(ID))
            {
                IList<Invoice> invoices = new List<Invoice>();

                using (StreamReader sr = new StreamReader(HttpRuntime.AppDomainAppPath + "/Models/Invoices.json"))
                {
                    invoices = JsonConvert.DeserializeObject<List<Invoice>>(sr.ReadToEnd());
                }
                if (ID.Equals("All"))
                {
                   
                    jsonresult = Json(invoices, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    //get specific
                    IEnumerable<Invoice> query = invoices.Where(i => i.ID.Equals(ID));
                    if (query.GetEnumerator().MoveNext())
                    {
                        jsonresult = Json(query.First(), JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return jsonresult;
        }

        /// <summary>
        /// Adds an invoice to the database
        /// </summary>
        /// <param name="charges">float amount due</param>
        /// <param name="chargeDesc">a description for the charge </param>
        /// <param name="chargeID">the id of the customer to charge to</param>
        /// <returns>The invoice back to the client if it was successfully added.</returns>
        [HttpPost]
        public JsonResult Add(string charges, 
            string chargeDesc, string chargeID)
        {
            JsonResult jsonresult;
            jsonresult = Json("", JsonRequestBehavior.DenyGet);
            Invoice invoice = new Invoice();
            if(invoice.SetInvoice("1", charges, chargeDesc, chargeID))
            {
                IList<Invoice> invoices = new List<Invoice>();
                //needs some try catch or some backout method.
                using (StreamReader sr = new StreamReader(HttpRuntime.AppDomainAppPath + "/Models/Invoices.json"))
                {
                    invoices = JsonConvert.DeserializeObject<List<Invoice>>(sr.ReadToEnd());
                }
                invoice.ID = (invoices.Count + 1).ToString();
                invoices.Add(invoice);

                string json = JsonConvert.SerializeObject(invoices.ToArray());
                using (StreamWriter sw = new StreamWriter(HttpRuntime.AppDomainAppPath + "/Models/Invoices.json"))
                {
                    sw.WriteLine(json);
                }
                jsonresult = Json(invoice, JsonRequestBehavior.AllowGet);
            }


            return jsonresult;
        }
    }
}