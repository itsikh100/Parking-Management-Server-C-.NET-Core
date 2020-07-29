using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ParkingManagementClassLibrary.ParkingSpots
{
    public abstract class ParkingSpot
    {
        public int ParkingNumber { get; set; }
        public Vehicle Vehicle { get; set; }

        public bool IsOcupied { get; set; }

        public string SpotType { get; set; }

    }
}
