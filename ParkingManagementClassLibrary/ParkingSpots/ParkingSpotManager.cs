using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingManagementClassLibrary.ParkingSpots
{
    public class ParkingSpotManager
    {
        private static ParkingSpotManager Instance = null;
        private static readonly object LockObject = new object();

        private Dictionary<int, string> parkingSpots;

        private Dictionary<string, ParkingSpot> VIPSpots;
        private Dictionary<string, ParkingSpot> ValueSpots;
        private Dictionary<string, ParkingSpot> RegularSpots;

        private ParkingSpotManager()
        {
            VIPSpots = new Dictionary<string, ParkingSpot>(10);
            ValueSpots = new Dictionary<string, ParkingSpot>(20);
            RegularSpots = new Dictionary<string, ParkingSpot>(30);

            parkingSpots = new Dictionary<int, string>(60);
            for(int i = 1; i <= 60; i++)
            {
                parkingSpots.Add(i, "");
            }
        }

        public static ParkingSpotManager GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (LockObject)
                    {
                        if (Instance == null)
                        {
                            Instance = new ParkingSpotManager();
                        }
                    }
                }

                return Instance;
            }
        }

        public string AddNewVehicleToParkingSpot(string name, string licensePlateNumber, string phone, string ticketType,
                    string carType, string height, string width, string length)
        {
            Enum.TryParse("2", out eTypeVehicle eCarType); //problem
            var d_height = Convert.ToDouble(height);
            var d_width = Convert.ToDouble(width);
            var d_length = Convert.ToDouble(length);
            Vehicle vehicle = new Vehicle(name, licensePlateNumber, phone, eCarType, d_height, d_width, d_length);

            var parkingSpotNumber = GetParkingSpotNumber(ticketType);
            var newParkingSpot = ParkingFactory.CreateParkingSpot(vehicle, ticketType, parkingSpotNumber);
            if (newParkingSpot == null)
            {
                return ticketType + " spots are not availble";
            }
            if(GetVehicleByLicensePlateNumber(vehicle.LicenseNumber) != null)
            {
                return ("This car is already in the parking");
            }
            else
            {
                parkingSpots[parkingSpotNumber] = vehicle.LicenseNumber;
                if (ticketType == "VIP")
                {
                    VIPSpots.Add(vehicle.LicenseNumber, newParkingSpot);
                }
                if (ticketType == "Value")
                {
                    ValueSpots.Add(vehicle.LicenseNumber, newParkingSpot);
                }
                if (ticketType == "Regular")
                {
                    RegularSpots.Add(vehicle.LicenseNumber, newParkingSpot);
                }
            }

            return "Added new Parkin spot";
        }

        private int GetParkingSpotNumber(string ticketType)
        {
            var result = 0;
            if (ticketType.ToLower() == "vip")
            {
                for (int i = 1; i <= 10; i++)
                {
                    if (parkingSpots[i] == "")
                    {
                        return i;
                    }
                }
            }
            if (ticketType.ToLower() == "value")
            {
                for (int i = 11; i <= 30; i++)
                {
                    if (parkingSpots[i] == "")
                    {
                        return i;
                    }
                }
            }
            if (ticketType.ToLower() == "regular")
            {
                for (int i = 31; i <= 60; i++)
                {
                    if (parkingSpots[i] == "")
                    {
                        return i;
                    }
                }
            }

            return result;
        }

        public Dictionary<int, string> GetStatusOfParking()
        {
            return parkingSpots;
        }

        public bool RemoveVehicleFromParking(string licensePlateNumber)
        {
            bool result = false;
            Vehicle vehicle = GetVehicleByLicensePlateNumber(licensePlateNumber);
            if(vehicle != null)
            {
                result = true;
                if (VIPSpots.ContainsKey(licensePlateNumber))
                {
                    parkingSpots[VIPSpots[licensePlateNumber].ParkingNumber] = "";
                    VIPSpots.Remove(licensePlateNumber);
                }
                if (ValueSpots.ContainsKey(licensePlateNumber))
                {
                    parkingSpots[ValueSpots[licensePlateNumber].ParkingNumber] = "";
                    ValueSpots.Remove(licensePlateNumber);
                }
                if (RegularSpots.ContainsKey(licensePlateNumber))
                {
                    parkingSpots[RegularSpots[licensePlateNumber].ParkingNumber] = "";
                    RegularSpots.Remove(licensePlateNumber);
                }
            }

            return result;
        }

        public Vehicle GetVehicleByLicensePlateNumber(string licencePlateNumber)
        {
            Vehicle vehicle = null;
            if (VIPSpots.ContainsKey(licencePlateNumber))
            {
                vehicle = VIPSpots[licencePlateNumber].Vehicle;

            }
            if (ValueSpots.ContainsKey(licencePlateNumber))
            {
                vehicle = ValueSpots[licencePlateNumber].Vehicle;

            }
            if (RegularSpots.ContainsKey(licencePlateNumber))
            {
                vehicle = RegularSpots[licencePlateNumber].Vehicle;

            }

            return vehicle;

        }

        public bool IsAreaAvailbleToPark(string spotType)
        {
            var result = true;
            if (spotType.ToLower() == "vip")
            {
                if (VIPSpots.Count == 10)
                {
                    result = false;
                }
            }

            if(spotType.ToLower() == "value")
            {
                if(ValueSpots.Count == 20)
                {
                    result = false;
                }
            }

            if (spotType.ToLower() == "regular")
            {
                if(RegularSpots.Count == 30)
                {
                    result = false;
                }
            }

            return result;
        }

        public bool IsFull()
        {
            var result = false;
            if (VIPSpots.Count == 10 && ValueSpots.Count == 20 && RegularSpots.Count == 30)
            {
                result = true;
            }

            return result;
        }
    }
}
