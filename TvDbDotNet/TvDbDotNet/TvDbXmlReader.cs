using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace TvDbDotNet
{
    public abstract class TvDbXmlReader<T>
    {
        private string elementName;

        public TvDbXmlReader(string elementName)
        {
            this.elementName = elementName;
        }

        public IEnumerable<T> Read(string xml)
        {
            using (StringReader xmlReader = new StringReader(xml))
            {
                XDocument xDoc = XDocument.Load(xmlReader);
                var objects = xDoc.Elements(elementName).Select(x => ReadElement(x)).ToList();
                return objects;
            }
        }

        public abstract T ReadElement(XElement xmlElement);
    }
}
