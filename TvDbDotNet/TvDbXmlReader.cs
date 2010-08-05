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
                var objects = xDoc.Descendants(elementName).Select(x => ReadElement(x)).ToList();
                return objects;
            }
        }

        protected abstract T ReadElement(XElement xmlElement);
    }
}
