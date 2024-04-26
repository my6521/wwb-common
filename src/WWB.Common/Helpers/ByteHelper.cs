using System.Text.RegularExpressions;

namespace WWB.Common.Helpers
{
    public class ByteHelper
    {
        /// <summary>
        /// 将字节数组转换成int类型
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static int ByteToInt(byte[] buff, int start, int len)//将字节数组转换成int类型
        {
            int temp = 0;
            int n = 0;
            for (int i = 0; i < len; i++)
            {
                n <<= 8;
                temp = buff[start + i] & 0xff;
                n |= temp;
            }

            return n;
        }

        /// <summary>
        /// int类型转字节数组
        /// </summary>
        /// <param name="vel"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static byte[] IntToByte(int vel, int len)
        {
            int velocity = vel;
            byte[] hex = new byte[len];
            int n = 0;
            for (var i = 0; i < len; i++)
            {
                hex[i] = (byte)((velocity >> i * 8) & 0xff);
            }

            Array.Reverse(hex);

            return hex;
        }

        /// <summary>
        /// 字节数组转hex
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHex(byte[] bytes)
        {
            string str = string.Empty;
            foreach (byte Byte in bytes)
            {
                str += String.Format("{0:X2}", Byte);
            }
            return str.Trim();
        }

        public static byte[] HexToByte(string hex)
        {
            var ByteStrings = Regex.Split(hex, @"(?<=\G.{2})(?!$)");

            byte[] ByteOut;

            ByteOut = new byte[ByteStrings.Length];

            for (int i = 0; i <= ByteStrings.Length - 1; i++)
            {
                ByteOut[i] = Byte.Parse(ByteStrings[i], System.Globalization.NumberStyles.HexNumber);
            }

            return ByteOut;
        }
    }
}