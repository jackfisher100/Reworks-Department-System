using HostSever.Classes;
using HostSever.Model;
using Microsoft.AspNetCore.Mvc;

namespace HostSever.Controllers
{
	[ApiController]
	[Route("Bays")]
	public class BayController : ControllerBase
	{
		private readonly DatabaseLink _databaseLink;

		public BayController(DatabaseLink databaseLink)
		{
			_databaseLink = databaseLink;
		}

		[HttpGet("allbays")]
		public IActionResult AllBays()
		{
			List<Car> openCars = _databaseLink.GetAllOpenCars();
			List<int> baysWithAndonOn = _databaseLink.GetCurrentAndon();

			List<BayInfo> response = new List<BayInfo>();

            foreach (Car c in openCars)
            {
				BayInfo temp = new BayInfo(c);

				if (baysWithAndonOn.Contains(c.BayNo.Value)) temp.AndonOn = true;
				else temp.AndonOn = false;

				temp.BreakPeriod = _databaseLink.GetTotalBreak(c.BayNo.Value, c.Barcode);

				response.Add(temp);
			}

            return Ok(response);
		}

		[HttpGet("todaysStats")]
		public IActionResult TodaysStats()
		{
			int result = _databaseLink.GetTodaysCarsCount();
			if (result == -1)
			{
				Console.WriteLine("Error: /TodaysStats Issue with Database");
				return BadRequest("Something went wrong!");
			}
			return Ok(result);
		}


		[HttpPost("andonon")]
		public IActionResult AndonOn([FromBody] Car car)
		{
			if (car.BayNo == null) return BadRequest("BayNo is missing");

			if (car.WorkerID == null)
			{
				if (car.ManagerID == null) return BadRequest("BayNo is missing");
				_databaseLink.InsertBayInfo(car.BayNo.Value, car.ManagerID, true);
				return Ok();
			}

			_databaseLink.InsertBayInfo(car.BayNo.Value, car.WorkerID, true);
			return Ok();
		}

		[HttpPost("andonoff")]
		public IActionResult AndonOff([FromBody] Car car)
		{
			if (car.BayNo == null) return BadRequest("BayNo is missing");
			
			if (car.WorkerID == null)
			{
				if (car.ManagerID == null) return BadRequest("Either WorkerID or ManagerID is required");
				_databaseLink.InsertBayInfo(car.BayNo.Value, car.ManagerID, false);
				return Ok();
			}

			_databaseLink.InsertBayInfo(car.BayNo.Value, car.WorkerID, false);
			return Ok();
		}

		[HttpPost("breakOn")]
		public IActionResult BreakOn([FromBody] Car car)
		{
			if (car.BayNo == null || car.Barcode == null) return BadRequest("BayNo or Barcode is missing");

			if (car.WorkerID == null)
			{
				if (car.ManagerID == null) return BadRequest("Either WorkerID or ManagerID is required");
				_databaseLink.InsertBreakInfo(car.BayNo.Value, car.ManagerID, car.Barcode, true);
				return Ok();
			}

			_databaseLink.InsertBreakInfo(car.BayNo.Value, car.WorkerID, car.Barcode, true);
			return Ok();
		}

		[HttpPost("breakOff")]
		public IActionResult BreakOff([FromBody] Car car)
		{
			if (car.BayNo == null || car.Barcode == null) return BadRequest("BayNo or Barcode is missing");

			if (car.WorkerID == null)
			{
				if (car.ManagerID == null) return BadRequest("Either WorkerID or ManagerID is required");
				_databaseLink.InsertBreakInfo(car.BayNo.Value, car.ManagerID, car.Barcode, false);
				return Ok();
			}

			_databaseLink.InsertBreakInfo(car.BayNo.Value, car.WorkerID, car.Barcode, false);
			return Ok();
		}
	}
}
