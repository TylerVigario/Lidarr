using NLog;
using NzbDrone.Common.Http;
using NzbDrone.Core.Configuration;
using NzbDrone.Core.Parser;

namespace NzbDrone.Core.Indexers.Nyaa
{
    public class Nyaa : HttpIndexerBase<NyaaSettings>
    {
        public override string Name => "Nyaa";

        public override string Protocol => nameof(TorrentDownloadProtocol);

        public Nyaa(IHttpClient httpClient, IIndexerStatusService indexerStatusService, IConfigService configService, IParsingService parsingService, Logger logger)
            : base(httpClient, indexerStatusService, configService, parsingService, logger)
        {
        }

        public override IIndexerRequestGenerator GetRequestGenerator()
        {
            return new NyaaRequestGenerator() { Settings = Settings };
        }

        public override IParseIndexerResponse GetParser()
        {
            return new TorrentRssParser() { UseGuidInfoUrl = true, SizeElementName = "size", InfoHashElementName = "infoHash", PeersElementName = "leechers", CalculatePeersAsSum = true, SeedsElementName = "seeders" };
        }
    }
}
