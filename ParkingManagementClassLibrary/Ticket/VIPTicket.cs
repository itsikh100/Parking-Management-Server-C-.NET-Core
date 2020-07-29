using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingManagementClassLibrary.Ticket
{
    public class VIPTicket : ParkingTicket
    {
        public VIPTicket() 
        {
            Cost = 200;
        }

        public override string ToString()
        {
            return "VIP";
        }
    }
}
