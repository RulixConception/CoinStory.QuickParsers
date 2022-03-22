using CoinStory.Core.QuickParsers.Transactions.ColumnDefs;
using CoinStory.Models;
using CoinStory.Models.Enumerations;
using CoinStory.QuickParsers.Transactions;
using QuickParser.Classes;
using QuickParser.Interfaces;
using static CoinStory.Core.QuickParsers.Transactions.ColumnDefs.BinanceETHColumn;
using static CoinStory.QuickParsers.Helpers.DefaultTransforms;

namespace CoinStory.Core.QuickParsers.Transactions.Parsers
{
    public class BinanceBETHParser : TransactionParser<BinanceETHColumn>
    {
        protected override Platform Platform => Platform.Binance;

        protected override IParsedRowMapping<Transaction> Mapping => new TransactionMap<Transaction>
        {
            DateMap = new ColumnMap<DateTime>(DATE, ConvertDate),
            AmountOutMap = new ColumnMap<decimal>(AMOUNT, ConvertAmount),
            CurrencyOutMap = new ColumnMap<Currency?>(TOKEN, ConvertCurrency),
            TypeMap = new ColumnMap<TransactionType>((_) => TransactionType.InterestEarned),
            MetadataConstruct = (_) => new
            {
                Description = "ETH 2.0 Staking Rewards"
            }
        };

        public BinanceBETHParser(string fileContent) : base(fileContent)
        {

        }
    }
}