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
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace TvDbDotNet
{
    /// <summary>
    /// Provides a translation from xml to T.
    /// </summary>
    public abstract class TvDbXmlReader<T>
    {
        private string elementName;

        /// <summary>
        /// Constructs a new instance of the TvDbXmlReader class.
        /// </summary>
        /// <param name="elementName">The name of the elements from which the objects will be populated.</param>
        public TvDbXmlReader(string elementName)
        {
            this.elementName = elementName;
        }

        /// <summary>
        /// Translates xml into a collection of T.
        /// </summary>
        /// <param name="xml">The xml to translate.</param>
        /// <returns>a collection of T.</returns>
        public IEnumerable<T> Read(string xml)
        {
            using (StringReader xmlReader = new StringReader(xml))
            {
                XDocument xDoc = XDocument.Load(xmlReader);
                var objects = xDoc.Descendants(elementName).Select(x => ReadElement(x)).ToList();
                return objects;
            }
        }

        /// <summary>
        /// Reads an XElement into an object.
        /// </summary>
        /// <param name="xml">The XElement to translate.</param>
        /// <returns>a populated object.</returns>
        protected abstract T ReadElement(XElement xmlElement);
    }
}
