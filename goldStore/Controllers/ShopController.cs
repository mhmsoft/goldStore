using goldStore.Areas.Panel.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goldStore.Controllers
{
    public class ShopController : Controller
    {
        ProductRepository repoProduct = new ProductRepository(new Areas.Panel.Models.goldstoreEntities());
        CategoryRepository repoCategory = new CategoryRepository(new Areas.Panel.Models.goldstoreEntities());
        // GET: Shop
        public ActionResult Index()
        {
            return RedirectToAction("Products");
        }
        public PartialViewResult PartialCategory()
        {
            return PartialView(repoCategory.GetAll());
        }
        // ürünleri gösteren method
        public ActionResult Products(int?categoryId)
        {
            var result = repoProduct.GetAll();
            // eğer kategoriye göre bir filtreleme istendiğinde
            if (categoryId!=null)
            {
                result = result.Where(x => x.categoryId == categoryId).ToList();
            }

            return View(result);
        }
    }
}