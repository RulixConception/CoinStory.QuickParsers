using CoinStory.Core.QuickParsers.Transactions.ColumnDefs;
using CoinStory.Models;
using CoinStory.Models.Enumerations;
using CoinStory.QuickParsers.Transactions;
using QuickParser.Classes;
using QuickParser.Interfaces;
using static CoinStory.Core.QuickParsers.Transactions.ColumnDefs.BinanceMiscColumn;
using static CoinStory.QuickParsers.Helpers.DefaultTransforms;

namespace CoinStory.Core.QuickParsers.Transactions.Parsers
{
    public class BinanceMiscParser : TransactionParser<BinanceMiscColumn>
    {
        protected override Platform Platform => Platform.Binance;

        protected override IParsedRowMapping<Transaction> Mapping => new TransactionMap<Transaction>
        {
            DateMap = new ColumnMap<DateTime>(TIME, ConvertDate),
            AmountOutMap = new ColumnMap<decimal>(CHANGE, ConvertAmount),
            CurrencyOutMap = new ColumnMap<Currency?>(COIN, ConvertCurrency),
            TypeMap = new ColumnMap<TransactionType>(OPERATION, (operation) => operation switch
            {
                "Super BNB Mining" or "POS savings interest" or "Commission Rebate" or "Commission History" => TransactionType.InterestEarned,
                _ => TransactionType.Ignore
            }),
            MetadataConstruct = (row) => new
            {
                Description = row[OPERATION]
            }
        };

        public BinanceMiscParser(string fileContent) : base(fileContent)
        {

        }
    }
}