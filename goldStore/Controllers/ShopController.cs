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

        public PartialViewResult PartialPrice()
        {
            return PartialView();
        }

        public PartialViewResult PartialCategory()
        {
            return PartialView(repoCategory.GetAll());
        }

        // ürünleri gösteren method
        public ActionResult Products(int?categoryId,decimal?min,decimal?max)
        {
            var result = repoProduct.GetAll();
            // eğer kategoriye göre bir filtreleme istendiğinde
            if (categoryId!=null)
            {
                result = result.Where(x => x.categoryId == categoryId).ToList();
            }
            else if (max != null && min != null)
            {
                result = result.Where(x => x.price >= min && x.price <= max).ToList();
               /* TempData["min"] = min;
                TempData.Keep("min");
                TempData["max"] = max;
                TempData.Keep("max");*/
            }

            return View(result);
        }
    }
}