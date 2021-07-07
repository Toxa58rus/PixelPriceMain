using System.Collections.Generic;

namespace Common.Models.Pixels
{
    public class ChangePixelsResponseModel
    {
        public List<Pixels> Pixels;
        public string GroupId { get; set; }
    }
}
