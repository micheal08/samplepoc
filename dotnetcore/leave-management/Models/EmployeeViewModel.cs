﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }

        [Display(Name ="User Name")]
        public string UserName { get; set; }

        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string TxtId { get; set; }

        [Display(Name = "DOB")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Date Joined")]
        public DateTime DateJoined { get; set; }
    }
}
