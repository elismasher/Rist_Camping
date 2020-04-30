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

            cmdInsert.CommandText = "INSERT INTO reservations VALUES(null, @firstname, @lastname, @email, @arrivalDate, @departureDate, @numberOfAdults, @numberOfChildren, @typeOfPlace, @status);";

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

            DbParameter paramTypeOfStatus = cmdInsert.CreateParameter();
            paramTypeOfStatus.ParameterName = "status";
            paramTypeOfStatus.Value = newReservation.Status;
            paramTypeOfStatus.DbType = DbType.Boolean;



            cmdInsert.Parameters.Add(paramFirstname);
            cmdInsert.Parameters.Add(paramLastname);
            cmdInsert.Parameters.Add(paramEMail);
            cmdInsert.Parameters.Add(paramArrivalDate);
            cmdInsert.Parameters.Add(paramDepartureDate);
            cmdInsert.Parameters.Add(paramNumberOfAdults);
            cmdInsert.Parameters.Add(paramNumberOfChildren);
            cmdInsert.Parameters.Add(paramTypeOfPlace);
            cmdInsert.Parameters.Add(paramTypeOfStatus);


            return cmdInsert.ExecuteNonQuery() == 1;
        }

        public List<Reservation> getAllReservations()
        {
            List<Reservation> reservations = new List<Reservation>();

            DbCommand cmdSelect = this._connection.CreateCommand();
            cmdSelect.CommandText = "SELECT * FROM reservations ORDER BY arrivalDate ASC";

            using (DbDataReader reader = cmdSelect.ExecuteReader())
            {
                while (reader.Read())
                {
                    reservations.Add(
                        new Reservation
                        {
                            ID = Convert.ToInt32(reader["id"]),
                            Firstname = Convert.ToString(reader["firstname"]),
                            Lastname = Convert.ToString(reader["lastname"]),
                            EMail = Convert.ToString(reader["email"]),
                            ArrivalDate = Convert.ToDateTime(reader["arrivalDate"]),
                            DepartureDate = Convert.ToDateTime(reader["departureDate"]),
                            NumberOfAdults = Convert.ToInt32(reader["numberOfAdults"]),
                            NumberOfChildren = Convert.ToInt32(reader["numberOfChildren"]),
                            TypeOfPlace = (Place)Convert.ToInt32(reader["typeOfPlace"]),
                            Status = Convert.ToBoolean(reader["statusReservierung"])
                        });
                }
            }
            return reservations;
        }

        public bool UpdateReservationStatus(int id, bool newStatus)
        {
            DbCommand cmdUpdate = this._connection.CreateCommand();
            cmdUpdate.CommandText = "UPDATE reservations SET statusReservierung=@newStatus WHERE id=@ID";

            DbParameter paramId = cmdUpdate.CreateParameter();
            paramId.ParameterName = "ID";
            paramId.Value = id;
            paramId.DbType = DbType.Int32;

            DbParameter paramStatus = cmdUpdate.CreateParameter();
            paramStatus.ParameterName = "newStatus";
            paramStatus.Value = newStatus;
            paramStatus.DbType = DbType.Boolean;

            cmdUpdate.Parameters.Add(paramId);
            cmdUpdate.Parameters.Add(paramStatus);

            return cmdUpdate.ExecuteNonQuery() == 1;
        }

        public bool DeleteReservation(int id)
        {
            DbCommand cmdDel = this._connection.CreateCommand();
            cmdDel.CommandText = "DELETE FROM reservations WHERE id=@ID";

            DbParameter paramId = cmdDel.CreateParameter();
            paramId.ParameterName = "ID";
            paramId.Value = id;
            paramId.DbType = DbType.Int32;

            cmdDel.Parameters.Add(paramId);

            return cmdDel.ExecuteNonQuery() == 1;
        }

    }
}