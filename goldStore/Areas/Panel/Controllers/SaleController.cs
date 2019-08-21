using goldStore.Areas.Panel.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goldStore.Areas.Panel.Controllers
{
    public class SaleController : Controller
    {
        OrderRepository repoOrder = new OrderRepository(new Models.goldstoreEntities());
        // GET: Panel/Sale
        public ActionResult Index()
        {
            return View(repoOrder.GetAll());
        }
        public ActionResult saleStatistics()
        {
            return View(repoOrder.GetAll());
        }
    }
}