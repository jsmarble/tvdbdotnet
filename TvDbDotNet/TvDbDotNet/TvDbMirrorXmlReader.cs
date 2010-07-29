using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TvDbDotNet
{
    public class TvDbMirrorXmlReader : TvDbXmlReader<TvDbMirror>
    {
        public TvDbMirrorXmlReader()
            : base("Mirror")
        {
        }

        protected override TvDbMirror ReadElement(XElement xmlElement)
        {
            TvDbMirror mirror = new TvDbMirror();
            mirror.Id = xmlElement.Element("id").Value;
            mirror.Url = new Uri(xmlElement.Element("mirrorpath").Value);
            mirror.Type = (TvDbMirrorType)(int.Parse(xmlElement.Element("typemask").Value));
            return mirror;
        }
    }
}
