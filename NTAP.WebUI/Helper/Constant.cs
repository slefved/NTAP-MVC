using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NTAP.WebUI.Models;
using Newtonsoft.Json;

namespace NTAP.WebUI.Helper
{
    public static class Constant
    {
        public const int SYSTEM = 0;
    }

    public static class WebHelper
    {
        public static string GetApprovalUsername(string p_json)
        {
            User user = JsonConvert.DeserializeObject<User>(p_json);
            return user?.UserName;
        }
        public static string GetApprovalFullName(string p_json)
        {
            User user = JsonConvert.DeserializeObject<User>(p_json);
            return user?.FullName;
        }
    }
}