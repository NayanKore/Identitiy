using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.DTOs
{
    public record LoginResponseDto(string Token, string FullName, string Role, DateTime ExpiresAt);
}
