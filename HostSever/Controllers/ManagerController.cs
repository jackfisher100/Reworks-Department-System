using Microsoft.AspNetCore.Mvc;
using HostSever.Classes;
using System.Threading.Tasks;
using HostSever.Model;

namespace HostSever.Controllers
{
	[ApiController]
	[Route("Manager")]
	public class ManagerController : ControllerBase
	{
		private  DatabaseLink _databaseLink;

		public ManagerController(DatabaseLink database)
		{
			_databaseLink = database;
		}

		[HttpPost("updatePredicted")]
		public  IActionResult UpdatePredicted([FromBody] Car car)
		{
			if (car.Barcode == null || car.Prediction == null | car.ManagerID == null)
			{
				return BadRequest("Either Barcode, ManagerID or Prediction missing");
			}
			_databaseLink.UpdatePredicted(car.Barcode, car.ManagerID, car.Prediction.Value);
			return Ok();
		}


		[HttpGet("allUsers")]
		public IActionResult AllUsers()
		{
			List<User> result = _databaseLink.getAllUsers();

			return Ok(result);
		}

		[HttpPost("editUser")]
		public IActionResult EditUser([FromBody] User user)
		{
			if (user.BentleyID == null || user.NameOfUser == null || user.Manager == null)
			{
				return BadRequest("Make sure to include UserID, BentleyID, NameOfUser and Manager");
			}

			if (!_databaseLink.UpdateUser(user))
			{
				return BadRequest("BentleyID already exists");
			}

			return Ok();
		}

		[HttpPost("updateNotes")]
		public IActionResult UpdateNotes([FromBody] Car car)
		{
			if (car.Barcode == null || car.Issue == null)
			{
				return BadRequest("Either Barcode or Issue missing");
			}

			string oldNotes = _databaseLink.GetCarNotes(car.Barcode);
			string newNotes = oldNotes + "\n\n" + DateTime.Now.ToShortDateString() + "   ---   " + DateTime.Now.ToShortTimeString() + "\n" + car.Issue;

			_databaseLink.AddNotes(car.Barcode, newNotes);
			
			return Ok();
		}

		[HttpGet("login")]
		public IActionResult Login([FromQuery] string managerID)
		{
			if (managerID == null) return BadRequest("Make sure WorkerID is included");

			User? result = _databaseLink.Login(managerID, true);
			if (result != null)
			{
				return Ok(result);
			}
			return BadRequest("Login failed");
		}
	}
}
