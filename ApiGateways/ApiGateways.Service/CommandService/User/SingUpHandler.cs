using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApiGateways.Context;
using ApiGateways.Domain;
using ApiGateways.Domain.Command.User;
using ApiGateways.Domain.Services.Pixels;
using Contracts.UserContract.UserEvent;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ApiGateways.Service.CommandService.User
{
    public class SingUpHandler : HandlerBase<SingUpCommand, string>
    {
        private readonly ApiGatewaysDbContext _context;
        private readonly IMd5Hash _md5Hash;
        private readonly ILogger<SingUpHandler> _logger;
        private readonly IPublishEndpoint _publish;
        private readonly IPixelAndGroupService _pixelCommandService;

        public SingUpHandler(
            ApiGatewaysDbContext context,
            IMd5Hash md5Hash,
            IPixelAndGroupService pixelCommandService,
            ILogger<SingUpHandler> logger,
            IPublishEndpoint publish)
        {
            _context = context;
            _md5Hash = md5Hash;
            _logger = logger;
            _publish = publish;
            _pixelCommandService = pixelCommandService;
            
        }

        protected override async Task<string> Execute(SingUpCommand request, CancellationToken cancellationToken)
        {
			if (request.Password != request.ConfirmPassword) 
				return null;
			
			if (_context.Users.AsNoTracking().Any(s => s.Email.Equals(request.Email))) 
				return null;

			//using (var tr = await _context.Database.BeginTransactionAsync(cancellationToken))
			{
				var userData = CreateUserData(request);
				await _context.Users.AddAsync(userData, cancellationToken);
				await _context.SaveChangesAsync(cancellationToken);

				//await tr.CommitAsync(cancellationToken);

				//await _publish.Publish(new CreateUserEvent { Userid = Guid.Parse(userData.Id), MailAddress = userData.Email }, cancellationToken);

				_logger.LogInformation($"user: {userData.Id}, {userData.Email} has registered");

				return "Ok";
			}
		}

        private Context.Models.User CreateUserData(SingUpCommand userData) =>
            new Context.Models.User()
            {
                Id = NewId.NextGuid().ToString(),
                Email = userData.Email.Contains("@") ? userData.Email : string.Empty,
                PasswordHash = _md5Hash.GetMd5Hash(userData.Password),
                UserName = userData.UserName,
                Active = true
            };
    }
}
