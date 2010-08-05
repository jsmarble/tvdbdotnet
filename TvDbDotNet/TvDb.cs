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
using System.Net;
using System.IO;
using Ionic.Zip;

namespace TvDbDotNet
{
    public class TvDb
    {
        protected string apiKey;

        protected IEnumerable<TvDbMirror> mirrors;
        protected IEnumerable<TvDbLanguage> languages;

        private object mirrorsLock = new object();
        private object languagesLock = new object();

        public TvDb(string apiKey)
        {
            this.ChangeApiKey(apiKey);
        }

        #region Properties

        public virtual TvDbMirror Mirror { get; set; }
        public virtual TvDbLanguage Language { get; set; }
        public virtual string TempPath { get; set; }

        #endregion

        #region Methods

        public void ChangeApiKey(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("An API key must be provided.", "apiKey");
            this.apiKey = apiKey;
        }

        public virtual string GetTempPath()
        {
            string path = this.TempPath;

            if (!string.IsNullOrEmpty(path))
                Directory.CreateDirectory(path);
            else
                path = Path.GetTempPath();

            return path;
        }

        #region Mirrors

        public virtual TvDbMirror GetMirror(TvDbMirrorType type)
        {
            TvDbMirror mirror = this.Mirror;
            if (mirror == null || !mirror.Type.HasFlag(type))
                mirror = GetRandomMirror(type);
            if (mirror == null)
                throw new InvalidOperationException("No mirrors could be found supporting the specific mirror type.");
            return mirror;
        }

        public virtual TvDbMirror GetRandomMirror(TvDbMirrorType type)
        {
            var mirrors = GetMirrors(type).ToList();
            if (mirrors.Count > 1)
            {
                Random rnd = new Random();
                int index = rnd.Next(0, mirrors.Count - 1);
                return mirrors[index];
            }
            else
                return mirrors.FirstOrDefault();
        }

        public virtual IEnumerable<TvDbMirror> GetMirrors(TvDbMirrorType type)
        {
            var mirrors = GetMirrors();
            mirrors = mirrors.Where(x => x.Type.HasFlag(type)).ToList();
            return mirrors;
        }

        public virtual IEnumerable<TvDbMirror> GetMirrors()
        {
            if (mirrors == null)
            {
                lock (mirrorsLock)
                {
                    if (mirrors == null)
                    {
                        string mirrorXml = GetMirrorsXml();
                        TvDbMirrorXmlReader xmlReader = new TvDbMirrorXmlReader();
                        mirrors = xmlReader.Read(mirrorXml);
                    }
                }
            }
            return mirrors;
        }

        public virtual string GetMirrorsXml()
        {
            string url = GetMirrorsUrl();
            using (WebClient wc = new WebClient())
            {
                string xml = wc.DownloadString(url);
                return xml;
            }
        }

        public virtual string GetMirrorsUrl()
        {
            string url = string.Format("http://www.thetvdb.com/api/{0}/mirrors.xml", this.apiKey);
            return url;
        }

        #endregion

        #region Languages

        public virtual TvDbLanguage GetLanguage()
        {
            TvDbLanguage lang = this.Language;
            if (lang == null)
                lang = TvDbLanguage.DefaultLanguage;
            return lang;
        }

        public virtual IEnumerable<TvDbLanguage> GetLanguages()
        {
            if (languages == null)
            {
                lock (languagesLock)
                {
                    if (languages == null)
                    {
                        string languageXml = GetLanguagesXml();
                        TvDbLanguageXmlReader xmlReader = new TvDbLanguageXmlReader();
                        languages = xmlReader.Read(languageXml);
                    }
                }
            }
            return languages;
        }

        public virtual string GetLanguagesXml()
        {
            string url = GetLanguagesUrl();
            using (WebClient wc = new WebClient())
            {
                string xml = wc.DownloadString(url);
                return xml;
            }
        }

        public virtual string GetLanguagesUrl()
        {
            TvDbMirror mirror = GetMirror(TvDbMirrorType.Xml);
            string url = string.Format("{0}api/{1}/languages.xml", mirror.Url, this.apiKey);
            return url;
        }

        #endregion

        #region Series Search

        public virtual IEnumerable<TvDbSeriesBase> GetSeries(string name)
        {
            string languageXml = GetSeriesSearchXml(name);
            TvDbSeriesSearchResultsXmlReader xmlReader = new TvDbSeriesSearchResultsXmlReader();
            var series = xmlReader.Read(languageXml);
            return series;
        }

        public virtual string GetSeriesSearchXml(string name)
        {
            string url = GetSeriesSearchUrl(name);
            using (WebClient wc = new WebClient())
            {
                string xml = wc.DownloadString(url);
                return xml;
            }
        }

        public virtual string GetSeriesSearchUrl(string name)
        {
            string url = string.Format("http://www.thetvdb.com/api/GetSeries.php?seriesname={0}", name);
            return url;
        }

        #endregion

        #region Series

        public virtual TvDbSeries GetSeries(TvDbSeriesBase seriesBase)
        {
            string file = DownloadSeriesZip(seriesBase);
            string extracted = ExtractZip(file);
            string xmlFile = Path.Combine(extracted, this.GetLanguage().Abbreviation + ".xml");
            string xml = File.ReadAllText(xmlFile);
            TvDbSeriesXmlReader reader = new TvDbSeriesXmlReader();
            TvDbSeries series = reader.Read(xml);
            return series;
        }

        public virtual string DownloadSeriesZip(TvDbSeriesBase seriesBase)
        {
            string path = GetSeriesZipPath(seriesBase);
            return this.DownloadSeriesZip(seriesBase, path);
        }

        public virtual string DownloadSeriesZip(TvDbSeriesBase seriesBase, string path)
        {
            if (!File.Exists(path))
            {
                string url = GetSeriesZipUrl(seriesBase);

                Directory.CreateDirectory(Path.GetDirectoryName(path));
                using (WebClient wc = new WebClient())
                    wc.DownloadFile(url, path);
            }
            return path;
        }

        public virtual string GetSeriesZipUrl(TvDbSeriesBase seriesBase)
        {
            TvDbMirror mirror = GetMirror(TvDbMirrorType.Zip);
            string url = string.Format("{0}api/{1}/series/{2}/all/{3}.zip", mirror.Url, this.apiKey, seriesBase.Id, this.GetLanguage().Abbreviation);
            return url;
        }

        public virtual string ExtractZip(string path)
        {
            string extracted = Path.ChangeExtension(path, null);
            ZipFile zip = ZipFile.Read(path);
            zip.ExtractAll(extracted, ExtractExistingFileAction.OverwriteSilently);
            return extracted;
        }

        public virtual string GetSeriesZipPath(TvDbSeriesBase seriesBase)
        {
            string file = string.Format("{0} [{1}].zip", seriesBase.Name, this.GetLanguage().Abbreviation);
            string path = Path.Combine(GetTempPath(), "TvDb", "Series", file);
            return path;
        }

        #endregion

        #endregion
    }
}
