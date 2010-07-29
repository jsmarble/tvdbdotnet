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

        private string GetSampleXml()
        {
            return Resources.mirrors;
        }
    }
}
