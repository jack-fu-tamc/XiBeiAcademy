using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HC.Manage.Controllers
{
    public class EnrolSysController : ManageBaseController
    {
        //
        // GET: /EnrolSys/

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

    }
}
