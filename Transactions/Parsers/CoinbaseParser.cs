using CoinStory.Core.QuickParsers.Transactions.ColumnDefs;
using CoinStory.Models.Enumerations;
using CoinStory.Models.Interfaces;
using QuickParser.Classes;
using QuickParser.Interfaces;
using System.Text.RegularExpressions;
using static CoinStory.Core.QuickParsers.Transactions.ColumnDefs.CoinbaseColumn;
using static CoinStory.QuickParsers.Helpers.DefaultTransforms;

namespace CoinStory.Core.QuickParsers.Transactions.Parsers
{
    public class CoinbaseParser : TransactionParser<CoinbaseColumn>
    {
        private static readonly string _notesRegex = @"Converted [0-9.]* [A-Z]{2,5} to ([0-9.]*) ([A-Z]{2,5})";

        protected override Platform Platform => Platform.Coinbase;

        protected override int HeaderRowIndex => 7;

        protected override IParsedRowMapping<ITransaction> Mapping => new TransactionMap<ITransaction>
        {
            DateMap = new ColumnMap<DateTime>(TIMESTAMP, ConvertDate),
            AmountOutMap = new ColumnMap<decimal>(QUANTITY, (value, row) => row[TYPE] switch
            {
                "Receive" => ConvertAmount(value),
                "Coinbase Earn" => ConvertAmount(value),
                "Convert" => ConvertAmount(Regex.Matches(row[NOTES], _notesRegex, RegexOptions.None).Single().Groups[1].Value),
                _ => 0
            }),
            CurrencyOutMap = new ColumnMap<Currency?>(ASSET, (value, row) => row[TYPE] switch
            {
                "Receive" => ConvertCurrency(value),
                "Coinbase Earn" => ConvertCurrency(value),
                "Convert" => ConvertCurrency(Regex.Matches(row[NOTES], _notesRegex, RegexOptions.None).Single().Groups[2].Value),
                _ => 0
            }),
            AmountInMap = new ColumnMap<decimal>(QUANTITY, (value, row) => row[TYPE] switch
            {
                "Send" => ConvertAmount(value),
                "Convert" => ConvertAmount(value),
                _ => 0
            }),
            CurrencyInMap = new ColumnMap<Currency?>(ASSET, (value, row) => row[TYPE] switch
            {
                "Send" => ConvertCurrency(value),
                "Convert" => ConvertCurrency(value),
                _ => 0
            }),
            FeeMap = new ColumnMap<decimal>(FEES, ConvertAmount),
            FeeCurrencyMap = new ColumnMap<Currency?>(SPOT_PRICE_CURRENCY, ConvertCurrency),
            TypeMap = new ColumnMap<TransactionType>(TYPE, (value, row) => value switch
            {
                "Send" => TransactionType.Withdrawal,
                "Receive" => TransactionType.Deposit,
                "Convert" => TransactionType.Trade,
                "Coinbase Earn" => TransactionType.Reward,
                _ => TransactionType.Unknown
            }),
            MetadataConstruct = (row) => new
            {
                Notes = row[NOTES],
                SpotPriceAtTransaction = row[SPOT_PRICE],
                SpotPriceCurrency = row[SPOT_PRICE_CURRENCY],
                Subtotal = row[SUBTOTAL],
                TotalWithFees = row[TOTAL]
            }
        };

        public CoinbaseParser(string fileContent) : base(fileContent)
        {

        }
    }
}