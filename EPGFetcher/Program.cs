using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EPGFetcher.Models;
using Newtonsoft.Json;

namespace EPGFetcher {
    internal class Program {
        private static async Task Main() {
            //TODO: Move this to config
            const int hours = 168;

            // Full listing at
            // https://broadcastservices.imdserve.com/broadcast/v1/schedulesettings
            var channels = new List<int> {
                                             //94, // BBC One London
                                             //105 // BBC Two London
                                             94,105,26,132,134,248,47,185,1859,1961,11708,158,1959,2056,160,13210,13275,197,2685,2050,11928,11820,288,2098,2062,2008,1963,1882,801,1061,2476,2212,5060,2058,2603,2059,11780,2178,2177,2586,11962,2189,40,922,11606,13090,11468,11578,2134,2179,12997,13211,11740,13058,11968,1201,5076,292,11816,13039,202,13043,11990,11693,13035,264,2200,2700,265,13036,300,11150,11568,11791,661,2142,123,11774,1876,483,482,1981,11927,2544,11670,1983,128,1661,156,421,165,215,2593,664,147,2668,4071,39,2559,2165,1601,2185,182,213,10008,1421,11595,271,2545,1462,721,48,49,256
                                         };

            //How many channels to load data for at one time
            //var ChannelBatches = 5;
            
            Console.WriteLine("Creating HTTP Client");
            var httpClient = new HttpClient();
            var startDate = DateTime.Today;
            
            Console.WriteLine("Creating Output Object");
            var output = new Xmltv.Tv();

            Console.WriteLine("Loading Results");
            List<InputData.Rootobject> results = (await (await channels.Select(channelId => httpClient.GetAsync($"https://broadcastservices.imdserve.com/broadcast/v1/schedule?startdate={startDate}&hours={hours}&totalwidthunits=1000&channels={channelId}"))
                                                                       .WhenAll())
                                                       .Where(response => response.IsSuccessStatusCode)
                                                       .Select(response => response.Content.ReadAsStringAsync())
                                                       .WhenAll())
                                                .Select(JsonConvert.DeserializeObject<InputData.Rootobject>)
                                                .ToList();
            Console.WriteLine("Got "+results.Count+" results");

            Console.WriteLine("Getting Channels");
            output.Channel.AddRange(results.SelectMany(rootObject => rootObject.Channels)
                                           .Select(channel => new Xmltv.TvChannel {
                                                                                      DisplayName = new List<string> {channel.DisplayName},
                                                                                      Icon = new Xmltv.TvChannelIcon {Src = channel.Image},
                                                                                      Id = channel.Id
                                                                                  }));
            Console.WriteLine("Got "+output.Channel.Count+" channels");

            Console.WriteLine("Getting Programs");
            output.Programme.AddRange(results.SelectMany(rootObject => rootObject.Channels.SelectMany(channel => channel.TvListings.Select(listing => (program: listing, channelId: channel.Id))))
                                             .Select(tvListing => new Xmltv.TvProgramme {
                                                                                            Channel = tvListing.channelId.ToString(),
                                                                                            Title = new Xmltv.TvProgrammeTitle {Value = tvListing.program.Title},
                                                                                            Subtitle = new Xmltv.TvProgrammeSubtitle {Value = tvListing.program.EpisodePositionInSeries},
                                                                                            Desc = new Xmltv.TvProgrammeDesc {Value = tvListing.program.Description},
                                                                                            Date = tvListing.program.StartTimeMF.Date,
                                                                                            Category = new List<Xmltv.TvProgrammeCategory> {new Xmltv.TvProgrammeCategory {Value = tvListing.program.Genre}},
                                                                                            EpisodeNum = new List<Xmltv.TvProgrammeEpisodeNum> {new Xmltv.TvProgrammeEpisodeNum {System = "onscreen", Value = tvListing.program.EpisodePositionInSeries}},
                                                                                            PreviouslyShown = tvListing.program.IsRepeat ? new Xmltv.TvProgrammePreviouslyShown() : null,
                                                                                            Start = tvListing.program.StartTimeMF,
                                                                                            Stop = tvListing.program.EndTimeMF
                                                                                        }));
            Console.WriteLine("Got "+output.Programme.Count+" programmes");

            Console.WriteLine("Creating serializer");
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Xmltv.Tv));
            Console.WriteLine("Writing Output");
            serializer.Serialize(Console.Out, output);
        }
    }

    internal static class Extensions {
        public static async Task<IEnumerable<T>> WhenAll<T>(this IEnumerable<Task<T>> tasks) => await Task.WhenAll(tasks);
    }
}
