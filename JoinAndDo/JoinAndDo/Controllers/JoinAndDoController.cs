using JoinAndDo.Entities;
using JoinAndDo.Repositoryes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JoinAndDo.Controllers
{
    public class JoinAndDoController : Controller
    {
        private SqlRepository sqlRepository = new SqlRepository();
        // GET: JoinAndDo
        public ActionResult Index()
        {
            ViewBag.listJoinsEntity = sqlRepository.GetAllFromJoins();
            return View();
        }

        public ActionResult my_accession()
        {
            ViewBag.listMyAccession = sqlRepository.GetAllFromMyAccession();
            return View();
        }

        public ActionResult my_message()
        {
            return View();
        }

        public ActionResult my_profile()
        {
            return View();
        }

        public ActionResult deals_accession()
        {
            ViewBag.listDealsAccession = sqlRepository.GetAllFromDealsAccession();
            return View();
        }

        public ActionResult test()
        {
            return View();
        }

        public ActionResult layout()
        {
            return View();
        }
    }
}