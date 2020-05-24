using System;

namespace EPGFetcher.Models {
    public class InputData {
        public class Rootobject {
            public Channel[] Channels { get; set; }
            public bool DisplaySkyButton { get; set; }
        }

        public class Channel {
            public string DisplayName { get; set; }
            public string Id { get; set; }
            public string Image { get; set; }
            public bool IsSkyApiEnabledForChannel { get; set; }
            public string Key { get; set; }
            public int Media { get; set; }
            public Tvlisting[] TvListings { get; set; }
            public string WatchNow { get; set; }
        }

        public class Tvlisting {
            public string ContinuationState { get; set; }
            public string Description { get; set; }
            public string EndTime { get; set; }
            public DateTime EndTimeMF { get; set; }
            public string EpisodeId { get; set; }
            public string EpisodePositionInSeries { get; set; }
            public int? FilmStarRating { get; set; }
            public string Genre { get; set; }
            public string Image { get; set; }
            public bool IsNewSeries { get; set; }
            public bool IsRepeat { get; set; }
            public bool IsTvChoice { get; set; }
            public string Link { get; set; }
            public string PaId { get; set; }
            public string ProgrammeId { get; set; }
            public int RelativeSize { get; set; }
            public string Specialisation { get; set; }
            public string StartTime { get; set; }
            public DateTime StartTimeMF { get; set; }
            public string Title { get; set; }
        }
    }
}
