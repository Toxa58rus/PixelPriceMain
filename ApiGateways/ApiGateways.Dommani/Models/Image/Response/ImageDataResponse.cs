using System;

namespace ApiGateways.Domain.Models.Image.Response
{
    public class ImageDataResponse
    {
	    public string ImageBaseString { get; set; }
        public int XCount { get; set; }
        public int YCount { get; set; }
        public Guid GroupId { get; set; }
    }
}
