using ApiGateways.Common.Models.User;
using ApiGateways.Context;
using ApiGateways.Domman.Command;
using ApiGateways.Service.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateways.Domman.Handler
{
    public class SingUpCommandHandler : IRequestHandler<SingUpCommand, Users>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMd5Hash _md5Hash;

        public SingUpCommandHandler(ApplicationDbContext context, IMd5Hash md5Hash)
        {
            _context = context;
            _md5Hash = md5Hash;
        }

        public async Task<Users> Handle(SingUpCommand request, CancellationToken cancellationToken)
        {

            if (request.Password != request.ConfirmPassword) return null;
            if (_context.Users.AsNoTracking().Any(s => s.Email.Equals(request.Email))) return null;

            using (var tr = await _context.Database.BeginTransactionAsync(cancellationToken))
            {
                var userData = CreateUserData(request);
                await _context.Users.AddAsync(userData, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await tr.CommitAsync(cancellationToken);

                return userData;
            }
        }

        private Users CreateUserData(SingUpCommand userData) =>
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = userData.Email.Contains("@") ? userData.Email : string.Empty,
                PasswordHash = _md5Hash.GetMd5Hash(userData.Password),
                UserName = userData.UserName,
                Active = true
            };
    }
}
