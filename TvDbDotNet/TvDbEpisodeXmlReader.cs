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
    /// Provides a translation from xml to TvDbEpisode.
    /// </summary>
    public class TvDbEpisodeXmlReader : TvDbXmlReader<TvDbEpisode>
    {
        /// <summary>
        /// Initializes a new instance of the TvDbEpisodeXmlReader class.
        /// </summary>
        public TvDbEpisodeXmlReader()
            : base("Episode")
        {
        }

        /// <summary>
        /// Reads an XElement into a TvDbEpisode object.
        /// </summary>
        /// <param name="xml">The XElement to translate.</param>
        /// <returns>a TvDbEpisode object.</returns>
        protected override TvDbEpisode ReadElement(XElement xmlElement)
        {
            TvDbEpisode episode = new TvDbEpisode();
            episode.Id = xmlElement.Element("id").Value;
            episode.Name = xmlElement.Element("EpisodeName").Value;
            episode.Directors = xmlElement.Element("Director").Value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            episode.Writers = xmlElement.Element("Writer").Value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            episode.GuestStars = xmlElement.Element("GuestStars").Value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            DateTime firstAired;
            if (DateTime.TryParse(xmlElement.Element("FirstAired").Value, out firstAired))
                episode.FirstAired = firstAired;

            return episode;
        }
    }
}
