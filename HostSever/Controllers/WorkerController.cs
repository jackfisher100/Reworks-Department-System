using Microsoft.AspNetCore.Mvc;
using HostSever.Classes;
using System.Threading.Tasks;
using HostSever.Model;

namespace HostSever.Controllers
{
	[ApiController]
	[Route("worker")]
	public class WorkerController : ControllerBase
	{
		private readonly DatabaseLink _databaseLink;

		public WorkerController(DatabaseLink db)
		{
			_databaseLink = db;
		}


		[HttpPost("checkinCar")]
		public  IActionResult CheckinCar([FromBody] Car car)
		{
			if (car.Barcode == null || car.BayNo == null || car.WorkerID == null)
			{
				return BadRequest("Make sure to include Barcode and Dwell");
			}

			_databaseLink.CheckinCar(car.Barcode, car.BayNo.Value, car.WorkerID);
			return Ok();

		}

		[HttpGet("getPredicted")]
		public IActionResult GetPredicted([FromQuery] string Barcode)
		{
			if (Barcode == null) return BadRequest("Make sure Barcode is included");
			int? result = _databaseLink.getPredicted(Barcode);
			if (result == null)
			{
				return BadRequest("Error: Barcode not found");
			}
			return Ok(result);
		}

		[HttpPost("checkoutCar")]
		public  async Task<IActionResult> CheckoutCar([FromBody] Car car)
		{
			using (StreamReader reader = new StreamReader(HttpContext.Request.Body))
			{
				string body = await reader.ReadToEndAsync();
				Console.WriteLine(body);
			}

			if (car.Barcode == null || car.Dwell == null)
			{
				return BadRequest("Make sure to include Barcode and Dwell");
			}

			if (!_databaseLink.CheckoutCar(car.Barcode, car.Dwell.Value))
			{
				return BadRequest("You are trying to check in a car that is not yet checked in. VIN: " + car.Barcode);
			}

			return Ok();
		}

		[HttpGet("login")]
		public IActionResult Login([FromQuery] string workerID)
		{
			if (workerID == null) return BadRequest("Make sure WorkerID is included");

			User? result = _databaseLink.Login(workerID, false);
			if (result != null)
			{
				return Ok(result);
			}
			return BadRequest("Login failed");
		}
	}
}
