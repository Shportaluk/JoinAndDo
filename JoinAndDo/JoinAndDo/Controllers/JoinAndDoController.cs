﻿using JoinAndDo.Entities;
using JoinAndDo.Repositoryes;
using System.Web;
using System.Web.Mvc;

namespace JoinAndDo.Controllers
{
    public class JoinAndDoController : Controller
    {
        private SqlRepository sqlRepository = new SqlRepository();
        // GET: JoinAndDo
        public ActionResult Login()
        {
            var login = Request.Params["login"].Split( new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries )[0];
            var pass = Request.Params["pass"];
            User user = sqlRepository.Authentication( login, pass );
            user.hash = sqlRepository.SetHash( login, pass );

            #region cookies
            var cookieLogin = new HttpCookie("cookieLogin")
            {
                Name = "login",
                Value = login
            };
            var cookieHash = new HttpCookie( "cookieHash" )
            {
                Name = "hash",
                Value = user.hash
            };
            Response.SetCookie(cookieLogin);
            Response.SetCookie(cookieHash);
            #endregion

            //TempData["LeftBoxesCssDisplay"] = "block";

            return RedirectToAction("/Index");
        }

        public ActionResult Logout()
        {
            #region cookies
            var cookieLogin = new HttpCookie("cookieLogin")
            {
                Name = "login",
                Value = ""
            };
            var cookieHash = new HttpCookie("cookieHash")
            {
                Name = "hash",
                Value = ""
            };
            Response.SetCookie(cookieLogin);
            Response.SetCookie(cookieHash);
            #endregion
            return RedirectToAction("/Index");
        }
        public ActionResult Index()
        {
            ViewBag.listJoinsEntity = sqlRepository.GetAllFromJoins();
            //ViewBag.LeftBoxesCssDisplay = TempData["LeftBoxesCssDisplay"];
            return View();
        }

        public ActionResult my_accession()
        {
            string login = Request.Cookies["login"].Value;
            string hash = Request.Cookies["hash"].Value;
            if ( sqlRepository.IsAuthenticated( login, hash ) )
            {
                ViewBag.listMyAccession = sqlRepository.GetAllFromMyAccession();
                return View();
            }
            //ViewBag.LeftBoxesCssDisplay = "none";
            //ViewBag.LeftBoxesCssDisplay = "block";
            return RedirectToAction( "/Index" );
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