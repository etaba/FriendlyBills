using FriendlyBills.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendlyBills
{
    public class Utilities
    {
        public static string FullName(ApplicationUser user)
        {
            return String.Format("{0} {1}", user.FirstName, user.LastName);
        }
    }
}