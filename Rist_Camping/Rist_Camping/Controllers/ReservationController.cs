using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Rist_Camping.Models;
using Rist_Camping.Models.db;

namespace Rist_Camping.Controllers
{
    public class ReservationController : Controller
    {
        private IRepositoryReservation rep;

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Request()
        {
            return View(new Reservation());
        }

        [HttpPost]
        public ActionResult Request(Reservation reservation)
        {
            if (reservation == null)
            {
                return RedirectToAction("Request");
            }

            CheckReservationData(reservation);

            if (!ModelState.IsValid)
            {
                return View(reservation);
            }
            else
            {
                rep = new RepositoryReservation();

                rep.Open();

                if (rep.Insert(reservation))
                {
                    
                    rep.Close();
                    
                    return View("Message", new Message("Request", "Ihre Anfrage wurde erfolgreich abgeschickt!"));
                }
                else
                {
                    rep.Close();
                    
                    return View("Message", new Message("Request", "Ihre Anfrage konnten nicht abgeschickt werden!"));
                }
            }

        }



        private void CheckReservationData(Reservation reservation)
        {
            if (reservation == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(reservation.Lastname.Trim()))
            {
                ModelState.AddModelError("Lastname", "Nachname ist ein Pflichfeld.");
            }
            if (reservation.ArrivalDate == null)
            {
                ModelState.AddModelError("ArrivalDate", "Bitte wählen Sie ein Anreisedatum aus.");
            }
            if (reservation.DepartureDate == null)
            {
                ModelState.AddModelError("DepartureDate", "Bitte wählen Sie ein Abreisedatum aus.");
            }
            if (reservation.NumberOfAdults == null)
            {
                ModelState.AddModelError("NumberOfAdults", "Bitte geben Sie eine Anzahl der Erwachsenen ein.");
            }
            if (reservation.NumberOfChildren == null)
            {
                ModelState.AddModelError("NumberOfChildren", "Bitte geben Sie eine Anzahl der Kinder ein.");
            }
            if (!EmailContainsAddSign(reservation.EMail))
            {
                ModelState.AddModelError("EMail", "Email muss ein \"@\" Zeichen enthalten.");
            }
            if (!EmailContainsAddSign(reservation.EMail))
            {
                ModelState.AddModelError("EMail", "Email muss einen \".\" enthalten.");
            }
            if (reservation.TypeOfPlace == Place.nichtAngegeben)
            {
                ModelState.AddModelError("TypeOfPlace", "Schlafplatz muss ausgewählt werden.");
            }
        }

        private bool EmailContainsAddSign(string email)
        {
            string allowedChars = "@";
            int count = 0;
            int minCount = 1;

            foreach (char c in email)
            {
                if (allowedChars.Contains(c))
                {
                    count++;
                }
            }

            return count >= minCount;
        }

        private bool EmailContainsDot(string email)
        {
            string allowedChars = ".";
            int count = 0;
            int minCount = 1;

            foreach (char c in email)
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