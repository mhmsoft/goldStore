using goldStore.Areas.Panel.Models;
using goldStore.Areas.Panel.Models.Repository;
using goldStore.Areas.Panel.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goldStore.Controllers
{
    [Authorize(Roles = "user")]
    public class AccountController : Controller
    {

        UserRepository repoUser = new UserRepository(new goldstoreEntities());
        CouponRepository repoCoupon = new CouponRepository(new goldstoreEntities());
        OrderRepository repoOrder = new OrderRepository(new goldstoreEntities());
        WishListRepository repoWishList = new WishListRepository(new goldstoreEntities());
        // GET: Account

        public ActionResult Index()
        {
            return RedirectToAction("MyProfile");
        }
        public  ActionResult MyProfile()
        {
            if (User.Identity.IsAuthenticated)
            {
                user _user = repoUser.GetAll().Where(x => x.email == User.Identity.Name).FirstOrDefault();
                return View(_user);
            }
            return View();
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyProfile(user userForm)
        {
            string message = "";
            bool status = false;
            if (userForm!=null)
            {
                user _user = repoUser.GetAll().Where(x => x.email == User.Identity.Name).FirstOrDefault();
                if (userForm.firstName != null)
                    _user.firstName = userForm.firstName;
                if (userForm.lastName != null)
                    _user.lastName = userForm.lastName;
                if(userForm.password!=null && userForm.rePassword != null)
                {
                    _user.password = Crypto.Hash(userForm.password);
                    _user.rePassword = Crypto.Hash(userForm.rePassword);
                }               
                if (userForm.phone!=null)               
                    _user.phone = userForm.phone;
                if (userForm.address != null)
                    _user.address = userForm.address;
                if (userForm.city != null)
                    _user.city = userForm.city;
                if (userForm.subscribe == true)
                    _user.subscribe = true;

                status = true;
                message = "Değişiklikler kaydedildi";
                repoUser.Update(_user);              
            }
            ViewBag.message = message;
            ViewBag.status = status;
            return View();
        }

        [HttpPost]
        public void applyDiscount(string discountCode)
        {
            var accountOwner = User.Identity.Name;
            int customerId = repoUser.GetAll().Where(x => x.email == accountOwner).FirstOrDefault().userId;
            var _discount = repoUser.Get(customerId).coupons.FirstOrDefault(i => i.isActive == true && i.couponCode == discountCode && DateTime.Now < i.expired).discount;
            if (_discount != null)
            {
                coupons _indirim = new coupons()
                {
                    discount = _discount,
                    couponCode = discountCode
                };
                Session["discount"] = _indirim;
            }
        }
        public ActionResult Coupons()
        {
            var customerId = repoUser.GetAll().Where(u => u.email == User.Identity.Name).FirstOrDefault().userId;
            return View(repoCoupon.GetAll().Where(x=>x.userId==customerId));
        }
        public ActionResult Myorders()
        {
            var customerId = repoUser.GetAll().Where(u => u.email == User.Identity.Name).FirstOrDefault().userId;
            return View(repoOrder.GetAll().Where(x => x.customerId == customerId));
        }
        public ActionResult Wishlist()
        {
            var customerId = repoUser.GetAll().Where(u => u.email == User.Identity.Name).FirstOrDefault().userId;
            return View(repoWishList.GetAll().Where(x => x.userId == customerId));
        }
        [HttpPost]
        public void DeleteWishItem(int id)
        {
            var favourite = repoWishList.Get(id);
            repoWishList.Delete(favourite);
        }
    }
}