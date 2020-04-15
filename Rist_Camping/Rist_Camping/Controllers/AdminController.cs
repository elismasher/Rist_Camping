﻿using System;
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
        private RepositoryUser rep;

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
                return RedirectToAction("Registration");
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
                    return RedirectToAction("Registration");
                }
            }
        }

        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult ReservationRequests()
        {
            return View();
        }

        public ActionResult RegisteredUsers()
        {
            return View();
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