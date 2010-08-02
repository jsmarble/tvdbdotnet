using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvDbDotNet
{
    public class TvDbMirror
    {
        public string Id { get; set; }
        public Uri Url { get; set; }
        public TvDbMirrorType Type { get; set; }
    }
}
