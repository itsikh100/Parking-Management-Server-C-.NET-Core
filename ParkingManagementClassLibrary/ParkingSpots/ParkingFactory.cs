using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingManagementClassLibrary.ParkingSpots
{
    public class ParkingFactory
    {
        public static ParkingSpot CreateParkingSpot(Vehicle vehicle, string typeOfTicket, int parkingSpotNumber)
        {
            ParkingSpotManager parkingSpotManager = ParkingSpotManager.GetInstance;
            ParkingSpot typeOfParkingSpot = null;
            switch (typeOfTicket)
            {
                case "VIP":
                    if (parkingSpotManager.IsAreaAvailbleToPark("vip"))
                    {
                       typeOfParkingSpot = new VIPParkingSpot(vehicle , "VIP", parkingSpotNumber);
                    }
                    break;

                case "Value":
                    if (parkingSpotManager.IsAreaAvailbleToPark("value"))
                    {
                        typeOfParkingSpot = new ValueParkingSpot(vehicle , "Value", parkingSpotNumber);
                    }
                    break;

                case "Regular":
                    if (parkingSpotManager.IsAreaAvailbleToPark("regular"))
                    {
                        typeOfParkingSpot = new RegularParkingSpot(vehicle, "Regular", parkingSpotNumber);
                    }
                    break;
            }

            return typeOfParkingSpot;
        }
    }
}
