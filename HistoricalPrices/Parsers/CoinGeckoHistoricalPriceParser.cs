using CoinStory.Core.QuickParsers.HistoricalPrices.ColumnDefs;
using CoinStory.Models.Enumerations;
using CoinStory.Models.Interfaces;
using QuickParser.Classes;
using QuickParser.Interfaces;
using static CoinStory.Core.QuickParsers.HistoricalPrices.ColumnDefs.CoinGeckoColumn;
using static CoinStory.QuickParsers.Helpers.DefaultTransforms;

namespace CoinStory.Core.QuickParsers.HistoricalPrices.Parsers
{
    public class CoinGeckoHistoricalPriceParser : HistoricalPriceParser<CoinGeckoColumn>
    {
        private readonly Currency _currency;

        protected override HistoricalDataSource Source => HistoricalDataSource.CoinGecko;

        protected override IParsedRowMapping<IHistoricalPrice> Mapping => new HistoricalPriceMap<IHistoricalPrice>
        {
            DateMap = new ColumnMap<DateTime>(SNAPPED_AT, (date) => ConvertDate(date.Replace("UTC", ""), "UTC")),
            CurrencyMap = new ColumnMap<Currency>((_) => _currency),
            OpenMap = new ColumnMap<decimal>(PRICE, ConvertAmount),
            VolumeMap = new ColumnMap<decimal>(TOTAL_VOLUME, ConvertAmount),
            MarketCapMap = new ColumnMap<decimal>(MARKET_CAP, ConvertAmount),
        };

        public CoinGeckoHistoricalPriceParser(string fileContent, Currency currency) : base(fileContent)
        {
            _currency = currency;
        }
    }
}