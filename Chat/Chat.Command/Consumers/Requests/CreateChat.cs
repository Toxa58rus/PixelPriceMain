using System.Threading.Tasks;
using ChatService.Context;
using MassTransit;

namespace ChatService.Command.Consumers.Requests
{
    public class CreateChat : IConsumer<string>
    {
		private readonly ChatDbContext _dbContext;

		public CreateChat(ChatDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task Consume(ConsumeContext<string> context)
		{
			return Task.CompletedTask;
		}
    }
}
