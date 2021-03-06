﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.ViewModels
{
    public class UserVM
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(20,MinimumLength =2,ErrorMessage ="Password must containt between 2 and 20 characters")]
        public string Password { get; set; }
        
        [Required]
        public string Gender { get; set; }
        [Required]
        public string KnownAs { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }

        public UserVM()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}
