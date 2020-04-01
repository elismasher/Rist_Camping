using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rist_Camping.Models.db
{
    interface IRepositoryReservation
    {
        void Open();
        void Close();
        bool Insert(Reservation newReservation);
    }
}
