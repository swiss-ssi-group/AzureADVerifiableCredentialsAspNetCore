using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NationalDrivingLicense.Services
{
    public class StringContentWithoutCharset : StringContent
    {
        public StringContentWithoutCharset(string content) : base(content)
        {
        }

        public StringContentWithoutCharset(string content, Encoding encoding) : base(content, encoding)
        {
            Headers.ContentType.CharSet = "";
        }

        public StringContentWithoutCharset(string content, Encoding encoding, string mediaType) : base(content, encoding, mediaType)
        {
            Headers.ContentType.CharSet = "";
        }

        public StringContentWithoutCharset(string content, string mediaType) : base(content, Encoding.UTF8, mediaType)
        {
            Headers.ContentType.CharSet = "";
        }
    }
}
