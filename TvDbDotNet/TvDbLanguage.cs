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
    /// Represents a language on TvDb.
    /// </summary>
    public class TvDbLanguage
    {
        /// <summary>
        /// The default language, English (en).
        /// </summary>
        public static readonly TvDbLanguage DefaultLanguage = new TvDbLanguage("en", "English");

        /// <summary>
        /// Initializes a new instance of the TvDbLanguage class.
        /// </summary>
        public TvDbLanguage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the TvDbLanguage class, setting the abbreviation and name as specified.
        /// </summary>
        public TvDbLanguage(string abbreviation, string name)
        {
            this.Abbreviation = abbreviation;
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the language Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the language abbreviation.
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the language name.
        /// </summary>
        public string Name { get; set; }
    }
}
