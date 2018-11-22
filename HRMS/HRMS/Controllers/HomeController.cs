using HRMS.DataBaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HRMS.Controllers
{
    public class HomeController : Controller

    {
        private HRMSContext db = new HRMSContext();

        // GET: Home
        [Authorize]
        public ActionResult Index()
        { 
            return View();
        }
    }
}