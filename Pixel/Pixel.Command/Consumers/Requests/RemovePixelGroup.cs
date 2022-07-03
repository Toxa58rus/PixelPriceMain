using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Errors;
using Contracts.PixelContract.PixelRequest;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using PixelService.Context;

namespace PixelService.Command.Consumers.Requests
{
    public class RemovePixelGroup : IConsumer<RemovePixelGroupRequestDto>
    {
        private readonly PixelContext _dbContext;

        public RemovePixelGroup(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<RemovePixelGroupRequestDto> context)
        {
	        var request = context.Message;

	        var defaultGroupIdUser =
		        await _dbContext.PixelGroups.AsNoTracking().FirstAsync(x => x.UserId == request.UserId && x.IsDefault == true);

	        var listPixelForRemoveGroup = await _dbContext.Pixels.AsNoTracking().Where(x => x.GroupId == request.GroupId).ToListAsync();

	        _dbContext.Pixels.AttachRange(listPixelForRemoveGroup);

            foreach (var item in listPixelForRemoveGroup)
	        {
		        item.GroupId = defaultGroupIdUser.Id;
		        _dbContext.Entry(item).Property(nameof(item.GroupId)).IsModified = true;
	        }

            var removeGroup=
	            await _dbContext.PixelGroups.AsNoTracking().FirstAsync(x => x.UserId == request.UserId && x.IsDefault == true);

            _dbContext.PixelGroups.Remove(removeGroup);

            await _dbContext.SaveChangesAsync();

            await context.RespondAsync(new ResultWithError(
	            (int)HttpStatusCode.BadRequest,
	            "Ошибка изменения цвета пикселей"));
        }

    }
    public class RemovePixelGroupDefinition : ConsumerDefinition<RemovePixelGroup>
    {
        public RemovePixelGroupDefinition()
        {
            EndpointName = "RemovePixelGroupRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<RemovePixelGroup> consumerConfigurator)
        {

        }
    }
}
