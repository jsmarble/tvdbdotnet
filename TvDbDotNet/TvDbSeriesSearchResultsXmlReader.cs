using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TvDbDotNet
{
    public class TvDbSeriesSearchResultsXmlReader : TvDbXmlReader<TvDbSeriesBase>
    {
        public TvDbSeriesSearchResultsXmlReader()
            : base("Series")
        {
        }

        protected override TvDbSeriesBase ReadElement(XElement xmlElement)
        {
            TvDbSeriesBase series = new TvDbSeriesBase();
            series.Id = xmlElement.Element("id").Value;
            series.Name = xmlElement.Element("SeriesName").Value;
            series.Language = xmlElement.Element("language").Value;
            var overviewElement = xmlElement.Element("Overview");
            if (overviewElement != null)
                series.Overview = overviewElement.Value;

            var firstAiredElement = xmlElement.Element("FirstAired");
            if (firstAiredElement != null)
            {
                DateTime firstAired;
                if (DateTime.TryParse(firstAiredElement.Value, out firstAired))
                    series.FirstAired = firstAired;
            }

            return series;
        }
    }
}
