using Identity.Application.Features.Auth.Commands;
using Identity.Application.Features.Common.Interfaces;
using Identity.Domain.Entities;
using Identity.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterCommandHandler(IUserRepository userRepo, IPasswordHasher passwordHasher)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check duplicates
            if (await _userRepo.ExistsByEmailAsync(request.Email, cancellationToken))
                throw new ValidationException("Email already exists.");
            if (await _userRepo.ExistsByEmployeeIdAsync(request.EmployeeId, cancellationToken))
                throw new ValidationException("EmployeeId already exists.");

            var user = new User
            {
                EmployeeId = request.EmployeeId,
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password),
                Role = request.Role,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            await _userRepo.AddAsync(user, cancellationToken);
            return Unit.Value;
        }
    }
}
