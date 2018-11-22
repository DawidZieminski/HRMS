using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS.Models;
using System.Net.Mail;
using System.Net;
using System.Web.Security;

namespace HRMS.Controllers
{
    public class UserController : Controller
    {
        //Rejestracja
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        //Rejestracja POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(HRMS.Models.User user)
        {
            bool Status = false;
            string message = "";

            // Walidacja
            if (ModelState.IsValid)
            {

                #region //Email is already Exist 
                var isExist = IsEmailExist(user.EmailID);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }
                #endregion



                #region  Password Hashing 
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword); //
                #endregion


                #region Save to Database
                using (HRMS.DataBaseAccess.HRMSContext dc = new HRMS.DataBaseAccess.HRMSContext())
                {
                    dc.User.Add(user);
                    dc.SaveChanges();
                    Status = true;
                }
                #endregion
            }
            else
            {
                message = "Nieprawidłowe żądanie";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View(user);
        }
        //Weryfikacja  

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (HRMS.DataBaseAccess.HRMSContext dc = new HRMS.DataBaseAccess.HRMSContext())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;



                dc.SaveChanges();
                Status = true;


            }
            ViewBag.Status = Status;
            return View();
        }

        //Login 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //Login POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl = "")
        {
            string message = "";
            using (HRMS.DataBaseAccess.HRMSContext dc = new HRMS.DataBaseAccess.HRMSContext())
            {
                var v = dc.User.Where(a => a.EmailID == login.EmailID).FirstOrDefault();
                if (v != null)
                {


                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.EmailID, login.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);


                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        message = "Podano niepoprawne dane logowania";
                    }
                }
                else
                {
                    message = "Podano niepoprawne dane logowania";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }


        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (HRMS.DataBaseAccess.HRMSContext dc = new HRMS.DataBaseAccess.HRMSContext())
            {
                var v = dc.User.Where(a => a.EmailID == emailID).FirstOrDefault();
                return v != null;
            }
        }





    }
}