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

namespace TvDbDotNet
{
    /// <summary>
    /// Represents a series on TvDb.
    /// </summary>
    public class TvDbSeries : TvDbSeriesBase
    {
        /// <summary>
        /// Initializes a new instance of the TvDbSeries class.
        /// </summary>
        public TvDbSeries()
        {
        }

        /// <summary>
        /// Gets or sets the series episodes.
        /// </summary>
        public IEnumerable<TvDbEpisode> Episodes { get; set; }

        /// <summary>
        /// Gets or sets the series network.
        /// </summary>
        public string Network { get; set; }

        /// <summary>
        /// Gets or sets the series actors.
        /// </summary>
        public IEnumerable<string> Actors { get; set; }

        /// <summary>
        /// Gets or sets the series genres.
        /// </summary>
        public IEnumerable<string> Genres { get; set; }

        /// <summary>
        /// Gets or sets the series status.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the series runtime in minutes.
        /// </summary>
        public string Runtime { get; set; }

        /// <summary>
        /// Gets or sets the series rating.
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// Gets or sets the series air time.
        /// </summary>
        public string AirTime { get; set; }

        /// <summary>
        /// Gets or sets the series air day.
        /// </summary>
        public string AirDay { get; set; }

        /// <summary>
        /// Gets or sets the series IMDB Id.
        /// </summary>
        public string IMDbId { get; set; }

        /// <summary>
        /// Gets or sets the series Zap2It Id.
        /// </summary>
        public string Zap2ItId { get; set; }
    }
}
