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
            mirror.Type = (TvDbMirrorType)(int.Parse(xmlElement.Element("typemask").Value));

            string mirrorPath = xmlElement.Element("mirrorpath").Value;
            if (Uri.IsWellFormedUriString(mirrorPath, UriKind.Absolute))
                mirror.Url = new Uri(mirrorPath);

            return mirror;
        }
    }
}
