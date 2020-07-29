using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingManagement.Services;
using ParkingManagement.ViewModel;
using ParkingManagementClassLibrary;
using ParkingManagementClassLibrary.Ticket;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private ParkingService _parkingService { get; set; } 
        public ParkingController(ParkingService parkingService)
        {
            _parkingService = parkingService;
        }
        
        [HttpGet("GetVehiclesTypeList")]
        public ActionResult GetVehiclesTypeList()
        {
            var vehicleTypeList = _parkingService.GetAllVehiclesType();
            return Ok(_parkingService.GetAllVehiclesType());
        }


        [HttpGet("GetStatusOfParking")]
        public ActionResult GetStatusOfParking()
        {
            var listOfParkingSpots = _parkingService.GetStatusOfParking();
            return Ok(listOfParkingSpots);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var licensePlateNumber = id.ToString();
            var vehicle = _parkingService.GetVehicleByLicensePlateNumber(licensePlateNumber);
            return Ok(new { message = vehicle });
        }

        [HttpPost("PostNewParking")]
        public ActionResult PostNewParking(string name, string licenseNumber, string phone, string ticketType, 
            string carType, string height, string width, string length)
        {
            var answer = "";
            CheckinViewModel model = new CheckinViewModel
            {
                Name = name,
                LicensePlateNumber = licenseNumber,
                Phone = phone,
                TicketType = ticketType,
                CarType = carType,
                Height = height,
                Width = width,
                Length = length
            };
            if(_parkingService.CheckValidationOfRequest(model))
            {
                answer = _parkingService.AddVehicleToParking(model);
                return Ok(new {message= answer });
            }
            else
            {
                //store the data about the availble tickets
                var listOfAvailbleTicketes = _parkingService.CheckAvailbleTicketTypes(model);
                StringBuilder sb = new StringBuilder("Dimension not valid, ");
                foreach (var availbleTicket in listOfAvailbleTicketes)
                {
                    sb.Append("Availble ticket: " + availbleTicket.TicketTypeName + "- Cost difference is " + availbleTicket.CostDifference + "$" + Environment.NewLine);
                }
                answer = sb.ToString();
                return Ok(new { message = answer });
            }    
        }


        [HttpPost("DeleteParking")]
        public ActionResult DeleteParking(int id)
        {
            var answer = "Car Not found on the system";
            var licensePlateNumber = id.ToString();
            if (_parkingService.RemoveVehicleFromParking(licensePlateNumber))
            {
                answer = "Checkout Succesfully, Thanks!";
            }

            return Ok(answer);
        }
    }
}
