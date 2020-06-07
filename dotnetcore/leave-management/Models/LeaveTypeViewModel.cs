﻿using System;
using System.ComponentModel.DataAnnotations;

namespace leave_management.Models
{

    public class LeaveTypeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Default Number of Days")]
        [Range(1,25, ErrorMessage = "Please enter valid number")]
        public int DefaultDays { get; set; }

        [Display(Name="Date Created")]
        public DateTime? DateCreated { get; set; }
    }
}
