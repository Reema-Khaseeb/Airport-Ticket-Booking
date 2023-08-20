using Airport_Ticket_Booking.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport_Ticket_Booking.Utilities
{
    public static class RoleAuthorization
    {
        public static void CheckPermission(UserRole userRole, UserRole requiredRole)
        {
            if (!CanUserPerformAction(userRole, requiredRole))
            {
                throw new UnauthorizedAccessException($"Permission denied. Required role: {requiredRole}");
            }
        }

        private static bool CanUserPerformAction(UserRole userRole, UserRole requiredRole)
        {
            return userRole == requiredRole;
        }
    }

}