using System;
using System.Linq;

namespace TASHTIP.EF.UsersRolePolicy
{
    /// <summary>
    /// The two account kinds in Tashtip: the back-office team (Admin) and
    /// everyone who books/tracks a finishing request (Customer).
    /// </summary>
    public enum Roles
    {
        Admin,
        Customer
    }
}
