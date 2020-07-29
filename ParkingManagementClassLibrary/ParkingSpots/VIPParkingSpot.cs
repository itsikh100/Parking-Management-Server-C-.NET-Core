using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingManagementClassLibrary.ParkingSpots
{
    public class VIPParkingSpot : ParkingSpot
    {
        public VIPParkingSpot(Vehicle vehicle, string spotType, int parkingNumber)
        {
            Vehicle = vehicle;
            SpotType = spotType;
            ParkingNumber = parkingNumber;
        }
    }
}
