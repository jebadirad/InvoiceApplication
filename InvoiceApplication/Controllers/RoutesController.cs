using InvoiceApplication.Models;
using System.Web.Mvc;

namespace InvoiceApplication.Controllers
{
    /// <summary>
    /// Routes all page views. 
    /// </summary>
    public class RoutesController : Controller
    {
        // GET: RoutesDemo
        public ActionResult AddForm()
        {
            return View();
        }

        
        public ActionResult InvoiceList()
        {
            return View();
        }
        public ActionResult UserList()
        {
            return View();
        }

        public ActionResult AddInvoiceForm()
        {
            return View();
        }
        /// <summary>
        /// sends the invoice form back to the ui.
        /// </summary>
        /// <param name="id">the id of the invoice in question for generation</param>
        /// <returns>the invoice form with the invoice. otherwise an empty form.</returns>
        public ActionResult InvoiceForm(string id)
        {
            Invoice invoice = new Invoice();
            invoice = invoice.Get(id);
            if(invoice != null)
            {
                ViewBag.Invoice = invoice;

            }


            return View();
        }
    }
}