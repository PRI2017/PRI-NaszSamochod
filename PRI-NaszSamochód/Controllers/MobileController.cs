﻿using Microsoft.AspNet.Identity.Owin;
using PRI_NaszSamochód.MobileAuthentication;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;
using PRI_NaszSamochód.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using PRI_NaszSamochód.Utilities;

namespace PRI_NaszSamochód.Controllers
{
    public class MobileController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        //private readonly IKeysHolder _keysHolder;

        public MobileController()
        {

        }

        public MobileController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IKeysHolder keysHolder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /* Mobile should send json with login, encrypted password (In Base64String !!!!!), and boolean RememberMe. Something like this:
             *
             *      {
             *          "Email" : "example@mail.com",
             *          "Password" : "WhvdZx7hx9RVVlYy6f7m2b2cY2AfcRN+EwcTHr8iy56k3LhhRFkIQq3el21YI9cVPWiUJG0uDibacXYAqQ1s97ycJn4O8G+
             *                        hFeYq3+SENkxbXFBHMfxN6QqRBT40N+pysJgN3ygc7fR7Ucu2/ZLhy5y6RiK5ZALo27XFXqsDt2c+FPEdz23lNFfSwOjAdPJynArU1KMTvvF7iPj6KAdpSZ795
             *                        /OEc0hzV/yA2udZd4RwwpZ6j4AMMo+sjo8qCnuVyTx4TVHRzWr7JRKiYmcb6PegZumuhDwRYTIMjEML0+o0USCMyOew1gLoL+N3iGM+YQJ8dlh+d6T1p3apEnkNmQ==",
             *          "RememberMe" : false
             *      }
             *      
             *      Still, it doesn't work - Null Object Reference in "SignInStatus x = await SignInManager.PasswordSignInAsync(" etc.
             */

        //
        // POST: Mobile/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl = null)
        {

            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SignInStatus x;
            try
            {
                //               byte[] passBytes = Convert.FromBase64String(model.Password);
                //               string password = CryptoRSA.Decrypt(passBytes, KeysHolder.Instance.PrivateKey);
                x = await SignInManager.PasswordSignInAsync(
                        model.Email,
                       model.Password,
                        model.RememberMe,
                        shouldLockout: false);
            }
            catch (Exception e)
            {
                x = SignInStatus.Failure;
            }

            switch (x)
            {
                case SignInStatus.Success:
                    var user =
                        SignInManager.AuthenticationManager;
                    var c = System.Web.HttpContext.Current.Response.Cookies;
                    return View();
                case SignInStatus.Failure:
                    return new HttpStatusCodeResult(HttpStatusCode.NoContent, "Sign in failed");
                case SignInStatus.LockedOut:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "User locked out");
                case SignInStatus.RequiresVerification:
                    return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "User requires verification");
                default:
                    return new HttpStatusCodeResult(HttpStatusCode.Conflict, "Server has a problem");
            }
        }

        public ActionResult Logout()
        {
            try
            {
                HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(666, ex.Message);
            }
        }

        public JsonResult UserGalleries()
        {
            var db = ApplicationDbContext.Create();
            var result = new List<GalleryMob>();
            var galleries = (from g in db.Galleries.Include("Owner")
                             select g).ToList();

            foreach (var i in galleries)
            {
                if (i.Owner.Id == User.Identity.GetUserId())
                {
                    var temp = new GalleryMob { id = i.Id, name = i.Name };
                    result.Add(temp);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}