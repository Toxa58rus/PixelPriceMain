namespace ApiGateways.Domain.Models.ImageParser
{
    public class ImageData
    {
        public ImageData()
        {
        }

        public ImageData(string imageBaseString, int xCount, int yCount)
        {
            ImageBaseString = imageBaseString;
            XCount = xCount;
            YCount = yCount;
        }

        public string ImageBaseString { get; set; }
        public int XCount { get; set; }
        public int YCount { get; set; }
    }
}
