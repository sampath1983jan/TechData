using CustomAuthentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Tz.Net;

namespace Tz.BackApp.Controllers.Authentication
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        //https://www.codeproject.com/Tips/825266/ASP-NET-MVC-Dynamic-Routing
        //https://blog.maartenballiauw.be/post/2009/05/20/aspnet-mvc-domain-routing.html
        public ActionResult Index()
        {
           
            return View();
        }
        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {


            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
                // return Logout();
            }
            //ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        public JsonpResult SaveUser(string Username, string firstName, string lastName, string password, int userType,string email)
        {
            Tz.Net.User u = new Net.User(Username, password, (UserType)userType, true,firstName,lastName,email);
            u.Save();
            return new JsonpResult(u.UserID);
        }
        [HttpPost]
        public ActionResult Login(string userName,string Password,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(userName, Password))
                {
                    
                    var user = (CustomMembershipUser)Membership.GetUser(userName, false);
                    if (user != null)
                    {
                        Models.CustomSerializeModel userModel = new Models.CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            RoleName = user.Roles
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, userName, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return Json(new { redirecturl = "/Client/Client" }, JsonRequestBehavior.AllowGet);
                        //return RedirectToAction("Index", "Home", new { area = "" });
                        //return Redirect("Index");
                    }
                }
            }
            ModelState.AddModelError("loginerror", "Something Wrong : Username or Password invalid ^_^ ");
            return View();
        }

        public ActionResult Logout() {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
        cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Authentication", null);
    }
    }
}