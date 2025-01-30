using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.DTOs
{
    public record CreateUserDTO
    (
        string Email,
        string Password,
        string Name,
        string Phone
    );
}
