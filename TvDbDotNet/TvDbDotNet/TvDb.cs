using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvDbDotNet
{
    public class TvDb
    {
        private string apiKey;
        private IEnumerable<TvDbMirror> mirrors;
        private IEnumerable<TvDbLanguage> languages;
        private object syncLock = new object();

        public TvDb(string apiKey)
        {
            this.ChangeApiKey(apiKey);
        }

        public void ChangeApiKey(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException("An API key must be provided.", "apiKey");
            this.apiKey = apiKey;
        }

        #region Mirrors

        public TvDbMirror GetRandomMirror(TvDbMirrorType type)
        {
            var mirrors = GetMirrors(type).ToList();
            Random rnd = new Random();
            int index = rnd.Next(0, mirrors.Count - 1);
            return mirrors[index];
        }

        public IEnumerable<TvDbMirror> GetMirrors(TvDbMirrorType type)
        {
            var mirrors = GetMirrors();
            mirrors = mirrors.Where(x => x.Type.HasFlag(type)).ToList();
            return mirrors;
        }

        public IEnumerable<TvDbMirror> GetMirrors()
        {
            if (mirrors == null)
            {
                lock (syncLock)
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

        private string GetMirrorsXml()
        {
            throw new NotImplementedException();
        } 

        #endregion

        #region Languages

        public IEnumerable<TvDbLanguage> GetLanguages()
        {
            if (languages == null)
            {
                lock (syncLock)
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

        private string GetLanguagesXml()
        {
            throw new NotImplementedException();
        } 

        #endregion
    }
}
