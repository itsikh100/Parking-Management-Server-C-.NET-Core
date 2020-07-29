using Microsoft.AspNetCore.Mvc;
using ParkingManagement.ViewModel;
using ParkingManagementClassLibrary;
using ParkingManagementClassLibrary.ParkingSpots;
using ParkingManagementClassLibrary.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingManagement.Services
{
    public class ParkingService
    {
        private List<ParkingTicket> ticketList = new List<ParkingTicket>();
        public Parking parking { get; set; }
        public ParkingService()
        {
            parking = Parking.GetInstance;
            VIPTicket vip = new VIPTicket();
            RegularTicket regular = new RegularTicket();
            ValueTicket value = new ValueTicket();

            ticketList.Add(vip);
            ticketList.Add(regular);
            ticketList.Add(value);
        }

        public List<string> GetAllVehiclesType()
        {
            return parking.GetAllVehiclesType();
        }

        private bool CheckDimentions(ParkingTicket ticket, double height, double width, double length)
        {
            bool value = false;
            if (height < ticket.MaxHeight && width < ticket.MaxWidth && length < ticket.MaxLength)
            {
                value = true;
            }

            return value;
        }

        public Dictionary<int, string> GetStatusOfParking()
        {
            return parking.GetStatusOfParking();
        }

        public List<ParkingTicket> AvailableTicketTypes(double height, double width, double length)
        {
            List<ParkingTicket> returnList = new List<ParkingTicket>();
            foreach (var ticket in ticketList)
            {
                if (height < ticket.MaxHeight && width < ticket.MaxWidth && length < ticket.MaxLength)
                {
                    returnList.Add(ticket);
                }
            }
            return returnList;
        }

        public string AddVehicleToParking(CheckinViewModel model)
        {
            var answerFromManager ="";
            var ticket = GetTicketTypeByString(model.TicketType);
            var isNotOcupied = parking.IsAreaAvailbleToPark(model.TicketType);
            if (isNotOcupied)
            {
                answerFromManager = parking.AddNewVehicleToParkingSpot(model.Name, model.LicensePlateNumber, model.Phone, model.TicketType, 
                    model.CarType, model.Height, model.Width, model.Length);
            }

            return answerFromManager;
        }

        public List<AvailbleTicketModel> CheckAvailbleTicketTypes(CheckinViewModel model)
        {
            List<ParkingTicket> availableTicketList = new List<ParkingTicket>();
            List<AvailbleTicketModel> answer = new List<AvailbleTicketModel>();
            try
            {
                var height = Convert.ToDouble(model.Height);
                var width = Convert.ToDouble(model.Width);
                var length = Convert.ToDouble(model.Length);

                availableTicketList = AvailableTicketTypes(height, width, length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }

            var ticket = GetTicketTypeByString(model.TicketType);
            foreach (var availableTicket in availableTicketList)
            {
                answer.Add(new AvailbleTicketModel
                {
                    TicketTypeName = availableTicket.ToString(),
                    CostDifference = availableTicket.Cost - ticket.Cost
                });
            }

            return answer;
        }

        public bool CheckValidationOfRequest(CheckinViewModel model)
        {
            bool returnValue = false;
            try
            {
                var height = Convert.ToDouble(model.Height);
                var width = Convert.ToDouble(model.Width);
                var length = Convert.ToDouble(model.Length);

                var ticket = GetTicketTypeByString(model.TicketType);

                if (CheckDimentions(ticket, height, width, length))
                {
                    returnValue = true;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.StackTrace);
            }

            return returnValue;
        }

        private ParkingTicket GetTicketTypeByString(string ticketType)
        {
            ParkingTicket parkingTicket = new ParkingTicket();
            foreach (var ticket in ticketList)
            {
                if (ticket.ToString().ToLower() == ticketType.ToLower())
                {
                    parkingTicket = ticket;
                }
            }

            return parkingTicket;
        }

        public Vehicle GetVehicleByLicensePlateNumber(string licensePlateNumber)
        {
            return parking.GetVehicleByLicensePlateNumber(licensePlateNumber);
        }

        public bool RemoveVehicleFromParking(string licensePlateNumber)
        {
            return parking.RemoveVehicleFromParking(licensePlateNumber);
        }

       
    }
}
