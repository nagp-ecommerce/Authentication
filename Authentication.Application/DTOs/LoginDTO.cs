using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.DTOs
{
    public record LoginDTO
    (
        [Required(ErrorMessage ="Email is required"),
        EmailAddress(ErrorMessage="Invalid Email Format")]
        string Email,
        string Password
    );
}
