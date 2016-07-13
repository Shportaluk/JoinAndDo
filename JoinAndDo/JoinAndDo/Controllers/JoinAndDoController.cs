using JoinAndDo.Entities;
using JoinAndDo.Repositoryes;
using System.Collections.Generic;
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

        public ActionResult new_join()
        {
            return View();
        }

        public ActionResult Login( string login, string pass )
        {
            string res = "OK";
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
        public ActionResult Logout( string login, string hash )
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
            return Redirect("/JoinAndDo");
        }
        public ActionResult my_accession()
        {
            try
            {
                string login = Request.Cookies["login"].Value;
                string hash = Request.Cookies["hash"].Value;
                ViewBag.listMyAccession = sqlRepository.GetAccessions(login, hash);
                return View();
            }
            catch
            {
                return RedirectToAction("/Index");
            }
        }
        public ActionResult my_message()
        {
            try
            {
                string cookieLogin = HttpContext.Request.Cookies["login"].Value;
                string cookieHash = HttpContext.Request.Cookies["hash"].Value;

                List<string> interlocutors = sqlRepository.GetInterlocutors(cookieLogin);
                List<Interlocutor> listInterlocutors = new List<Interlocutor>();

                foreach (string login in interlocutors)
                {
                    listInterlocutors.Add(new Interlocutor(login, sqlRepository.GetDialog(cookieLogin, cookieHash, login)));
                }
                ViewBag.listInterlocutors = listInterlocutors;
                return View();
            }
            catch
            {
                return Redirect( "/JoinAndDo" );
            }
            
        }
        public ActionResult peopleId( int? id )
        {
            if (id == null)
            {
                return RedirectToAction("/NoUser");
            }

            
            User user = sqlRepository.GetUserById( id.ToString() );
            if( user != null )
            {
                ViewBag.user = user;
            }
            else
            {
                return RedirectToAction( "/NoUser" );
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
        public ActionResult search_people(string name)
        {
            ViewBag.listUser = sqlRepository.GetUsers( name );
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

        public void NewJoin(string login, string hash, string name, string text, string category, string needPeople)
        {
            sqlRepository.NewJoin(login, hash, name, text, category, needPeople);
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