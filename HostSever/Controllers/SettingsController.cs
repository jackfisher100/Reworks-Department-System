using Microsoft.AspNetCore.Mvc;
using HostSever.Classes;
using System.Threading.Tasks;

namespace HostSever.Controllers
{
	[ApiController]
	[Route("Settings")]
	public class SettingsController : ControllerBase
	{
		private readonly DatabaseLink _databaseLink;

		public SettingsController(DatabaseLink db)
		{
			_databaseLink = db;
		}

		[HttpGet("getSettings")]
		public IActionResult GetSettings([FromQuery]string key)
		{
			string result = _databaseLink.GetSettings(key);
			if (!string.IsNullOrEmpty(result))
			{
				return Ok(result);
			}
			return BadRequest("Settings not found");
		}
	}
}