using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.UserContract.UserRequest;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UserService.Context;
using UserService.Context.Models;
using UserService.Domain;

namespace UserService.BL
{
    public class SignUpHandler : IConsumer<SignUpUserDataRequestDto>
    {
		private readonly UserDbContext _context;
		private readonly IConfiguration _configuration;
		private readonly IMd5Hash _md5Hash;
		private readonly ILogger<SignUpHandler> _logger;

		public SignUpHandler(
	        UserDbContext context,
	        IMd5Hash md5Hash,
	        IConfiguration configuration,
	        ILogger<SignUpHandler> logger)
        {
	        _context = context;
	        _md5Hash = md5Hash;
	        _configuration = configuration;
	        _logger = logger;

        }

        private User CreateUserData(SignUpUserDataRequestDto userData) =>
            new User()
            {
                Id = NewId.NextGuid().ToString(),
                Email = userData.Login.Contains("@") ? userData.Login : string.Empty,
                PasswordHash = _md5Hash.GetMd5Hash(userData.Password),
                UserName = string.Empty,
                Active = true
            };

        public async Task Consume(ConsumeContext<SignUpUserDataRequestDto> context)
        {
	        if (context.Message.Password != context.Message.ConfirmPassword)
		        return ;

	        if (_context.Users.AsNoTracking().Any(s => s.Email.Equals(context.Message.Login)))
		        return ;

	        await using var tr = await _context.Database.BeginTransactionAsync();
	        
	        var userData = CreateUserData(context.Message);
	        await _context.Users.AddAsync(userData);
	        await _context.SaveChangesAsync();

	        await tr.CommitAsync();

	        //await _publish.Publish(new CreateUserEvent { Userid = Guid.Parse(userData.Id), MailAddress = userData.Email }, cancellationToken);

	        _logger.LogInformation($"user: {userData.Id}, {userData.Email} has registered");

	        return ;
        }
    }
}
