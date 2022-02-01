using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.PixelContract.PixelRequest;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using PixelService.Context;
using PixelService.Context.Models;

namespace PixelService.Command.Consumers.Requests
{
    public class ChangePixelGroupOwner : IConsumer<ChangePixelGroupOwnerRequest>
    {
        private readonly PixelContext _dbContext;

        public ChangePixelGroupOwner(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ChangePixelGroupOwnerRequest> context)
        {

            var value = context.Message;//<ChangePixelGroupOwnerRequest>();
            var result = new List<PixelGroup>();

            /*try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    foreach (var item in value.GroupIds)
                    {
                        if (item.IsDefault == false)
                        {
                            var group = await _dbContext.PixelGroups.FirstOrDefaultAsync(s => s.Id == item.Id);
                            var pixels = await _dbContext.Pixels.Where(s => s.GroupId == item.Id).ToListAsync();

                            foreach (var pixelItem in pixels)
                            {
                                pixelItem.UserId = value.UserId;
                                await _dbContext.SaveChangesAsync();
                            }


                            group.UserId = value.UserId;

                            await _dbContext.SaveChangesAsync();
                            result.Add(group);
                        }
                    }
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {

            }
            */
        }
    }
    public class ChangePixelGroupOwnerCommandDefinition : ConsumerDefinition<ChangePixelGroupOwner>
    {
        public ChangePixelGroupOwnerCommandDefinition()
        {
            EndpointName = "ChangePixelGroupOwnerRequest";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ChangePixelGroupOwner> consumerConfigurator)
        {

        }
    }
}
