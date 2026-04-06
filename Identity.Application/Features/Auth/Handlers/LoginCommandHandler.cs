using Identity.Application.Features.Auth.Commands;
using Identity.Application.Features.Auth.DTOs;
using Identity.Application.Features.Common.Exceptions;
using Identity.Application.Features.Common.Interfaces;
using Identity.Domain.Interfaces;
using Identity.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;

        public LoginCommandHandler(IUserRepository userRepo, IJwtService jwtService, IPasswordHasher passwordHasher)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email, cancellationToken);
            if (user == null || !user.IsActive)
                throw new UnauthorizedException("Invalid credentials.");

            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid credentials.");

            var token = _jwtService.GenerateToken(user);
            return new LoginResponseDto(token, user.FullName, user.Role, DateTime.UtcNow.AddHours(1));
        }
    }
}
