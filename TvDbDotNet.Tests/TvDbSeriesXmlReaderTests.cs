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
    public class TvDbSeriesXmlReaderTests
    {
        [Fact]
        public void Can_Read_Sample()
        {
            TvDbSeriesXmlReader reader = new TvDbSeriesXmlReader();
            string xml = GetSampleXml();
            var series = reader.Read(xml);
            Assert.NotNull(series);
        }

        [Fact]
        public void Episodes_Gets_Populated()
        {
            TvDbSeriesXmlReader reader = new TvDbSeriesXmlReader();
            string xml = GetSampleXml();
            var series = reader.Read(xml);
            Assert.NotNull(series.Episodes);
            Assert.True(series.Episodes.Any());
        }

        [Fact]
        public void Id_Property_Gets_Set_Correctly()
        {
            TvDbSeriesXmlReader reader = new TvDbSeriesXmlReader();
            string xml = GetSampleXml();
            var series = reader.Read(xml);

            string expected = "72449";
            string actual = series.Id;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Name_Property_Gets_Set_Correctly()
        {
            TvDbSeriesXmlReader reader = new TvDbSeriesXmlReader();
            string xml = GetSampleXml();
            var series = reader.Read(xml);

            string expected = "Stargate SG-1";
            string actual = series.Name;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Network_Property_Gets_Set_Correctly()
        {
            TvDbSeriesXmlReader reader = new TvDbSeriesXmlReader();
            string xml = GetSampleXml();
            var series = reader.Read(xml);

            string expected = "SciFi";
            string actual = series.Network;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Actors_Property_Gets_Set_Correctly()
        {
            TvDbSeriesXmlReader reader = new TvDbSeriesXmlReader();
            string xml = GetSampleXml();
            TvDbSeries series = reader.Read(xml);

            Assert.True(series.Actors.Count() > 2);
        }

        private string GetSampleXml()
        {
            return Resources.series;
        }
    }
}
