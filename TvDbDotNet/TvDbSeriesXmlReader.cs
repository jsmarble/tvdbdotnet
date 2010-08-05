#region License
/*
"The contents of this file are subject to the Mozilla Public License
Version 1.1 (the "License"); you may not use this file except in
compliance with the License. You may obtain a copy of the License at
http://www.mozilla.org/MPL/

Software distributed under the License is distributed on an "AS IS"
basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See the
License for the specific language governing rights and limitations
under the License.

The Initial Developer of the Original Code is Joshua Marble.
Portions created by Joshua Marble are Copyright (C) 2010. All Rights Reserved.

Contributor(s):

Alternatively, the contents of this file may be used under the terms
of the GPL license (the "[GPL] License"), in which case the
provisions of [GPL] License are applicable instead of those
above. If you wish to allow use of your version of this file only
under the terms of the [GPL] License and not to allow others to use
your version of this file under the MPL, indicate your decision by
deleting the provisions above and replace them with the notice and
other provisions required by the [GPL] License. If you do not delete
the provisions above, a recipient may use your version of this file
under either the MPL or the [GPL] License."
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
