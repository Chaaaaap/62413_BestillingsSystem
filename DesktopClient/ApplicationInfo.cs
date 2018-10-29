using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace DesktopClient
{
    public static class ApplicationInfo
    {
        public static User CurrentUser { get; set; }
        public static string WebApiKey = "https://localhost:44310/api";
    }
}
