using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using TvDbDotNet.Tests.Properties;

namespace TvDbDotNet.Tests
{
    public class TvDbLanguageXmlReaderTests
    {
        [Fact]
        public void Can_Read_Sample()
        {
            TvDbLanguageXmlReader reader = new TvDbLanguageXmlReader();
            string xml = GetSampleXml();
            var languages = reader.Read(xml);
            Assert.NotNull(languages);
            Assert.True(languages.Any());
        }

        [Fact]
        public void Id_Property_Gets_Set_Correctly()
        {
            TvDbLanguageXmlReader reader = new TvDbLanguageXmlReader();
            string xml = GetSampleXml();
            var languages = reader.Read(xml);
            
            var dansk = languages.First();
            string expected = "10";
            string actual = dansk.Id;
            Assert.Equal(expected, actual);

            var english = languages.Last();
            expected = "9";
            actual = english.Id;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Name_Property_Gets_Set_Correctly()
        {
            TvDbLanguageXmlReader reader = new TvDbLanguageXmlReader();
            string xml = GetSampleXml();
            var languages = reader.Read(xml);

            var dansk = languages.First();
            string expected = "Dansk";
            string actual = dansk.Name;
            Assert.Equal(expected, actual);

            var english = languages.Last();
            expected = "Norsk";
            actual = english.Name;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Abbreviation_Property_Gets_Set_Correctly()
        {
            TvDbLanguageXmlReader reader = new TvDbLanguageXmlReader();
            string xml = GetSampleXml();
            var languages = reader.Read(xml);

            var dansk = languages.First();
            string expected = "da";
            string actual = dansk.Abbreviation;
            Assert.Equal(expected, actual);

            var english = languages.Last();
            expected = "no";
            actual = english.Abbreviation;
            Assert.Equal(expected, actual);
        }

        private string GetSampleXml()
        {
            return Resources.languages;
        }
    }
}
