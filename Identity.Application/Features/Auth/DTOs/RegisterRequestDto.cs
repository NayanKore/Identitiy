using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.DTOs
{
    public record RegisterRequestDto(
        string EmployeeId,
        string FullName,
        string Email,
        string Password,
        string Role);
}
