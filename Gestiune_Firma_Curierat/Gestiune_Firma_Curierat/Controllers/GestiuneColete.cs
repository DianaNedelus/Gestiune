using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace Gestiune_Firma_Curierat.Controllers
{
    public class GestiuneColeteController : Controller
    {

        private readonly FirmaContext db;
        public GestiuneColeteController(FirmaContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            if (Request.Cookies["LoggedIn"] != null && Request.Cookies["LoggedIn"].ToLower() == "true")
            {
                return View(db.Colete.ToList());
            }
            return View("GuestIndex", db.Colete.ToList());
        }

        public IActionResult AdaugaColet()
        {
            if (Request.Cookies["LoggedIn"] != null && Request.Cookies["LoggedIn"].ToLower() == "true")
            {
                return View();
            }

            return RedirectToAction("Login");

        }

        [HttpPost]
        public IActionResult AdaugaColet(Colet colet)
        {

            colet.Stare = "Expediat";
            db.Add(colet);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult ChangeState(int coletId)
        {
            var colet = db.Colete.Find(coletId);

            if (colet != null)
            {
                // Cycle through enum values (assuming it's a circular enum)
                if (colet.Stare == "Tranzit")
                {
                    colet.Stare = "Preluat";
                }
                else
                {
                    colet.Stare = "Tranzit";
                }

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Users()
        {
            if (Request.Cookies["LoggedIn"] != null && Request.Cookies["LoggedIn"].ToLower() == "true" &&
                Request.Cookies["isAdmin"] != null && Request.Cookies["isAdmin"].ToLower() == "true")
            {
                return View(db.Users.ToList());
            }

            return RedirectToAction("Login");

        }

        public IActionResult DeleteUser(int UserId)
        {
            if (Request.Cookies["LoggedIn"] != null && Request.Cookies["LoggedIn"].ToLower() == "true" &&
                Request.Cookies["isAdmin"] != null && Request.Cookies["isAdmin"].ToLower() == "true")
            { 
                var user = db.Users.Find(UserId);

                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Users");

        }

        public IActionResult Login(User user)
        {
           
            if (user.Username == null) return View();


            var connectedUser = db.Users.First(x => x.Username ==  user.Username);

            if (connectedUser != null)
            {
                if(connectedUser.Parola == user.Parola)
                {
                   Response.Cookies.Append(
                        "LoggedIn",
                        "True",
                        new CookieOptions()
                        {
                            Path = "/"
                        }
                    );

                    if(connectedUser.isAdmin == true)
                    {
                        Response.Cookies.Append(
                            "isAdmin",
                            "True",
                            new CookieOptions()
                            {
                                Path = "/"
                            }
                        );
                    }

                    return RedirectToAction("AdaugaColet");
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Signup(User user)
        {

            if (user.Username == null) return View();


            db.Users.Add(user);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
