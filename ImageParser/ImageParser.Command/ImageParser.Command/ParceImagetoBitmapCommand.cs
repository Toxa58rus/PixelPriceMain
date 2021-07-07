using Common.Extensions;
using Common.Models.ImageParser;
using Common.Models.Pixels;
using Common.Rcp;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImageParser.Command
{
    public class ParseImageCommand : ServiceCommand
    {
        public override string Name => "ParseImage";

        public override async Task<string> Execute(object jsonValue)
        {
            try
            {
                var imageData = JsonConvert.DeserializeObject<ImageData>(jsonValue.ToJson());
                var bytes = Convert.FromBase64String(imageData.ImageBaseString);

                await using (var memory = new MemoryStream(bytes))
                {
                    using (var image = Image.Load(memory, out var format))
                    {
                        var options = new ResizeOptions();
                        
                        options.Size = new Size(imageData.XCount, imageData.YCount);
                        options.Compand = false;
                        options.Mode = ResizeMode.BoxPad;
                        
                        image.Mutate(x => x.Resize(options));
                        var base64 = image.ToBase64String(format);

                        var result = new ImageData(base64, imageData.XCount, imageData.YCount);

                        return result.ToJson();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
