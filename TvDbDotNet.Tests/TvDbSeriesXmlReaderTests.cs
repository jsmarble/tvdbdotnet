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
