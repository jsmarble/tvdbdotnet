#region License
/*
Copyright © 2010, The TvDbDotNet Project
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright
notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright
notice, this list of conditions and the following disclaimer in the
documentation and/or other materials provided with the distribution.

3. Neither the name of the nor the
names of its contributors may be used to endorse or promote products
derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS “AS IS” AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
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
    /// <summary>
    /// An interface to some capabilities of the tvdb.com API.
    /// </summary>
    public class TvDb
    {
        protected string apiKey;

        protected IEnumerable<TvDbMirror> mirrors;
        protected IEnumerable<TvDbLanguage> languages;

        private object mirrorsLock = new object();
        private object languagesLock = new object();

        /// <summary>
        /// Initializes a new instance of the TvDb class using the specified API key.
        /// </summary>
        /// <param name="apiKey"></param>
        public TvDb(string apiKey)
        {
            this.ChangeApiKey(apiKey);
        }

        #region Properties

        /// <summary>
        /// The selected TvDbMirror. If null, api operations will choose a random mirror.
        /// </summary>
        public virtual TvDbMirror Mirror { get; set; }

        /// <summary>
        /// The selected TvDbLanguage. If null, api operations will use TvDbLanguage.DefaultLanguage.
        /// </summary>
        public virtual TvDbLanguage Language { get; set; }

        /// <summary>
        /// The path used for storage of temporary files, such as downloaded or extracted zip files. If not a valid path, api operations will use the system temp path.
        /// </summary>
        public virtual string TempPath { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Changes the tvdb.com API key used.
        /// </summary>
        /// <param name="apiKey">The new API key.</param>
        public void ChangeApiKey(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("An API key must be provided.", "apiKey");
            this.apiKey = apiKey;
        }

        /// <summary>
        /// Gets the temporary path to use.
        /// </summary>
        /// <returns>the value of the TempPath property, if a valid path, otherwise the system temp path.</returns>
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

        /// <summary>
        /// Gets a mirror that supports the specified type.
        /// </summary>
        /// <param name="type">The type the mirror should support.</param>
        /// <returns>the value of the Mirror property, if not null and supporting the specified type, otherwise a random mirror from the mirrors list.</returns>
        public virtual TvDbMirror GetMirror(TvDbMirrorType type)
        {
            TvDbMirror mirror = this.Mirror;
            if (mirror == null || !mirror.Type.HasFlag(type))
                mirror = GetRandomMirror(type);
            if (mirror == null)
                throw new InvalidOperationException("No mirrors could be found supporting the specific mirror type.");
            return mirror;
        }

        /// <summary>
        /// Gets a random mirror from the mirrors list, supporting the specified type.
        /// </summary>
        /// <param name="type">The type the mirror should support.</param>
        /// <returns>a TvDbMirror object.</returns>
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

        /// <summary>
        /// Gets the list of mirrors from TvDb, supporting the specified type.
        /// </summary>
        /// <param name="type">The type the mirror should support.</param>
        /// <returns>a collection of TvDbMirror objects.</returns>
        public virtual IEnumerable<TvDbMirror> GetMirrors(TvDbMirrorType type)
        {
            var mirrors = GetMirrors();
            mirrors = mirrors.Where(x => x.Type.HasFlag(type)).ToList();
            return mirrors;
        }

        /// <summary>
        /// Gets the list of mirrors from TvDb.
        /// </summary>
        /// <returns>a collection of TvDbMirror objects.</returns>
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

        /// <summary>
        /// Gets the mirrors xml from TvDb.
        /// </summary>
        /// <returns>the mirrors xml.</returns>
        public virtual string GetMirrorsXml()
        {
            string url = GetMirrorsUrl();
            using (WebClient wc = new WebClient())
            {
                string xml = wc.DownloadString(url);
                return xml;
            }
        }

        /// <summary>
        /// Gets the url for the mirrors xml from TvDb.
        /// </summary>
        /// <returns>the mirrors url.</returns>
        public virtual string GetMirrorsUrl()
        {
            string url = string.Format("http://www.thetvdb.com/api/{0}/mirrors.xml", this.apiKey);
            return url;
        }

        #endregion

        #region Languages

        /// <summary>
        /// Gets the desired language.
        /// </summary>
        /// <returns>the value of the Language property, if not null, otherwise TvDbLanguage.DefaultLanguage.</returns>
        public virtual TvDbLanguage GetLanguage()
        {
            TvDbLanguage lang = this.Language;
            if (lang == null)
                lang = TvDbLanguage.DefaultLanguage;
            return lang;
        }

        /// <summary>
        /// Gets the list of languages from TvDb.
        /// </summary>
        /// <returns>a collection of TvDbLanguage objects.</returns>
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

        /// <summary>
        /// Gets the languages xml from TvDb.
        /// </summary>
        /// <returns>the languages xml.</returns>
        public virtual string GetLanguagesXml()
        {
            string url = GetLanguagesUrl();
            using (WebClient wc = new WebClient())
            {
                string xml = wc.DownloadString(url);
                return xml;
            }
        }

        /// <summary>
        /// Gets the url for the languages xml from TvDb.
        /// </summary>
        /// <returns>the languages url.</returns>
        public virtual string GetLanguagesUrl()
        {
            TvDbMirror mirror = GetMirror(TvDbMirrorType.Xml);
            string url = string.Format("{0}api/{1}/languages.xml", mirror.Url, this.apiKey);
            return url;
        }

        #endregion

        #region Series Search

        /// <summary>
        /// Gets series by name.
        /// </summary>
        /// <param name="name">The series name.</param>
        /// <returns>a collection of TvDbSeriesBase objects.</returns>
        public virtual IEnumerable<TvDbSeriesBase> GetSeries(string name)
        {
            string languageXml = GetSeriesSearchXml(name);
            TvDbSeriesSearchResultsXmlReader xmlReader = new TvDbSeriesSearchResultsXmlReader();
            var series = xmlReader.Read(languageXml);
            return series;
        }

        /// <summary>
        /// Gets the series search xml from TvDb.
        /// </summary>
        /// <returns>the series search xml.</returns>
        public virtual string GetSeriesSearchXml(string name)
        {
            string url = GetSeriesSearchUrl(name);
            using (WebClient wc = new WebClient())
            {
                string xml = wc.DownloadString(url);
                return xml;
            }
        }

        /// <summary>
        /// Gets the url for the series search xml from TvDb.
        /// </summary>
        /// <returns>the series search url.</returns>
        public virtual string GetSeriesSearchUrl(string name)
        {
            string url = string.Format("http://www.thetvdb.com/api/GetSeries.php?seriesname={0}", name);
            return url;
        }

        #endregion

        #region Series

        /// <summary>
        /// Gets the full series information for the specified TvDbSeriesBase.
        /// </summary>
        /// <param name="seriesBase">The base series.</param>
        /// <returns>a TvDbSeries object.</returns>
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

        /// <summary>
        /// Downloads the series zip file for the specified TvDbSeriesBase.
        /// </summary>
        /// <param name="seriesBase">The base series.</param>
        /// <returns>the path to the downloaded zip file.</returns>
        public virtual string DownloadSeriesZip(TvDbSeriesBase seriesBase)
        {
            string path = GetSeriesZipPath(seriesBase);
            return this.DownloadSeriesZip(seriesBase, path);
        }

        /// <summary>
        /// Downloads the series zip file for the specified TvDbSeriesBase.
        /// </summary>
        /// <param name="seriesBase">The base series.</param>
        /// <param name="path">The path to which to download the zip file.</param>
        /// <returns>the path to the downloaded zip file.</returns>
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

        /// <summary>
        /// Gets the url for the series zip file from TvDb.
        /// </summary>
        /// <returns>the series zip url.</returns>
        public virtual string GetSeriesZipUrl(TvDbSeriesBase seriesBase)
        {
            TvDbMirror mirror = GetMirror(TvDbMirrorType.Zip);
            string url = string.Format("{0}api/{1}/series/{2}/all/{3}.zip", mirror.Url, this.apiKey, seriesBase.Id, this.GetLanguage().Abbreviation);
            return url;
        }

        /// <summary>
        /// Extract the zip file at the specified path.
        /// </summary>
        /// <param name="path">The path of the zip file.</param>
        /// <returns>the path to the extracted files.</returns>
        public virtual string ExtractZip(string path)
        {
            string extracted = Path.ChangeExtension(path, null);
            ZipFile zip = ZipFile.Read(path);
            zip.ExtractAll(extracted, ExtractExistingFileAction.OverwriteSilently);
            return extracted;
        }

        /// <summary>
        /// Generates a zip file path based on the temp path and specified TvDbSeriesBase.
        /// </summary>
        /// <param name="seriesBase">The series.</param>
        /// <returns>a generated file path.</returns>
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
