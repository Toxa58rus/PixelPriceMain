using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Pixels
{
    public class ChangePixelColorResponseModel
    {
	    public ChangePixelColorResponseModel(List<Pixel> pixels,int color)
	    {
		    Pixels = pixels;
		    Color = color;
	    }

        public List<Pixel> Pixels { get; private set; }
        public int Color { get; private set; }
    }
}
