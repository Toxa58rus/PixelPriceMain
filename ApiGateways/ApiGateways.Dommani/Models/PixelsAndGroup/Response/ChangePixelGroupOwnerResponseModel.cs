using System;
using System.Collections.Generic;

namespace ApiGateways.Domain.Models.PixelsAndGroup.Response
{
    public class ChangePixelGroupOwnerResponseModel
    {
	    public ChangePixelGroupOwnerResponseModel(List<PixelGroup> groups, Guid userId)
	    {
		    Groups = groups;
		    UserId = userId;
	    }
        public List<PixelGroup> Groups { get; private set; }
        public Guid UserId { get; private set; }
    }
}
