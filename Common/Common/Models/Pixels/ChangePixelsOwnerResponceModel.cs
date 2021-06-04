using System.Collections.Generic;

namespace Common.Models.Pixels
{
    public class ChangePixelsOwnerResponceModel
    {
        public List<Pixels> Pixels;
        public string UserId { get; set; }
    }
}
