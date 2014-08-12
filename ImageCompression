using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace Helpers
{
    /// <summary>
    /// Helper methods to aid in image compression, conversion, etc...
    /// </summary>
    public static class ImageHelpers
    {
        /// <summary>
        /// Compresses an image from a byte array with the given compression rate
        /// </summary>
        /// <param name="image">Byte array of the image to be compressed</param>
        /// <param name="compressionRate">A Long value ranging from 0 to 100 of how much to compress the image</param>
        /// <returns></returns>
        public static byte[] CompressImage(byte[] image, long compressionRate = 10L)
        {
            using (Image preImage = Image.FromStream(new MemoryStream(image)))
            {
                return CompressImage(preImage, compressionRate);
            }
        }

        public static byte[] CompressImage(MemoryStream stream, long compressionRate = 10L)
        {
            using (Image preImage = Image.FromStream(stream))
            {
                return CompressImage(preImage, compressionRate);
            }
        }

        private static byte[] CompressImage(Image preImage, long compressionRate)
        {
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, compressionRate);

            ImageCodecInfo jpegCodec = null;
            foreach (var codec in ImageCodecInfo.GetImageEncoders())
            {
                if (codec.FormatID == ImageFormat.Jpeg.Guid)
                {
                    jpegCodec = codec;
                    break;
                }
            }

            using (MemoryStream jpegStream = new MemoryStream())
            {
                preImage.Save(jpegStream, jpegCodec, encoderParams);
                var imageArray = ConvertStreamToByteArray(jpegStream);
                return imageArray;
            }
        }

        /// <summary>
        /// Converts the inputed stream into a byte array 
        /// </summary>
        /// <param name="stream">The sequence of bytes to be converted into a byte array</param>
        /// <returns>The byte array converted from the inputed stream</returns>
        public static byte[] ConvertStreamToByteArray(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.Position = 0;
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
