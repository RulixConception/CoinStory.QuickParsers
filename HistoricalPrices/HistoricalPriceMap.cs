using CoinStory.Models.Enumerations;
using CoinStory.Models.Interfaces;
using QuickParser.Classes;
using QuickParser.Interfaces;

namespace CoinStory.Core.QuickParsers.HistoricalPrices
{
    public class HistoricalPriceMap<TObject> : IParsedRowMapping<TObject> where TObject : IHistoricalPrice
    {
        public ColumnMap<DateTime>? DateMap { get; set; }

        public ColumnMap<Currency>? CurrencyMap { get; set; }

        public ColumnMap<decimal>? OpenMap { get; set; }

        public ColumnMap<decimal>? HighMap { get; set; }

        public ColumnMap<decimal>? LowMap { get; set; }

        public ColumnMap<decimal>? CloseMap { get; set; }

        public ColumnMap<decimal>? VolumeMap { get; set; }

        public ColumnMap<decimal>? MarketCapMap { get; set; }

        public TObject Map(ParsedRow row, TObject instance, params object[] parameters)
        {
            instance.Date = DateMap?.GetValue(row) ?? new DateTime();
            instance.Currency = CurrencyMap?.GetValue(row) ?? Currency.USD;
            instance.Open = OpenMap?.GetValue(row) ?? 0;
            instance.High = HighMap?.GetValue(row) ?? 0;
            instance.Low = LowMap?.GetValue(row) ?? 0;
            instance.Close = CloseMap?.GetValue(row) ?? 0;
            instance.Volume = VolumeMap?.GetValue(row) ?? 0;
            instance.MarketCap = MarketCapMap?.GetValue(row) ?? 0;

            return instance;
        }
    }
}