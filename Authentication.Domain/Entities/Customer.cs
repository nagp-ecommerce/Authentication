﻿using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Authentication.Domain.Entities;

namespace Authentication.Domain
{
    public class Customer: User
    {
        [Required]
        public Account Account { get; set; }
        //public Order order { get; set; }
    }
}
