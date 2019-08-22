using goldStore.Areas.Panel.Models.Repository;
using goldStore.Models.ViewModel;
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
        OrderDetailRepository repoOrderDetail = new OrderDetailRepository(new Models.goldstoreEntities());
        CategoryRepository repoCategory = new CategoryRepository(new Models.goldstoreEntities());
        // GET: Panel/Sale
        public ActionResult Index()
        {
            return View(repoOrder.GetAll());
        }
        public ActionResult saleStatistics()
        {
            return View(repoOrderDetail.GetAll());
        }
        // satış istatistiklerinin grafiksel
        public ActionResult getGraphData()
        {
            return Json(repoOrderDetail.donutGraphValues(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult getGraphDataPaymentType()
        {
            return Json(repoOrderDetail.donutGraphPaymentTypeValues(), JsonRequestBehavior.AllowGet);
        }
    }
}