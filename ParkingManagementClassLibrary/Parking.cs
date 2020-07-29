using ParkingManagementClassLibrary.ParkingSpots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingManagementClassLibrary
{
    public class Parking
    {
        private static Parking Instance = null;
        private static readonly object LockObject = new object();
        ParkingSpotManager parkingSpotManager;

        private Parking()
        {
            parkingSpotManager = ParkingSpotManager.GetInstance;
        }

        public static Parking GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (LockObject)
                    {
                        if (Instance == null)
                        {
                            Instance = new Parking();
                        }
                    }
                }

                return Instance;
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            return parkingSpotManager.IsFull();
           
        }

        public bool IsAreaAvailbleToPark(string spotType)
        {
            return parkingSpotManager.IsAreaAvailbleToPark(spotType);
        }
        public List<string> GetAllVehiclesType()
        {
            return Enum.GetNames(typeof(eTypeVehicle)).ToList();
        }

        public Dictionary<int, string> GetStatusOfParking()
        {
            return parkingSpotManager.GetStatusOfParking();
        }

        public string AddNewVehicleToParkingSpot(string name, string licensePlateNumber, string phone, string ticketType,
                    string carType, string height, string width, string length)
        {
            return parkingSpotManager.AddNewVehicleToParkingSpot(name, licensePlateNumber, phone, ticketType,
                   carType, height, width, length);
        }

        public Vehicle GetVehicleByLicensePlateNumber(string licencePlateNumber)
        {
            return parkingSpotManager.GetVehicleByLicensePlateNumber(licencePlateNumber);
        }

        public bool RemoveVehicleFromParking(string licensePlateNumber)
        {
            return parkingSpotManager.RemoveVehicleFromParking(licensePlateNumber);
        }
    }
}
