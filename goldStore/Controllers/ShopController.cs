using goldStore.Areas.Panel.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using goldStore.Models.ViewModel;
using goldStore.Areas.Panel.Models;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace goldStore.Controllers
{
    public class ShopController : Controller
    {
        ProductRepository repoProduct           = new ProductRepository(new goldstoreEntities());
        CategoryRepository repoCategory         = new CategoryRepository(new goldstoreEntities());
        BrandRepository repoBrand               = new BrandRepository(new goldstoreEntities());
        UserRepository repoUser                 = new UserRepository(new goldstoreEntities());
        OrderRepository repoOrder               = new OrderRepository( new goldstoreEntities());
        OrderDetailRepository repoOrderDetail   = new OrderDetailRepository(new goldstoreEntities());

        // GET: Shop
        public ActionResult Index()
        {
            return RedirectToAction("Products");
        }
        public PartialViewResult PartialBrands()
        {
            // Tüm markaları partial View'e Gönder
            return PartialView(repoBrand.GetAll());
        }
        public PartialViewResult PartialNewArrivals()
        {
            return PartialView();
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
        public ActionResult Products(int?brandId,int?categoryId,decimal?min,decimal?max, int? page, int? PageSize, int? orderBy)
        {
            ViewBag.orderBy = new List<SelectListItem>() {
                new SelectListItem { Text = "Fiyat", Value = "1", Selected = true },
                new SelectListItem { Text = "İsim", Value = "2" },

            };
            ViewBag.PageSize = new List<SelectListItem>() {
                new SelectListItem { Text = "12", Value = "12", Selected = true },
                new SelectListItem { Text = "9", Value = "9" },
                new SelectListItem { Text = "6", Value = "6" },
                new SelectListItem { Text = "3", Value = "3" },
                new SelectListItem { Text = "2", Value = "2" }
            };

            int _page = page ?? 1;
            int _pageSize = PageSize ?? 6;
            TempData["PageSize"] = _pageSize;
            TempData.Keep("PageSize");

            var result = repoProduct.GetAll();
            // eğer markaya göre bir filtreleme istendiğinde
            if ( brandId!=null)
            {
                result = result.Where(x => x.brandId == brandId).ToList();
            }
            // eğer kategoriye göre bir filtreleme istendiğinde
            else if (categoryId!=null)
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
            // order by Fiyat seçilmişse
            else if (orderBy ==1)
            {
                result = result.OrderBy(x => x.price).ToList();
                TempData["orderby"] = 1;
                TempData.Keep("orderby");
            }
            // order by isim seçilmişse
            else if (orderBy ==2)
            {
                result = result.OrderBy(x => x.productName).ToList();
                TempData["orderby"] = 2;
                TempData.Keep("orderby");
            }

            return View(result.ToPagedList(_page,_pageSize));
        }
        
        // ürün detayı
        public ActionResult ProductDetail(int productId)
        {
            return View(repoProduct.Get(productId));
        }
        // product resimleri için
        public ActionResult Thumbnail(int width, int height, int Id, int _imageId)
        {
            byte[] photo = repoProduct.Get(Id).productImage.FirstOrDefault(x => x.imageId == _imageId).image;
            var base64 = Convert.ToBase64String(photo);
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);

            using (var newImage = new Bitmap(width, height))
            using (var graphics = Graphics.FromImage(newImage))
            using (var stream = new MemoryStream())
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(image, new Rectangle(0, 0, width, height));
                newImage.Save(stream, ImageFormat.Png);
                return File(stream.ToArray(), "image/png");
            }

        }

        //brand resmi için
        public ActionResult BrandThumbnail(int width, int height, int Id)
        {
            byte[] photo = repoBrand.Get(Id).image;
            var base64 = Convert.ToBase64String(photo);
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);

            using (var newImage = new Bitmap(width, height))
            using (var graphics = Graphics.FromImage(newImage))
            using (var stream = new MemoryStream())
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(image, new Rectangle(0, 0, width, height));
                newImage.Save(stream, ImageFormat.Png);
                return File(stream.ToArray(), "image/png");
            }

        }

        [NonAction]
        private int isExistInCard(int id)
        {
            List<BasketItem> card = (List<BasketItem>)Session["card"];
            for (int i = 0; i < card.Count; i++)
                if (card[i].product.productId.Equals(id))
                    return i;
            return -1;
        }       
       
        public ActionResult AddCard(int productId, int quantity)
        {           
            product _product = repoProduct.Get(productId);
            if (Session["card"] == null)
            {
                List<BasketItem> Card = new List<BasketItem>();
                Card.Add(new BasketItem()
                {
                    Id=Guid.NewGuid(),
                    product = _product,
                    quantity = quantity,                 
                    DateCreated = DateTime.Now
                });
                Session["card"] = Card;
            }
            else
            {
                List<BasketItem> card = (List<BasketItem>)Session["card"];
                // sepette eklenen ürünün  sepetteki sıra numarasına bakılır. varsa sepetteki sıra no gönderilir, yoksa -1 değeri gönderilir.
                int index = isExistInCard(productId);
                // sepette eklenen ürün varsa
                if (index != -1)
                {
                    // sadece adedini gelen quantity kadar arttıracak.
                    card[index].quantity += quantity;
                }
                // sepette girilen ürün yoksa 
                else
                {
                    // sepete ekle
                    card.Add(new BasketItem
                    {
                        product = _product,
                        quantity = quantity,
                        DateCreated = DateTime.Now
                    });
                }
                Session["card"] = card;
               
            }
            return RedirectToAction("Index");
            // return Json((List<BasketItem>)Session["card"],JsonRequestBehavior.AllowGet);
        }
        // sepetteki elemanı silme
        public  void Remove(int productId)
        {            
            List<BasketItem> card = (List<BasketItem>)Session["card"];
            if (card.Exists(x => x.product.productId == productId))
            {
                int index = isExistInCard(productId);
                card.RemoveAt(index);
                Session["card"] = card;                
            }

        }

        public ActionResult Basket()
        {
            return View((List<BasketItem>)Session["card"]);
        }

        [Authorize(Roles = "User")]
        public ActionResult Checkout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }
            user _user = repoUser.GetAll().Where(x => x.email == User.Identity.Name).FirstOrDefault();

            return View(_user);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Checkout(user _user,bool?shipbox,int? shipmethod, int?paymenttype,string ad,string soyad,string adres,string sehir,int?postakodu,string eposta,string telefon)
        {
            //shipbox-> shipbox true ise başkası adına yada basşa bir farklı adrese gönderim
            //shipprice-> Hızlıgönderim:10 tl yada normal gönderim 5 tl
            //paymenttype_>ödeme tipi 1-havale,2-kredi kartı,3- kapıda ödeme vb.
            string message = "";
            bool status=false;
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "User");
            }
            user loginUser = repoUser.GetAll().Where(x => x.email == User.Identity.Name).FirstOrDefault();
            orders newOrder = new orders();
            newOrder.customerId = loginUser.userId;
            // eğer kendine gönderiyorsa
            if (shipbox != false)
            {
                if (_user != null)
                {
                    if (paymenttype == null)
                        message = "Bir ödeme tipi seçmeniz gerekir";
                    else
                        newOrder.paymentType = paymenttype;
                    if (shipmethod == null)
                        message = "Bir gönderme tipi(Hızlı ya da Normal gönderim) seçmeniz gerekir";
                    else
                        newOrder.ShipPrice = shipmethod;

                    // faklı birine yada farklı bir adrese göndermiyorsa
                    newOrder.isOther = false;

                    if (string.IsNullOrEmpty(_user.firstName))
                    {
                        message = "isim alanını doldurmanız gerekir";
                        ViewBag.message = message;
                        return View();
                    }
                    else
                        newOrder.firstname = _user.firstName;
                    if (string.IsNullOrEmpty(_user.lastName))
                    {
                        message = "Soyad alanını doldurmanız gerekir";
                        ViewBag.message = message;
                        return View();
                    }
                    else
                        newOrder.lastname = _user.lastName;
                    if (string.IsNullOrEmpty(_user.address))
                    {
                        message = "adres alanını doldurmanız gerekir";
                        ViewBag.message = message;
                        return View();
                    }
                    else
                        newOrder.address = _user.address;
                    if (string.IsNullOrEmpty(_user.city))
                    {
                        message = "Şehir alanını doldurmanız gerekir";
                        ViewBag.message = message;
                        return View();
                    }
                    else
                        newOrder.city = _user.city;
                    if (string.IsNullOrEmpty(_user.phone))
                    {
                        message = "telefon alanını doldurmanız gerekir";
                        ViewBag.message = message;
                        return View();
                    }
                    else
                        newOrder.phone = _user.phone;

                    newOrder.postCode = _user.postCode;
                    // sipariş kaydet
                    repoOrder.Save(newOrder);
                    // sepette ürünler varsa
                    if (Session["card"] != null)
                    {
                        List<BasketItem> Basket = (List<BasketItem>)Session["card"];
                        orderDetails newOrderDetail = new orderDetails();
                        foreach (var item in Basket)
                        {
                            newOrderDetail.orderId = newOrder.orderId;
                            newOrderDetail.productId = item.product.productId;
                            newOrderDetail.quantity = item.quantity;
                            repoOrderDetail.Save(newOrderDetail);

                        }
                        SendOrderInfo(loginUser.email);
                        message = " Sipariş işlemi tamamlandı. siparişiniz ile ilgili bilgi mailinize gönderilmiştir. <br/>" +
                                  "Goldstore sayfanızda sipariş detaylarını görebilirisiniz. Detay için aşağıdaki linke tıklayınız";
                        status = true;     

                    }
                }
            }
            // farklı birine yada farklı bir adrese gönderiyorsa
            else
            {
               
                Session["ad"] = ad;
                Session["soyad"] = soyad;
                Session["adres"] = adres;
                Session["sehir"] = sehir;
              
                Session["telefon"] = telefon;
                Session["postakodu"] = postakodu ?? 0;
                Session["isGuest"] = true;

                if (string.IsNullOrEmpty(ad))
                {
                    message = "isim alanı boş bıraktınız";
                    ViewBag.message = message;
                    return View();
                }
                else
                    newOrder.firstname = ad;
                if (string.IsNullOrEmpty(soyad))
                {
                    message = "Soyisim alanı boş bıraktınız";
                    ViewBag.message = message;
                    return View();
                }
                else
                    newOrder.lastname = soyad;
                
                if (string.IsNullOrEmpty(adres))
                {
                    message = "Adres alanı boş bıraktınız";
                    ViewBag.message = message;
                    return View();
                }

                else
                    newOrder.address = adres;
                if (string.IsNullOrEmpty(sehir))
                {
                    message = "Şehir alanı boş bıraktınız";
                    ViewBag.message = message;
                    return View();
                }
                else
                    newOrder.city = sehir;
                if (postakodu != null)
                    newOrder.postCode = postakodu;
                if (!string.IsNullOrEmpty(eposta))
                    newOrder.email = eposta;
                
                if (string.IsNullOrEmpty(telefon))
                {
                    message = "Telefon alanı boş bıraktınız";
                    ViewBag.message = message;
                    return View();
                }

                else
                    newOrder.phone = telefon;
            }
            ViewBag.message = message;
            ViewBag.status = status;
            return View();
        }

            
        [NonAction]
        public void SendOrderInfo(string emailID)
        {
            SmtpSection network = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            try
            {
                var url = "/Account/MyOrders";
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);
                var fromEmail = new MailAddress(network.Network.UserName, "Goldstore Sipariş Bilgisi");
                var toEmail = new MailAddress(emailID);

                string subject = "Goldstore Sipariş Bilgisi";
                string body = "<br/><br/>Goldstore sayfanızda sipariş detaylarını görebilirisiniz. Detay için aşağıdaki linke tıklayınız" +
                      " <br/><br/><a href='" + link + "'>" + link + "</a> ";
                var smtp = new SmtpClient
                {
                    Host = network.Network.Host,
                    Port = network.Network.Port,
                    EnableSsl = network.Network.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = network.Network.DefaultCredentials,
                    Credentials = new NetworkCredential(network.Network.UserName, network.Network.Password)
                };
                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}



