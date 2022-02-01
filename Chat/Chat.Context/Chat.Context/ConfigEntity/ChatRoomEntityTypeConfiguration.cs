using ChatService.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Context.ConfigEntity
{
    public class ChatRoomEntityTypeConfiguration : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
	        builder.ToTable("ChatRoom");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);

        }
    }
}
