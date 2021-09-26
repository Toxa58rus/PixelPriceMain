using Common.Extensions;
using Contracts.PixelContract.PixelRequest;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.EntityFrameworkCore;
using PixelService.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelService.Command.Consumers.Command
{
    public class ChangePixelGroupOwnerCommand : IConsumer<ChangePixelGroupOwnerRequest>
    {
        private readonly PixelContext _dbContext;

        public ChangePixelGroupOwnerCommand(PixelContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<ChangePixelGroupOwnerRequest> context)
        {

            var value = context.ToString().DeserializeToObject<ChangePixelGroupOwnerRequest>();
            var result = new List<PixelGroup>();

            try
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    foreach (var item in value.Groups)
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
        }
    }
    public class ChangePixelGroupOwnerCommandDefinition : ConsumerDefinition<ChangePixelGroupOwnerCommand>
    {
        public ChangePixelGroupOwnerCommandDefinition()
        {
            EndpointName = "ChangePixelGroupOwnerCommand";
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<ChangePixelGroupOwnerCommand> consumerConfigurator)
        {

        }
    }
}
