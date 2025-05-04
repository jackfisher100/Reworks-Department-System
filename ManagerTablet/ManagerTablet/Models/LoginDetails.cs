using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerTablet.Models;
public class LoginDetails
{
    public int? UserID { get; set; }
    public string? BentleyID { get; set; }
    public string? NameOfUser { get; set; }
    public bool? Manager { get; set; }
    public int? BayNo { get; set; }
}
