using System;

namespace ParkingManagementClassLibrary
{
    public class Vehicle
    {
        public enum eClass
        {
            A,
            B,
            C
        }

        public eTypeVehicle eCarType { get; set; }

        public string LicenseNumber { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public double Height { get; set; }
               
        public double Width { get; set; }
               
        public double Length { get; set; }

        public Vehicle(string name, string licenseNumber,string phone, eTypeVehicle ecarType, double height, double width, double length)
        {
            Name = name;
            LicenseNumber = licenseNumber;
            Phone = phone;
            eCarType = ecarType;
            Height = height;
            Width = width;
            Length = length;
        }
    }
}
