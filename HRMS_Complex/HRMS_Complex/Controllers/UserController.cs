using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HRMS_Complex.Models;
using System.Web.Security;

namespace HRMS_Complex.Controllers
{
    public class UserController : Controller
    {
        //Rejestracja
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        // Rejestracja POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User user)
        {
            bool Status = false;
            string message = "";
       
            if (ModelState.IsValid)
            {

                #region //Email is already Exist 
                var isExist = IsEmailExist(user.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "To konto już istnieje, zaloguj się");
                    return View(user);
                }
                #endregion



                #region  Password Hashing 
                user.Password = Crypto.Hash(user.Password);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword); //
                #endregion


                #region Save to Database
                using (HRMS_DBEntities dc = new HRMS_DBEntities())
                {
                    dc.User.Add(user);
                    dc.SaveChanges();

                    //Send Email to User

                    message = "Witaj" + user.FirstName + ". Rejestracja przebiegła pomyślnie";
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
            using (HRMS_DBEntities dc = new HRMS_DBEntities())
            {
                var v = dc.User.Where(a => a.Email == login.Email).FirstOrDefault();
                if (v != null)
                {
                  

                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                        var ticket = new FormsAuthenticationTicket(login.Email, login.RememberMe, timeout);
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
        public bool IsEmailExist(string email)
        {
            using (HRMS_DBEntities dc = new HRMS_DBEntities())
            {
                var v = dc.User.Where(a => a.Email == email).FirstOrDefault();
                return v != null;
            }
        }
    }
}