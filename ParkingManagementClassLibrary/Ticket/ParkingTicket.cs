using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ParkingManagementClassLibrary.Ticket
{
    public class ParkingTicket
    {

        public int Cost { get; protected set; }
        public double MaxHeight { get; protected set; }
        public double MaxWidth { get; protected set; }
        public double MaxLength { get; protected set; }
        public int TimeLimit { get; protected set; }

        public ParkingTicket()
        {
            Cost = 0;
            MaxHeight = double.MaxValue;
            MaxWidth = double.MaxValue;
            MaxLength = double.MaxValue;
        }

        public ParkingTicket(int cost, double maxHeight, double maxWidth, double maxLength)
        {
            Cost = cost;
            MaxHeight = maxHeight;
            MaxWidth = maxWidth;
            MaxLength = maxLength;
        }

        public bool checkDimension(double Height, double Width, double Length)
        {
            bool returnValue = false;
            if (Height < MaxHeight && Width < MaxWidth && Length < MaxLength)
            {
                returnValue = true;
            }

            return returnValue;
        }

    }
}
