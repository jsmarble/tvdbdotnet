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

namespace TvDbDotNet
{
    /// <summary>
    /// Provides a translation from xml to TvDbSeriesBase.
    /// </summary>
    public class TvDbSeriesSearchResultsXmlReader : TvDbXmlReader<TvDbSeriesBase>
    {
        /// <summary>
        /// Initializes a new instance of the TvDbSeriesSearchResultsXmlReader class.
        /// </summary>
        public TvDbSeriesSearchResultsXmlReader()
            : base("Series")
        {
        }

        /// <summary>
        /// Reads an XElement into a TvDbSeriesBase object.
        /// </summary>
        /// <param name="xml">The XElement to translate.</param>
        /// <returns>a TvDbSeriesBase object.</returns>
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
