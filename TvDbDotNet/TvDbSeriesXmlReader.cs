#region License
/*
Copyright © 2010, The TvDbDotNet Project
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright
notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright
notice, this list of conditions and the following disclaimer in the
documentation and/or other materials provided with the distribution.

3. Neither the name of the nor the
names of its contributors may be used to endorse or promote products
derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS “AS IS” AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace TvDbDotNet
{
    /// <summary>
    /// Provides a translation from xml to TvDbSeries.
    /// </summary>
    public class TvDbSeriesXmlReader
    {
        /// <summary>
        /// Initializes a new instance of the TvDbSeriesXmlReader class.
        /// </summary>
        public TvDbSeriesXmlReader()
        {
        }

        /// <summary>
        /// Translates xml into a TvDbSeries object.
        /// </summary>
        /// <param name="xml">The xml to translate.</param>
        /// <returns>a TvDbSeries object.</returns>
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

                var overviewElement = seriesElement.Element("Overview");
                if (overviewElement != null)
                    series.Overview = overviewElement.Value;

                var firstAiredElement = seriesElement.Element("FirstAired");
                if (firstAiredElement != null)
                {
                    DateTime firstAired;
                    if (DateTime.TryParse(firstAiredElement.Value, out firstAired))
                        series.FirstAired = firstAired;
                }
            }
            TvDbEpisodeXmlReader episodeReader = new TvDbEpisodeXmlReader();
            series.Episodes = episodeReader.Read(xml);

            return series;
        }
    }
}
