using CF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CF.Web.Controllers
{
    public class HomeController : Controller
    {
        private ICFUow uow;
        public HomeController(ICFUow uow)
        {
            this.uow = uow;

            TestMethod();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void TestMethod()
        {
            try
            {
                var user = uow.Users.GetAll().ToList();
            }
            catch(Exception ex)
            {

            }
           
        }
    }
}