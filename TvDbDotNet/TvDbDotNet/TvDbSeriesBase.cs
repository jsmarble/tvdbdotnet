using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvDbDotNet
{
    public class TvDbSeriesBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public DateTime FirstAired { get; set; }
        public string Language { get; set; }
    }
}
