using CoinStory.Core.QuickParsers.Transactions.ColumnDefs;
using CoinStory.Models;
using CoinStory.Models.Enumerations;
using CoinStory.QuickParsers.Transactions;
using QuickParser.Classes;
using QuickParser.Interfaces;
using static CoinStory.Core.QuickParsers.Transactions.ColumnDefs.NetcoinsColumn;
using static CoinStory.QuickParsers.Helpers.DefaultTransforms;

namespace CoinStory.Core.QuickParsers.Transactions.Parsers
{
    public class NetcoinsParser : TransactionParser<NetcoinsColumn>
    {
        protected override Platform Platform => Platform.Netcoins;

        protected override IParsedRowMapping<Transaction> Mapping => new TransactionMap<Transaction>
        {
            DateMap = new ColumnMap<DateTime>(CREATED_AT, (value) => ConvertDate(value, "UTC-11")), // Looks like UTC-11 timezone somehow
            AmountOutMap = new ColumnMap<decimal>(INCREASE_AMOUNT, ConvertAmount),
            CurrencyOutMap = new ColumnMap<Currency?>(INCREASE_CURRENCY, ConvertCurrency),
            AmountInMap = new ColumnMap<decimal>(DECREASE_AMOUNT, (value, row) => ConvertAmount(row[DECREASE_CURRENCY] == "CAD" ? row[SUBTOTAL] : value)),
            CurrencyInMap = new ColumnMap<Currency?>(DECREASE_CURRENCY, ConvertCurrency),
            MetadataConstruct = (row) => new
            {
                Rate = row[RATE],
                Wallet = row[WALLET],
                Memo = row[MEMO],
            },
            TypeMap = new ColumnMap<TransactionType>(ACTION, (value) => value switch
            {
                "limit-sell" or "limit-buy" or "sell" or "buy" => TransactionType.Trade,
                "fund" or "deposit" => TransactionType.Deposit,
                "reward" => TransactionType.Reward,
                "withdraw" => TransactionType.Withdrawal,
                _ => TransactionType.Unknown
            }),
            FeeCurrencyMap = new ColumnMap<Currency?>((row) => row[ACTION] == "withdraw"
                ? ConvertCurrency(row[DECREASE_CURRENCY])
                : ConvertAmount(row[FEE]) == 0m ? null : Currency.CAD),
            FeeMap = new ColumnMap<decimal>(7, ConvertAmount),
        };

        public NetcoinsParser(string fileContent) : base(fileContent)
        {

        }
    }
}