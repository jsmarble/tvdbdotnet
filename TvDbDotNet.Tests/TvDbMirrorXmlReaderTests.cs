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
using Xunit;
using TvDbDotNet.Tests.Properties;

namespace TvDbDotNet.Tests
{
    public class TvDbMirrorXmlReaderTests
    {
        [Fact]
        public void Can_Read_Sample()
        {
            TvDbMirrorXmlReader reader = new TvDbMirrorXmlReader();
            string xml = GetSampleXml();
            var mirrors = reader.Read(xml);
            Assert.NotNull(mirrors);
            Assert.True(mirrors.Any());
        }

        [Fact]
        public void Id_Property_Gets_Set_Correctly()
        {
            TvDbMirrorXmlReader reader = new TvDbMirrorXmlReader();
            string xml = GetSampleXml();
            var mirrors = reader.Read(xml);
            var mirror = mirrors.First();
            string expected = "4";
            string actual = mirror.Id;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Url_Property_Gets_Set_Correctly()
        {
            TvDbMirrorXmlReader reader = new TvDbMirrorXmlReader();
            string xml = GetSampleXml();
            var mirrors = reader.Read(xml);
            var mirror = mirrors.First();
            Uri expected = new Uri("http://thetvdb.com");
            Uri actual = mirror.Url;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Type_Property_Gets_Set_Correctly()
        {
            TvDbMirrorXmlReader reader = new TvDbMirrorXmlReader();
            string xml = GetSampleXml();
            var mirrors = reader.Read(xml);
            var mirror = mirrors.First();
            TvDbMirrorType expected = TvDbMirrorType.Xml | TvDbMirrorType.Zip;
            TvDbMirrorType actual = mirror.Type;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Invalid_MirrorPath_Does_Not_Throw_Exception()
        {
            TvDbMirrorXmlReader reader = new TvDbMirrorXmlReader();
            string xml = GetSampleXml();
            xml = xml.Replace("http://thetvdb.com", "invalid mirror path");
            var mirrors = reader.Read(xml);
            var mirror = mirrors.First();
            Assert.Null(mirror.Url);
        }

        private string GetSampleXml()
        {
            return Resources.mirrors;
        }
    }
}
