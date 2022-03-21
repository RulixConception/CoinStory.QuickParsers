using CoinStory.Models.Enumerations;
using CoinStory.Models.Interfaces;
using QuickParser.Classes;

namespace CoinStory.Core.QuickParsers.HistoricalPrices
{
    public abstract class HistoricalPriceParser<TColumnDef> : ParserBase<IHistoricalPrice, TColumnDef>
        where TColumnDef : struct, IConvertible
    {
        protected abstract HistoricalDataSource Source { get; }

        public HistoricalPriceParser(string fileContent) : base(fileContent)
        {

        }

        protected override IList<IHistoricalPrice> PostProcessing(IEnumerable<IHistoricalPrice> objects)
        {
            return objects.Where(o => o.Currency != Currency.USD).ToList();
        }

        protected override object[] GetParams() => new object[] { Source };
    }
}