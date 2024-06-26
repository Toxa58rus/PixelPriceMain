﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Errors;
using Contracts.PixelContract.PixelRequest;
using Contracts.PixelContract.PixelResponse;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using PixelService.Context;
using PixelService.Context.Models;

namespace PixelService.Command.Consumers.Requests
{
    public class CreatePixelGroup : IConsumer<CreatePixelGroupRequestDto>
    {
        private readonly PixelContext _dbContext;

        public CreatePixelGroup(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<CreatePixelGroupRequestDto> context)
        {
	        var value = context.Message;

	        var isExist = _dbContext.PixelGroups.Any(x => x.Name == value.Name);

	        if (isExist)
	        {
		        await context.RespondAsync(new ResultWithError<CreatePixelGroupResponseDto>(
			        (int)HttpStatusCode.BadRequest,
			        "Имя группы уже существует",
					null
		        ));
			}

	        var newGroup = new PixelGroup()
	        {
		        Id = NewId.NextGuid(),
		        IsDefault = false,
		        Name = value.Name,
				UserId = value.UserId
	        };

	        try
	        {
		        await _dbContext.PixelGroups.AddAsync(newGroup);
		        await _dbContext.SaveChangesAsync();
	        }
	        catch (Exception e)
	        {
				await context.RespondAsync(new ResultWithError<CreatePixelGroupResponseDto>(
					(int)HttpStatusCode.BadRequest,
					"Ошибка создания группы",
					null)
				);
			}

	        await context.RespondAsync(new ResultWithError<CreatePixelGroupResponseDto>(
	            (int)HttpStatusCode.OK,
	            null,
	            new CreatePixelGroupResponseDto()
	            {
		            GroupId = newGroup.Id
	            })
            );
        }
    }
    public class CreatePixelGroupDefinition : ConsumerDefinition<CreatePixelGroup>
    {
        public CreatePixelGroupDefinition()
        {
            EndpointName = "CreatePixelGroupRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<CreatePixelGroup> consumerConfigurator)
        {
        }
    }
}
