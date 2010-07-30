using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvDbDotNet
{
    public class TvDbSeries : TvDbSeriesBase
    {
        public IEnumerable<TvDbEpisode> Episodes { get; set; }
        public string Network { get; set; }
        public IEnumerable<string> Actors { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public string Status { get; set; }
        public string Runtime { get; set; }
        public string Rating { get; set; }
        public string AirTime { get; set; }
        public string AirDay { get; set; }
        public string IMDbId { get; set; }
        public string Zap2ItId { get; set; }
    }
}
