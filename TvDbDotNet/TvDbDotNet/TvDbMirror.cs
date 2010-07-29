using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvDbDotNet
{
   public class TvDbMirror
    {
       public string Id { get; internal set; }
        public Uri Url { get; internal set; }
        public TvDbMirrorType Type { get; internal set; }
    }
}
