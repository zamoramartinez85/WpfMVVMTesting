﻿using System;

namespace WpfMVVMTesting.Models
{
    public class Friend
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public bool IsDeveloper { get; set; }
    }
}
