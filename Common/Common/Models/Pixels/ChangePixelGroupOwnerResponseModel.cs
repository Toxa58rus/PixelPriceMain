using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Pixels
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
