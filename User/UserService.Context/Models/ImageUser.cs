using System;

namespace UserService.Context.Models
{
    public class ImageUser
    {
        public Guid Id { get; set; }

        public byte[] Image { get; set; }

	    public string UserId { get; set; } 
        public virtual User User { get; set; }
    }
}
