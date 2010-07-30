using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TvDbDotNet
{
    public class TvDbEpisodeXmlReader : TvDbXmlReader<TvDbEpisode>
    {
        public TvDbEpisodeXmlReader()
            : base("Episode")
        {
        }

        protected override TvDbEpisode ReadElement(XElement xmlElement)
        {
            TvDbEpisode episode = new TvDbEpisode();
            episode.Id = xmlElement.Element("id").Value;
            episode.Name = xmlElement.Element("EpisodeName").Value;
            episode.Director = xmlElement.Element("Director").Value;
            episode.Writer = xmlElement.Element("Writer").Value;

            DateTime firstAired;
            if (DateTime.TryParse(xmlElement.Element("FirstAired").Value, out firstAired))
                episode.FirstAired = firstAired;

            return episode;
        }
    }
}
