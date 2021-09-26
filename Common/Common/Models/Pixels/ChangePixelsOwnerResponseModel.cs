using System;
using System.Collections.Generic;

namespace Common.Models.Pixels
{
    public class ChangePixelsOwnerResponseModel
    {
        public List<Pixel> Pixels;
        public Guid UserId { get; set; }
    }
}
