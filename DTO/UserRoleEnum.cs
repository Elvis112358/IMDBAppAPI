using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    [Flags]
    public enum UserRole
    {
        //0, 1, 2, 4, 8, 16...
        None = 0,
        Admin = 1,
        Customer = 2,
        Test = 4
    }
}
