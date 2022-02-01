using System.Threading.Tasks;
using Contracts.UserContract.UserEvent;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;

namespace ImageParserService.Command.Consumers.Events
{
  /*  public class CreateUser : IConsumer<CreateUserEvent>
    {
        private readonly MailDbContext _dbContext;

        public CreateUser(MailDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<CreateUserEvent> context)
        {
            _dbContext.Mail.Add(new Mail()
            {
                Id = NewId.NextGuid(),
                MailAddress = context.Message.MailAddress,
                UserId = context.Message.Userid
            });

            await _dbContext.SaveChangesAsync();

            return;
        }
    }

    public class CreateUserDefinition : ConsumerDefinition<CreateUser>
    {
        public CreateUserDefinition()
        {
            EndpointName = "CreateUserEventMail";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<CreateUser> consumerConfigurator)
        {

        }
    }*/
}
