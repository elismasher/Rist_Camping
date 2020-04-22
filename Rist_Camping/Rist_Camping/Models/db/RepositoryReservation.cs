using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace Rist_Camping.Models.db
{
    public class RepositoryReservation : RepositoryBase, IRepositoryReservation
    {
        public bool Insert(Reservation newReservation)
        {
            if (newReservation == null)
            {
                return false;
            }

            DbCommand cmdInsert = this._connection.CreateCommand();

            cmdInsert.CommandText = "INSERT INTO reservations VALUES(null, @firstname, @lastname, @email, @arrivalDate, @departureDate, @numberOfAdults, @numberOfChildren, @typeOfPlace);";

            DbParameter paramFirstname = cmdInsert.CreateParameter();
            paramFirstname.ParameterName = "firstname";
            paramFirstname.Value = newReservation.Firstname;
            paramFirstname.DbType = DbType.String;

            DbParameter paramLastname = cmdInsert.CreateParameter();
            paramLastname.ParameterName = "lastname";
            paramLastname.Value = newReservation.Lastname;
            paramLastname.DbType = DbType.String;

            DbParameter paramEMail = cmdInsert.CreateParameter();
            paramEMail.ParameterName = "email";
            paramEMail.Value = newReservation.EMail;
            paramEMail.DbType = DbType.String;

            DbParameter paramArrivalDate = cmdInsert.CreateParameter();
            paramArrivalDate.ParameterName = "arrivalDate";
            paramArrivalDate.Value = newReservation.ArrivalDate;
            paramArrivalDate.DbType = DbType.Date;

            DbParameter paramDepartureDate = cmdInsert.CreateParameter();
            paramDepartureDate.ParameterName = "departureDate";
            paramDepartureDate.Value = newReservation.DepartureDate;
            paramDepartureDate.DbType = DbType.Date;

            DbParameter paramNumberOfAdults = cmdInsert.CreateParameter();
            paramNumberOfAdults.ParameterName = "numberOfAdults";
            paramNumberOfAdults.Value = newReservation.NumberOfAdults;
            paramNumberOfAdults.DbType = DbType.Int32;

            DbParameter paramNumberOfChildren = cmdInsert.CreateParameter();
            paramNumberOfChildren.ParameterName = "numberOfChildren";
            paramNumberOfChildren.Value = newReservation.NumberOfChildren;
            paramNumberOfChildren.DbType = DbType.Int32;

            DbParameter paramTypeOfPlace = cmdInsert.CreateParameter();
            paramTypeOfPlace.ParameterName = "typeOfPlace";
            paramTypeOfPlace.Value = newReservation.TypeOfPlace;
            paramTypeOfPlace.DbType = DbType.Int32;


            cmdInsert.Parameters.Add(paramFirstname);
            cmdInsert.Parameters.Add(paramLastname);
            cmdInsert.Parameters.Add(paramEMail);
            cmdInsert.Parameters.Add(paramArrivalDate);
            cmdInsert.Parameters.Add(paramDepartureDate);
            cmdInsert.Parameters.Add(paramNumberOfAdults);
            cmdInsert.Parameters.Add(paramNumberOfChildren);
            cmdInsert.Parameters.Add(paramTypeOfPlace);


            return cmdInsert.ExecuteNonQuery() == 1;
        }
    }
}