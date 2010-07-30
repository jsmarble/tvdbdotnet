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
            series.Overview = xmlElement.Element("Overview").Value;

            DateTime firstAired;
            if (DateTime.TryParse(xmlElement.Element("FirstAired").Value, out firstAired))
                series.FirstAired = firstAired;

            return series;
        }
    }
}
