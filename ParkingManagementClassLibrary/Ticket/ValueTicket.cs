using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingManagementClassLibrary.Ticket
{
    public class ValueTicket : ParkingTicket
    {
        public ValueTicket()
        {
            Cost = 100;
            MaxHeight = 2500;
            MaxWidth = 2400;
            MaxLength = 5000;
            TimeLimit = 72;
        }

        public override string ToString()
        {
            return "Value";
        }
    }
}
