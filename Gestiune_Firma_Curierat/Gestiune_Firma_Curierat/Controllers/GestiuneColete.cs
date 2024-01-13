using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            if (User.Identity.IsAuthenticated)
            {
                return View(db.Colete.ToList());
            }
            else
            {
                return View("GuestIndex", db.Colete.ToList());
            }
        }

        public IActionResult AdaugaColet()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeletePackage(int packageId)
        {
            var package = db.Colete.Find(packageId);

            if (package != null)
            {
                db.Colete.Remove(package);
                db.SaveChanges();
            }

            return RedirectToAction("Index");

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

        public IActionResult Colet(string coletId)
        {
            var colet = db.Colete.Find(Int32.Parse(coletId));

            if(colet != null)
                return View(colet);
            else 
                return Redirect("/");

        }
        public IActionResult Istoric(string destinatar)
        {
            var colete = db.Colete.Where(colet => colet.Destinatar == destinatar).ToList();

            if (colete.Count > 0)
                return View(colete);
            else
                return Redirect("/");

        }
        public IActionResult Users()
        {

            return View(db.Users.ToList());

        }
        public IActionResult DeleteUser(int UserId)
        {
            var user = db.Users.Find(UserId);

            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }

            return RedirectToAction("Users");

        }
    }
}
