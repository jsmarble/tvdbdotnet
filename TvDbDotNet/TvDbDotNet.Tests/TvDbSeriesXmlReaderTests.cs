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

        private string GetSampleXml()
        {
            return Resources.series;
        }
    }
}
