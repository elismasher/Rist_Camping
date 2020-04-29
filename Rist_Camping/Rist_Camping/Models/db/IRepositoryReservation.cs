using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rist_Camping.Models.db
{
    interface IRepositoryReservation : IRepositoryBase
    {
        bool Insert(Reservation newReservation);
        List<Reservation> getAllReservations();
        bool UpdateReservationStatus(int id, bool newStatus);
        bool DeleteReservation(int id);

    }
}
