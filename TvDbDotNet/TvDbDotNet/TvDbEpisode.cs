using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvDbDotNet
{
    public class TvDbEpisode
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int Season { get; set; }
        public DateTime FirstAired { get; set; }
        public IEnumerable<string> GuestStars { get; set; }
        public string Writer { get; set; }
        public string Director { get; set; }
    }
}
