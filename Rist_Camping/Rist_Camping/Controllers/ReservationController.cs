using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rist_Camping.Controllers
{
    public class ReservationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Request()
        {
            return View();
        }
    }
}