using Common.Extensions;
using Common.Models.ImageParser;
using Common.Models.Pixels;
using Common.Rcp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace ImageParser.Command
{
    public class ParceImagetoBitmapCommand : ServiceCommand
    {
        public override string Name => "ParceImageToBitmap";

        public override async Task<string> Execute(object jsonValue)
        {
            try
            {
                var imageData = JsonConvert.DeserializeObject<ImageData>(jsonValue.ToJson());
                var bytes = Convert.FromBase64String(imageData.ImageBaseString);

                await using (var memory = new MemoryStream(bytes))
                {
                    var bitmap = new Bitmap((Bitmap)Image.FromStream(memory));
                    bitmap = Scale(bitmap, imageData.XCount);

                    var converter = new ImageConverter();
                    var base64 = Convert.ToBase64String((byte[])converter.ConvertTo(bitmap, typeof(byte[])));
                    var result = new ImageData(base64, imageData.XCount, imageData.YCount);

                    return result.ToJson();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private Bitmap Scale(Bitmap bitmap, int maxX)
        {
            var newHeight = bitmap.Height / 1 * maxX / bitmap.Width;
            if (bitmap.Width > maxX || bitmap.Height > newHeight)
            {
                bitmap = new Bitmap(bitmap, new Size(maxX, (int)newHeight));
            }

            return bitmap;
        }
    }
}
