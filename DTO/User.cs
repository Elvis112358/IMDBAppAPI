﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class User
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
