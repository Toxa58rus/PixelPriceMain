using System.Collections.Generic;

namespace Common.Models.Pixels
{
    public class ChangePixelsOwnerResponseModel
    {
        public List<Pixels> Pixels;
        public string UserId { get; set; }
    }
}
