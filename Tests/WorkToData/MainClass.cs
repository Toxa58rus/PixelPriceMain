using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiGateways.Context;
using Microsoft.EntityFrameworkCore;
using PixelService.Context;
using PixelService.Context.Models;

class MainClass
{
	static void Main()
	{
		var color = new PixelColor()
		{
			Color = 1,
			Id = Guid.NewGuid()
		};

		var group = new PixelGroup()
		{
			Id = Guid.NewGuid(),
			Name = "Default",
			IsDefault = true,
			UserId = Guid.Empty
		};

		var pixel = new List<Pixel>();

		for (int x = 0, y = 0, i = 0; i <= 100; i++)
		{
			if (x > 9)
			{
				x = 0;
				y++;
			}

			pixel.Add(new Pixel()
				{
					Id= Guid.NewGuid(),
					GroupId= group.Id,
					UserId= Guid.Empty,
					X = x,
					Y = y,
					Color=color.Color
					//ColorId=color.Id
				}
			);
			x++;
		}

		var pixelContext = new PixelContext();
		//pixelContext.Database.EnsureDeleted();
		pixelContext.Database.EnsureCreated();
		//pixelContext.PixelColors.Add(color);
		pixelContext.Pixels.AddRange(pixel);
		pixelContext.PixelGroups.Add(group);
		pixelContext.SaveChanges();

		var apiGatewaysDbContext = new ApiGatewaysDbContext();

		
	}
}

