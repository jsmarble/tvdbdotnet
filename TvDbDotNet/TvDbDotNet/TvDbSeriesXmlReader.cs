using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace TvDbDotNet
{
    public class TvDbSeriesXmlReader
    {
        public TvDbSeries Read(string xml)
        {
            TvDbSeries series = new TvDbSeries();
            using (StringReader xmlReader = new StringReader(xml))
            {
                XDocument xDoc = XDocument.Load(xmlReader);
                XElement seriesElement = xDoc.Element("Data").Element("Series");
                series.Id = seriesElement.Element("id").Value;
                series.Name = seriesElement.Element("SeriesName").Value;
                series.Language = seriesElement.Element("Language").Value;
                series.Network = seriesElement.Element("Network").Value;
                series.AirDay = seriesElement.Element("Airs_DayOfWeek").Value;
                series.AirTime = seriesElement.Element("Airs_Time").Value;
                series.Rating= seriesElement.Element("ContentRating").Value;
                series.Runtime = seriesElement.Element("Runtime").Value;
                series.Status = seriesElement.Element("Status").Value;
                series.Genres = seriesElement.Element("Genre").Value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                series.Actors = seriesElement.Element("Actors").Value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                series.IMDbId = seriesElement.Element("IMDB_ID").Value;
                series.Zap2ItId = seriesElement.Element("zap2it_id").Value;
            }
            TvDbEpisodeXmlReader episodeReader = new TvDbEpisodeXmlReader();
            series.Episodes = episodeReader.Read(xml);

            return series;
        }
    }
}
