using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Errors;
using Contracts.PixelContract.PixelRequest;
using Contracts.PixelContract.PixelResponse;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using PixelService.Context;
using PixelService.Context.Models;

namespace PixelService.Command.Consumers.Requests
{
    public class ChangeGroup : IConsumer<ChangeGroupRequestDto>
    {
        private readonly PixelContext _dbContext;

        public ChangeGroup(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ChangeGroupRequestDto> context)
        {
	        var request = context.Message;
	        try
	        {
		        var group = await _dbContext.PixelGroups.Where(s => s.Id == request.GroupId && s.IsDefault == false)
			        .AsNoTracking().FirstOrDefaultAsync();

				if (group != null)
				{

					group.Name = request.Name;
					group.Massage= request.Massage;
					group.UserId = request.UserId;

			        _dbContext.PixelGroups.Update(group);

					await _dbContext.SaveChangesAsync();

					await context.RespondAsync(new ResultWithError<ChangeGroupResponseDto>(
						(int)HttpStatusCode.OK,
						null,
						new ChangeGroupResponseDto()
						{
							GroupId = request.GroupId,
							UserId = request.UserId,
							Massage = request.Massage,
							Name = request.Name
						}));
				}
	        }
	        catch (Exception ex)
	        {
		         await context.RespondAsync(new ResultWithError((int)HttpStatusCode.BadRequest, null));
		         return;
	        }

	        await context.RespondAsync(new ResultWithError((int)HttpStatusCode.OK, null));
        }
    }

    public class ChangeGroupDefinition : ConsumerDefinition<ChangeGroup>
    {
        public ChangeGroupDefinition()
        {
            EndpointName = "ChangeGroupRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ChangeGroup> consumerConfigurator)
        {

        }
    }
}
