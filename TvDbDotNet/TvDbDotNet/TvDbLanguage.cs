using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvDbDotNet
{
    public class TvDbLanguage
    {
        internal TvDbLanguage()
        {
        }

        internal TvDbLanguage(string abbreviation, string name)
        {
            this.Abbreviation = abbreviation;
            this.Name = name;
        }

        public string Id { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
    }
}
