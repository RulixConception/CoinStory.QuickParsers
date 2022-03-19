using CoinStory.Models.Enumerations;
using CoinStory.Models.Interfaces;
using Newtonsoft.Json;
using QuickParser.Classes;
using QuickParser.Interfaces;

namespace CoinStory.Core.QuickParsers.Transactions
{
    public class TransactionMap<TObject> : IParsedRowMapping<TObject> where TObject : ITransaction
    {
        public ColumnMap<string>? IdentifierMap { get; set; }

        public ColumnMap<DateTime>? DateMap { get; set; }

        public ColumnMap<decimal>? AmountInMap { get; set; }

        public ColumnMap<Currency?>? CurrencyInMap { get; set; }

        public ColumnMap<decimal>? AmountOutMap { get; set; }

        public ColumnMap<Currency?>? CurrencyOutMap { get; set; }

        public ColumnMap<decimal>? FeeMap { get; set; }

        public ColumnMap<Currency?>? FeeCurrencyMap { get; set; }

        public ColumnMap<TransactionType>? TypeMap { get; set; }

        public Func<ParsedRow, object>? MetadataConstruct { get; set; }

        public TObject Map(ParsedRow row, TObject instance, params object[] parameters)
        {
            instance.Platform = parameters[0] as Platform? ?? Platform.Binance;
            instance.Identifier = IdentifierMap?.GetValue(row) ?? "";
            instance.Date = DateMap?.GetValue(row) ?? new DateTime();
            instance.AmountIn = AmountInMap?.GetValue(row) ?? 0;
            instance.CurrencyIn = CurrencyInMap?.GetValue(row) ?? null;
            instance.AmountOut = AmountOutMap?.GetValue(row) ?? 0;
            instance.CurrencyOut = CurrencyOutMap?.GetValue(row) ?? null;
            instance.Fee = FeeMap?.GetValue(row) ?? 0;
            instance.FeeCurrency = FeeCurrencyMap?.GetValue(row) ?? null;
            instance.Type = TypeMap?.GetValue(row) ?? TransactionType.Unknown;
            instance.Metadata = MetadataConstruct != null ? JsonConvert.SerializeObject(MetadataConstruct.Invoke(row)) : "";

            return instance;
        }
    }
}