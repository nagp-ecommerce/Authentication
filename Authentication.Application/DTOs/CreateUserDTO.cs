using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.DTOs
{
    public record CreateUserDTO
    (
        [Required(ErrorMessage ="Email is required"), 
        EmailAddress(ErrorMessage="Invalid Email Format")]
        string Email,

        [Required(ErrorMessage ="Password is required"), 
        MinLength(8, ErrorMessage="Password must be atleast 8 charachter long")]
        string Password,

        [Required(ErrorMessage ="Name is required")]
        string Name,

        [Required(ErrorMessage ="Phone is required"), 
        RegularExpression(@"^\d{10}$", ErrorMessage="Phone is 10 digits")]
        string Phone,

        string? Role
        // This will be subject to verification from admin account
        // in case admin role is selected
    );
}
