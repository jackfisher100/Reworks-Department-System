using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerTablet.Models;

public class User
{
    public int? UserID { get; set; }
    public string? BentleyID { get; set; }
    public string? NameOfUser { get; set; }
    public bool? Manager { get; set; }

    public bool? NotManager { get { return !Manager; } set { Manager = !value; } }


}

