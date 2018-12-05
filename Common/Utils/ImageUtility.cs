using System;
using System.Drawing;
using System.IO;

namespace Common.Utils
{
    class ImageUtility
    {
        public Image ConvertByteArrayToImage(byte[] imageBytes)
        {
            Image image = null;
            try
            {
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                image = Image.FromStream(ms, true);
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e);
            }

            return image;
        }
    }
}
