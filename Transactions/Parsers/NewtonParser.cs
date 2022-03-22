using CoinStory.Core.QuickParsers.Transactions.ColumnDefs;
using CoinStory.Models;
using CoinStory.Models.Enumerations;
using CoinStory.QuickParsers.Transactions;
using QuickParser.Classes;
using QuickParser.Interfaces;
using static CoinStory.Core.QuickParsers.Transactions.ColumnDefs.NewtonColumn;
using static CoinStory.QuickParsers.Helpers.DefaultTransforms;

namespace CoinStory.Core.QuickParsers.Transactions.Parsers
{
    public class NewtonParser : TransactionParser<NewtonColumn>
    {
        protected override Platform Platform => Platform.Newton;

        protected override IParsedRowMapping<Transaction> Mapping => new TransactionMap<Transaction>
        {
            DateMap = new ColumnMap<DateTime>(DATE, (value) => ConvertDate(value, "Eastern Standard Time")),
            AmountOutMap = new ColumnMap<decimal>(RECEIVED_QUANTITY, ConvertAmount),
            CurrencyOutMap = new ColumnMap<Currency?>(RECEIVED_CURRENCY, ConvertCurrency),
            AmountInMap = new ColumnMap<decimal>(SENT_QUANTITY, ConvertAmount),
            CurrencyInMap = new ColumnMap<Currency?>(SENT_CURRENCY, ConvertCurrency),
            FeeMap = new ColumnMap<decimal>(FEE_AMOUNT, ConvertAmount),
            FeeCurrencyMap = new ColumnMap<Currency?>(FEE_CURRENCY, ConvertCurrency),
            TypeMap = new ColumnMap<TransactionType>(TYPE, (value, row) => value switch
            {
                "DEPOSIT" => row[TAG] != "Referral Program" ? TransactionType.Deposit : TransactionType.Reward,
                "WITHDRAWN" => TransactionType.Withdrawal,
                "TRADE" => TransactionType.Trade,
                _ => TransactionType.Unknown
            })
        };

        public NewtonParser(string fileContent) : base(fileContent)
        {

        }
    }
}