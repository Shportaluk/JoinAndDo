using JoinAndDo.Entities;
using JoinAndDo.Repositoryes;
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
            //ViewBag.LeftBoxesCssDisplay = TempData["LeftBoxesCssDisplay"];
            return View();
        }
        public ActionResult Login( string login, string pass )
        {
            string res = "OK";
            //var login = Request.Params["login"].Split( new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries )[0];
            //var pass = Request.Params["pass"];
            User user = sqlRepository.Authentication( login, pass );
            if (user.login == null)
            {
                res = "Invalid name or password";
                return Content(res);
            }
            user.hash = sqlRepository.SetHash( login, pass );
            #region cookies
            var cookieId = new HttpCookie("cookieId")
            {
                Name = "id",
                Value = user.id
            };
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
            Response.SetCookie(cookieId);
            Response.SetCookie(cookieLogin);
            Response.SetCookie(cookieHash);
            #endregion
            return Content(res);
        }
        [HttpPost]
        public ActionResult Registration( string login, string pass, string firstName, string lastName )
        {
            string res = sqlRepository.Registration( login, pass, firstName, lastName );
            return Content(res);
        }
        public void Logout( string login, string hash )
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
            sqlRepository.DeleteHash( login, hash );

            cookieLogin.Value = "";
            cookieHash.Value = "";

            Response.SetCookie(cookieLogin);
            Response.SetCookie(cookieHash);
            #endregion
            //return RedirectToAction("/Index");
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
        public ActionResult peopleId( int? id )
        {
            if (id == null)
            {
                //return RedirectToAction("/NoUser");
            }
            User user = sqlRepository.GetUserById( id.ToString() );
            if( user != null )
            {
                ViewBag.user = user;
            }
            else
            {
                //return RedirectToAction( "/NoUser" );
            }
            return View();
        }
        public ActionResult NoUser()
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


        public string GetLastMessages(string login, string hash)
        {
            return sqlRepository.GetLastMessages(login, hash);
        }
        public string CheckSms(string login, string hash)
        {
            return sqlRepository.GetCountMessages(login, hash);
        }
        public string SendMsg(string login, string hash, string to, string text)
        {
            return sqlRepository.SendMsg( login, hash, to, text );
        }
    }
}