using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvDbDotNet
{
    [Flags]
    public enum TvDbMirrorType
    {
        Xml = 1,
        Banners = 2,
        Zip = 4
    }
}
