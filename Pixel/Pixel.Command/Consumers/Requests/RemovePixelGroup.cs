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
           // Очень странно удаляется надо разобраться, надо не удалять дефолтные группы и если это не дефолит, то перенести все пиксели из удаляемой группы в дефолт
	        var request = context.Message;

	        var defaultGroupIdUser =
		        await _dbContext.PixelGroups.AsNoTracking().FirstAsync(x => x.UserId == request.UserId && x.IsDefault == true);

	        var removeGroup =
		        await _dbContext.PixelGroups.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.GroupId && x.IsDefault == false);

	        if (removeGroup == null)
	        {
		        await context.RespondAsync(new ResultWithError(
			        (int)HttpStatusCode.BadRequest, "Группа не найдена"));
	        }

            var listPixelForRemoveGroup = await _dbContext.Pixels.AsNoTracking().Where(x => x.GroupId == request.GroupId).ToListAsync();

	        if (listPixelForRemoveGroup.Count > 0)
	        {
		        _dbContext.Pixels.AttachRange(listPixelForRemoveGroup);

		        foreach (var item in listPixelForRemoveGroup)
		        {
			        item.GroupId = defaultGroupIdUser.Id;
			        _dbContext.Entry(item).Property(nameof(item.GroupId)).IsModified = true;
		        }

            }

            _dbContext.PixelGroups.Remove(removeGroup);

            await _dbContext.SaveChangesAsync();

            await context.RespondAsync(new ResultWithError(
	            (int)HttpStatusCode.OK, null));
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
