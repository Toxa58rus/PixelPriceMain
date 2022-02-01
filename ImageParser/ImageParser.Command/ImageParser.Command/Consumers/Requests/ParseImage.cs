namespace ImageParserService.Command.Consumers.Requests
{
    public class ParseImage
    {
        public  string Name => "ParseImage";

       /* public  async Task<string> Execute(object jsonValue)
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
        }*/
    }
}
