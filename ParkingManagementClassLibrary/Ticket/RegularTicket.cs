using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingManagementClassLibrary.Ticket
{
    public class RegularTicket : ParkingTicket
    {
        public RegularTicket()
        {
            Cost = 50;
            MaxHeight = 2000;
            MaxWidth = 2000;
            MaxLength = 3000;
            TimeLimit = 24;
        }

        public override string ToString()
        {
            return "Regular";
        }
    }
}
