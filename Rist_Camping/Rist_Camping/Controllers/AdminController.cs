using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Rist_Camping.Models;
using Rist_Camping.Models.db;

namespace Rist_Camping.Controllers
{
    public class AdminController : Controller
    {
        private IRepositoryUser rep;
        private IRepositoryReservation rep2;

        public ActionResult Login()
        {
            return View(new UserLogin());
        }
        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            User userFromDB;

            rep = new RepositoryUser();
            rep.Open();
            userFromDB = rep.Login(user);
            rep.Close();

            if (userFromDB == null)
            {
                ModelState.AddModelError("Username", "Benutzername oder Passwort stimmen nicht überein!");
                return View(user);
            }
            else
            {
                Session["loggedInUser"] = userFromDB;

                if(userFromDB.UserRole == UserRole.admin)
                {
                    Session["isAdmin"] = true;
                }
                else
                {
                    Session["isAdmin"] = false;
                }
                
                
                return RedirectToAction("index", "home");
            }
        }

        public ActionResult Registration()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Registration(User user)
        {
            if (user == null)
            {
                return View(user);
            }

            CheckUserData(user);

            if (!ModelState.IsValid)
            {
                return View(user);
            }
            else
            {
                user.UserRole = UserRole.registeredUser;

                rep = new RepositoryUser();

                rep.Open();
                if (rep.Insert(user))
                {
                    rep.Close();
                    return RedirectToAction("Login");
                }
                else
                {
                    rep.Close();
                    return View(user);
                }
            }
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult ReservationRequests()
        {
            List<Reservation> reservations;
            rep2 = new RepositoryReservation();

            rep2.Open();
            reservations = rep2.getAllReservations();
            rep2.Close();

            List<Reservation> newReservations = new List<Reservation>();

            foreach(var r in reservations)
            {
                if(!r.Status)
                {
                    newReservations.Add(r);
                }
            }

            return View(newReservations);
        }

        
        public ActionResult SubmitReservationRequest(int idReservation)
        {
            if (Session["loggedInUser"] == null)
            {
                return RedirectToAction("login", "admin");
            }

            if (!Convert.ToBoolean(Session["isAdmin"]))
            {
                return RedirectToAction("index", "home");
            }

            rep2 = new RepositoryReservation();

            rep2.Open();
            rep2.UpdateReservationStatus(idReservation, true);
            rep2.Close();
            return View();
        }

        public ActionResult DeleteReservationRequests(int idReservation)
        {
            if (Session["loggedInUser"] == null)
            {
                return RedirectToAction("login", "admin");
            }

            if (!Convert.ToBoolean(Session["isAdmin"]))
            {
                return RedirectToAction("index", "home");
            }

            rep2 = new RepositoryReservation();

            rep2.Open();
            rep2.DeleteReservation(idReservation);
            rep2.Close();
            return RedirectToAction("ReservationRequests", "admin");
        }

        public ActionResult RegisteredUsers()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["loggedInUser"] = null;
            Session["isAdmin"] = false;
            return RedirectToAction("index", "home");
        }




        // Methoden:
        private void CheckUserData(User user)
        {
            if (user == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(user.Firstname.Trim()))
            {
                ModelState.AddModelError("Firstname", "Vorname muss eingetragen werden");
            }
            if (string.IsNullOrEmpty(user.Lastname.Trim()))
            {
                ModelState.AddModelError("Lastname", "Nachname muss eingetragen werden");
            }
            if (string.IsNullOrEmpty(user.Username.Trim()))
            {
                ModelState.AddModelError("Username", "Ein Username muss angegeben werden");
            }

            if (!CheckPassword(user.Password))
            {
              ModelState.AddModelError("Password", "Passwort muss mindestens 8 Zeichen lang sein und mindestens einen Großbuchstaben und mindstens eine Zahl enthalten.");
            }

            if (user.Password != user.PasswordConfirm)
            {
                ModelState.AddModelError("PasswordConfirm", "Die Passwörter stimmen nicht überein!");
            }

            if (!EmailContainsAddSign(user.Email, 1))
            {
                ModelState.AddModelError("Email", "Email muss ein @ Zeichen enthalten.");
            }

            if (!EmailContainsAddSign(user.Email, 1))
            {
                ModelState.AddModelError("Email", "Email muss  einen . enthalten.");
            }

        }

        private bool CheckPassword(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }

            if (!PWContainsNumber(password, 1))
            {
                return false;
            }
            if (!PWContainsUppercaseCharacter(password, 1))
            {
                return false;
            }

            return true;
        }

       private bool PWContainsUppercaseCharacter(string text,int minCount)
       {
               int count = 0;
               foreach (char c in text)
                {
                    if (char.IsUpper(c))
                    {
                        count++;
                    }
                }

           return count >= minCount;
       }

        private bool PWContainsNumber(string text, int minCount)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (char.IsNumber(c))
                {
                    count++;
                }
            }
            return count >= minCount;
        }

        private bool EmailContainsAddSign(string text, int minCount)
        {
            string allowedChars = "@";
            int count = 0;
            foreach (char c in text)
            {
                if (allowedChars.Contains(c))
                {
                    count++;
                }
            }

            return count >= minCount;
        }

        private bool EmailContainsDot(string text, int minCount)
        {
            string allowedChars = ".";
            int count = 0;
            foreach (char c in text)
            {

                if (allowedChars.Contains(c))
                {
                    count++;
                }
            }
            return count >= minCount;
        }
    }
}