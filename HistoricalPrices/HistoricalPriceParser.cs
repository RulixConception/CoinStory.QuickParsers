using CoinStory.Models;
using CoinStory.Models.Enumerations;
using QuickParser.Classes;

namespace CoinStory.QuickParsers.HistoricalPrices
{
    public abstract class HistoricalPriceParser<TColumnDef> : ParserBase<HistoricalPrice, TColumnDef>
        where TColumnDef : struct, IConvertible
    {
        protected abstract HistoricalDataSource Source { get; }

        public HistoricalPriceParser(string fileContent) : base(fileContent)
        {

        }

        protected override IList<HistoricalPrice> PostProcessing(IEnumerable<HistoricalPrice> objects) =>
            objects.Where(o => o.Currency != Currency.USD).ToList();

        protected override HistoricalPrice OnInstantiate() =>
            new HistoricalPrice { Source = Source };
    }
}