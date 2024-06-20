using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeploKor.Model
{
    public class CurrentUser
    {
        public int UserId { get; set; }
        public static bool IsClient { get; set; } = false;
        public static bool IsAdmin { get; set; } = false;
        public static bool IsEmployee { get; set; } = false;
    }
}
