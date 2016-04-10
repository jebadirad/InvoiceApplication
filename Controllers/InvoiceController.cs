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
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public JsonResult GetAll()
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }

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