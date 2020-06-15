using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Phân quyền cho user
    /// </summary>
    public class CommonConstants
    {
        //"MEMBER, ADMIN, MOD" trùng với ID trong database
        public static string MEMBER_GROUP = "MEMBER";
        public static string ADMIN_GROUP = "ADMIN";
        public static string MOD_GROUP = "MOD";
    }
}
