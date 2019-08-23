using goldStore.Areas.Panel.Models;
using goldStore.Areas.Panel.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goldStore.Areas.Panel.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        UserRepository repoUser = new UserRepository(new Models.goldstoreEntities() );
        // GET: Panel/Users
        public ActionResult Index()
        {
            return View(repoUser.GetAll());
        }
        [HttpPost]
        public string doPassiveUser(int userId)
        {
            string message = "";
            user _user = repoUser.Get(userId);
            if (_user.isActive == true)
            {
                _user.isActive = false;
                message = "kullanıcı askıya alındı";
            }

            else
            {
                _user.isActive = true;
                message = "kullanıcı aktif edildi";
            }
                

            repoUser.Update(_user);
            return message ;
        }
    }
}