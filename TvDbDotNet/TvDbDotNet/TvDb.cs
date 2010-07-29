using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TvDbDotNet
{
    public class TvDb
    {
        private string apiKey;

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
    }
}
