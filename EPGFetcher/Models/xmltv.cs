using System;
using System.Collections.Generic;

namespace EPGFetcher.Models {
    public class Xmltv {
        [Serializable]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
        public class Tv {
            [System.Xml.Serialization.XmlElement("channel")]
            public List<TvChannel> Channel { get; set; }

            [System.Xml.Serialization.XmlElement("programme")]
            public List<TvProgramme> Programme { get; set; }

            internal Tv() {
                Channel = new List<TvChannel>();
                Programme = new List<TvProgramme>();
            }
        }

        [Serializable]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public class TvChannel {
            [System.Xml.Serialization.XmlElement("display-name")]
            public List<string> DisplayName { get; set; }

            public TvChannelIcon Icon { get; set; }

            [System.Xml.Serialization.XmlAttribute]
            public string Id { get; set; }
        }

        [Serializable]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public class TvChannelIcon {
            [System.Xml.Serialization.XmlAttribute]
            public string Src { get; set; }
        }

        [Serializable]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public class TvProgramme {
            public TvProgrammeTitle Title { get; set; }

            [System.Xml.Serialization.XmlElement("sub-title")]
            public TvProgrammeSubtitle Subtitle { get; set; }

            public TvProgrammeDesc Desc { get; set; }
            public DateTime Date { get; set; }

            [System.Xml.Serialization.XmlElement("category")]
            public List<TvProgrammeCategory> Category { get; set; }

            [System.Xml.Serialization.XmlElement("episode-num")]
            public List<TvProgrammeEpisodeNum> EpisodeNum { get; set; }

            [System.Xml.Serialization.XmlElement("previously-shown")]
            public TvProgrammePreviouslyShown PreviouslyShown { get; set; }

            [System.Xml.Serialization.XmlAttribute]
            public DateTime Start { get; set; }

            [System.Xml.Serialization.XmlAttribute]
            public DateTime Stop { get; set; }

            [System.Xml.Serialization.XmlAttribute]
            public string Channel { get; set; }
        }

        [Serializable]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public class TvProgrammeTitle {
            [System.Xml.Serialization.XmlText]
            public string Value { get; set; }
        }

        [Serializable]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public class TvProgrammeSubtitle {
            [System.Xml.Serialization.XmlText]
            public string Value { get; set; }
        }

        [Serializable]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public class TvProgrammeDesc {
            [System.Xml.Serialization.XmlText]
            public string Value { get; set; }
        }

        [Serializable]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public class TvProgrammeCategory {
            [System.Xml.Serialization.XmlText]
            public string Value { get; set; }
        }

        [Serializable]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public class TvProgrammeEpisodeNum {
            [System.Xml.Serialization.XmlAttribute]
            public string System { get; set; }

            [System.Xml.Serialization.XmlText]
            public string Value { get; set; }
        }

        [Serializable]
        [System.ComponentModel.DesignerCategory("code")]
        [System.Xml.Serialization.XmlType(AnonymousType = true)]
        public class TvProgrammePreviouslyShown {
            // [System.Xml.Serialization.XmlAttribute]
            // public DateTime? Start { get; set; }
        }
    }
}
