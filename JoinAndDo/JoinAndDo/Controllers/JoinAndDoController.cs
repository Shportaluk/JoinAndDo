using JoinAndDo.Entities;
using JoinAndDo.Repositoryes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JoinAndDo.Controllers
{
    public class JoinAndDoController : Controller
    {
        private SqlRepository _sqlRepository = new SqlRepository();
        // GET: JoinAndDo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult new_join()
        {
            return View();
        }

        public ActionResult Login( string login, string pass )
        {
            string res = "OK";
            User user = _sqlRepository.Authentication( login, CalculateMD5Hash(pass) );
            if (user.Login == null)
            {
                res = "Invalid name or password";
                return Content(res);
            }
            user.Hash = _sqlRepository.SetHash( login, CalculateMD5Hash(pass) );
            #region cookies
            var cookieId = new HttpCookie("cookieId")
            {
                Name = "id",
                Value = user.Id
            };
            var cookieLogin = new HttpCookie("cookieLogin")
            {
                Name = "login",
                Value = login
            };
            var cookieHash = new HttpCookie( "cookieHash" )
            {
                Name = "hash",
                Value = user.Hash
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
            string res = _sqlRepository.Registration( login, CalculateMD5Hash( pass ), firstName, lastName );
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
            _sqlRepository.DeleteHash( login, hash );

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
                ViewBag.listMyAccessionsManagement = _sqlRepository.GetMyAccessionsManagement(login, hash);
                ViewBag.listMyAccessions = _sqlRepository.GetMyAccessions(login, hash);
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

                List<string> interlocutors = _sqlRepository.GetInterlocutors(cookieLogin);
                List<Interlocutor> listInterlocutors = new List<Interlocutor>();

                foreach (string login in interlocutors)
                {
                    listInterlocutors.Add(new Interlocutor(login, _sqlRepository.GetDialog(cookieLogin, cookieHash, login)));
                }
                ViewBag.listInterlocutors = listInterlocutors;
                return View();
            }
            catch
            {
                return Redirect( "/JoinAndDo" );
            }
            
        }
        public ActionResult peopleId( string id )
        {
            if (id == null)
            {
                return RedirectToAction("/NoUser");
            }

            int res;
            User user;
            if ( int.TryParse(id, out res) )
            {
                user = _sqlRepository.GetUserById(res);
            }
            else
            {
                user = _sqlRepository.GetUserByLogin(id);
            }
            
           
            ViewBag.IsMyProfile = false;
            if (user != null)
            {
                ViewBag.user = user;
                ViewBag.skills = _sqlRepository.GetSkillsOfUserByLogin(user.Login);
                try
                {
                    if (user.Login == Request.Cookies["login"].Value)
                    {
                        ViewBag.IsMyProfile = true;
                    }
                }
                catch { }
            }
            else
            {
                return RedirectToAction("/NoUser");
            }
            return View();
        }
        public ActionResult NoUser()
        {
            return View();
        }
        public ActionResult deals_accession()
        {
            ViewBag.listDealsAccession = _sqlRepository.GetAllFromDealsAccession();
            return View();
        }
        public ActionResult search_people(string name)
        {
            ViewBag.listUser = _sqlRepository.GetUsers( name );
            return View();
        }
        public ActionResult search_accession(string text)
        {
            ViewBag.listAccession = _sqlRepository.GetAccessions( text );
            return View();
        }
        public ActionResult AdminPanel()
        {
            return View();
        }
        public ActionResult AccessionId( int? id )
        {
            if (id == null)
            {
                return RedirectToAction("/NoAccession");
            }

            Accession accession = _sqlRepository.GetAccessionById( id );
            if (accession != null)
            {
                ViewBag.IsInAccession = false;
                ViewBag.DialogInAccession = null;
                accession.ListAvailableRoles = _sqlRepository.GetListAvailableRolesOfAccessionById(id);
                ViewBag.Accession = accession;
                try
                {
                    string login = Request.Cookies["login"].Value;
                    string hash = Request.Cookies["hash"].Value;

                    List<User> users = _sqlRepository.GetUsersByIdOfAccession(id);
                    foreach (User user in users)
                    {
                        if (user.Login == login )
                        {
                            ViewBag.IsInAccession = true;
                            ViewBag.DialogInAccession = GetDialogOfAccession( login, hash, accession.Id );
                        }
                    }
                    List<RequestJoinToAccession> listRequestsAdditionOf = _sqlRepository.GetRequestsAdditionToAccession(id);
                    //for ( int i = 0; i < users.Count; i++ )
                    //{
                    //    string role = users[i].Role;
                    //    users[i] = _sqlRepository.GetUserByLogin( users[i].Login );
                    //    users[i].Role = role;
                    //}
                    ViewBag.ListUsers = users;
                    ViewBag.ListRequestsAdditionOf = listRequestsAdditionOf;
                }
                catch
                {
                    ViewBag.ListUsers = new List<User>();
                    ViewBag.ListRequestsAdditionOf = new List<RequestJoinToAccession>();
                    return View();
                }
            }
            else
            {
                return RedirectToAction("/NoAccession");
            }

            return View();
        }
        public ActionResult NoAccession()
        {
            return View();
        }
        public ActionResult MyInvitation()
        {
            try
            {
                string login = Request.Cookies["login"].Value;
                string hash = Request.Cookies["hash"].Value;
                ViewBag.listAccession = _sqlRepository.GetMyInvitation(login, hash);
                return View();
            }
            catch
            {
                return RedirectToAction("/Index");
            }
        }

        public string RemoveUserFromAccession(string login, string hash, int idAccession, string user)
        {
            return _sqlRepository.RemoveUserFromAccession(login, hash, idAccession, user);
        }
        public string AddSkillToUser(string login, string hash, string pathImg, string name)
        {
            return _sqlRepository.AddSkillToUser(login, hash, pathImg, name);
        }
        public string SendRequestCompleteAccession(string login, string hash, int idAccession)
        {
            return _sqlRepository.SendRequestCompleteAccession(login,hash,idAccession);
        }
        public string LoadProfileImg( HttpPostedFileBase fileInput)
        {
            try
            {
                string login = Request.Cookies["login"].Value;
                string hash = Request.Cookies["hash"].Value;

                string fileName = CalculateMD5Hash(DateTime.Now.ToString()) + ".bmp";
                var image = new Bitmap(fileInput.InputStream);
                var imageMini = new Bitmap( image, new Size(64, 64) );
                string pathFile = Path.Combine(HttpContext.Server.MapPath("/Images/"), fileName);
                string pathFileMini = Path.Combine(HttpContext.Server.MapPath("/Images/"), "mini_" + fileName);
                if (!System.IO.File.Exists(pathFile))
                {
                    image.Save(pathFile, System.Drawing.Imaging.ImageFormat.Bmp);
                    imageMini.Save(pathFileMini, System.Drawing.Imaging.ImageFormat.Bmp);

                    string res = _sqlRepository.LoadProfileImg(login, hash, fileName);
                    if (res == "Ok")
                    {
                        return fileName;
                    }
                    else
                    {
                        return res;
                    }
                }
                return "Error Load";
            }
            catch
            {
                return "Error";
            }
        }
        public string ExitWithAccession( int idAccession )
        {
            try
            {
                string login = Request.Cookies["login"].Value;
                string hash = Request.Cookies["hash"].Value;

                return _sqlRepository.ExitWithAccession(login, hash, idAccession);
            }
            catch
            {
                return "Error";
            }

        }
        public string EditTitleOfAccession(string login, string hash, int idAccession, string title)
        {
            return _sqlRepository.EditTitleOfAccession(login, hash, idAccession, title);
        }
        public string EditDescriptionOfAccession(string login, string hash, int idAccession, string description)
        {
            return _sqlRepository.EditDescriptionOfAccession( login, hash, idAccession, description );
        }
        public string SendRequestToAccession(string login, string hash, string text, string category, int idAccession)
        {
            return _sqlRepository.SendRequestToAccession( login, hash, text, category, idAccession );
        }
        public string AcceptRequestOfUserToAccession(string login, string hash, string user, string role, string idAccession)
        {
            return _sqlRepository.AcceptRequestOfUserToAccession(login, hash, user, role, idAccession);
        }
        public string RejectRequestOfUserToAccession(string login, string hash, string user, string idAccession)
        {
            return _sqlRepository.RejectRequestOfUserToAccession(login, hash, user, idAccession);
        }
        public string DeleteJoin( string login, string hash, int idAccession )
        {
            return _sqlRepository.DeleteJoin( login, hash, idAccession );
        }
        public string NewJoin(string login, string hash, string name, string text, string category, string needPeople, string listRoles)
        {
            return _sqlRepository.NewJoin(login, hash, name, text, category, needPeople, listRoles);
        }
        public string GetLastMessages(string login, string hash)
        {
            return _sqlRepository.GetLastMessages(login, hash);
        }
        public string CheckSms(string login, string hash)
        {
            return _sqlRepository.GetCountMessages(login, hash);
        }
        public string SendMsg(string login, string hash, string to, string text)
        {
            return _sqlRepository.SendMsg( login, hash, to, text );
        }
        public string SendMsgToAccession(string login, string hash, int idAccession, string text)
        {
            return _sqlRepository.SendMsgToAccession(login, hash, idAccession, text);
        }
        public List<Message> GetDialogOfAccession(string login, string hash, int idAccession)
        {
            return _sqlRepository.GetDialogOfAccession(login, hash, idAccession);
        }

        private string CalculateMD5Hash(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();

        }
    }
}