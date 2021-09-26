using System;
using System.Collections.Generic;

namespace Common.Models.Pixels
{
    public class ChangePixelsResponseModel
    {
        public List<Pixel> Pixels;
        public Guid GroupId { get; set; }
    }
}
