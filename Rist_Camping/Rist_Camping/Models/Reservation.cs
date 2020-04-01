using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rist_Camping.Models
{
    public enum Place
    {
        stehplatz, huette, schlaffass, nichtAngegeben
    }
    public class Reservation
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EMail { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int? NumberOfAdults { get; set; }
        public int? NumberOfChildren { get; set; }
        public Place TypeOfPlace { get; set; }

        public Reservation(): this(0, "", "","", null, null, null, null, Place.nichtAngegeben) { }
        public Reservation(int id, string firstname, string lastname, string email, DateTime? arrivalDate, DateTime? departureDate, int? numberOfAdults, int? numberOfChildren, Place typeOfPlace)
        {
            this.ID = id;
            this.Firstname = firstname;
            this.Lastname = lastname;
            this.EMail = email;
            this.ArrivalDate = arrivalDate;
            this.DepartureDate = departureDate;
            this.NumberOfAdults = numberOfAdults;
            this.NumberOfChildren = numberOfChildren;
            this.TypeOfPlace = typeOfPlace;
        }
    }
}