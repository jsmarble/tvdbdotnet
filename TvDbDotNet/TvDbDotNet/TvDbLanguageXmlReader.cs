using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TvDbDotNet
{
    public class TvDbLanguageXmlReader : TvDbXmlReader<TvDbLanguage>
    {
        public override TvDbLanguage ReadElement(XElement xmlElement)
        {
            TvDbLanguage lang = new TvDbLanguage();
            lang.Id = xmlElement.Element("id").Value;
            lang.Abbreviation = xmlElement.Element("abbreviation").Value;
            lang.Name = xmlElement.Element("name").Value;
            return lang;
        }
    }
}
