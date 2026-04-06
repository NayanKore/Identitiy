using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Commands
{
    public record RegisterCommand(
        string EmployeeId,
        string FullName,
        string Email,
        string Password,
        string Role) : IRequest<Unit>;
}
