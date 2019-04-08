using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BigHat
{
    internal static class Utils
    {
        public static byte[] ReadBinaryDataFromFile(string filename)
        {
            if (!File.Exists(filename))
                throw new ArgumentException();
            return File.ReadAllBytes(filename);
        }

        public static string ReadNullTerminatedWideString(IReadOnlyList<byte> data, int offset, int maxLength)
        {
            var charList = new List<byte>(maxLength);
            for (var i = 0; i < maxLength; i += 2)
            {
                if (offset + i + 1 >= data.Count)
                    break;
                var a = data[offset + i];
                var b = data[offset + i + 1];
                charList.Add(a);
                charList.Add(b);
                if (a == 0x0 & b == 0x0)
                    break;
            }
            return Encoding.Unicode.GetString(charList.ToArray());
        }
    }
}