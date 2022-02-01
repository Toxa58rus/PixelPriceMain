using ChatService.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatService.Context.ConfigEntity
{
    public class ChatMessageEntityTypeConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
	        builder.ToTable("ChatMessage");
	        
	        builder.HasKey(x => x.Id);

	        builder.Property(x => x.Id);
        }
    }
}
