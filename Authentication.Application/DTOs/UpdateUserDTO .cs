using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.DTOs
{
    public record UpdateUserDTO
    (
         [Required(ErrorMessage ="Password is required"),
        MinLength(8, ErrorMessage="Password must be atleast 8 charachter long")]
        string Password,

        [Required(ErrorMessage ="Name is required")]
        string Name,

        [Required(ErrorMessage ="Phone is required"),
        RegularExpression(@"^\d{10}$", ErrorMessage="Phone is 10 digits")]
        string Phone
    );
}
