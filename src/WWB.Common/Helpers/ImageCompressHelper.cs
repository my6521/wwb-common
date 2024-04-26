using System.Drawing;
using System.Drawing.Imaging;

namespace WWB.Common.Helpers
{
    public class ImageCompressHelper
    {
        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="srcFileStream"></param>
        /// <param name="flag">压缩质量（数字越小压缩率越高）1-100</param>
        /// <param name="size">压缩后图片的最大大小</param>
        /// <returns></returns>
        public static Stream CompressImage(Stream srcFileStream, int flag = 90, int size = 300)
        {
            //如果是第一次调用，原始图像的大小小于要压缩的大小，则直接复制文件，并且返回true
            if (srcFileStream.Length < size * 1024)
            {
                return srcFileStream;
            }

            Image iSource = Image.FromStream(srcFileStream);
            ImageFormat tFormat = iSource.RawFormat;
            int dHeight = iSource.Height / 2;
            int dWidth = iSource.Width / 2;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            using (Graphics g = Graphics.FromImage(ob))
            {
                g.Clear(Color.WhiteSmoke);
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width,
                    iSource.Height, GraphicsUnit.Pixel);
            }

            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag; //设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;

            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }

                var outStream = new MemoryStream { Position = 0 };
                ob.Save(outStream, jpegICIinfo, ep);
                outStream.Position = 0;

                if (jpegICIinfo != null)
                {
                    if (outStream.Length > 1024 * size)
                    {
                        flag = flag - 10;
                        return CompressImage(outStream, flag, size);
                    }
                    else
                    {
                        return outStream;
                    }
                }
                else
                {
                    return outStream;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }
    }
}