using CoinStory.Core.QuickParsers.Transactions.ColumnDefs;
using CoinStory.Models.Enumerations;
using CoinStory.Models.Interfaces;
using QuickParser.Classes;
using QuickParser.Interfaces;
using static CoinStory.Core.QuickParsers.Transactions.ColumnDefs.ShakepayColumn;
using static CoinStory.QuickParsers.Helpers.DefaultTransforms;

namespace CoinStory.Core.QuickParsers.Transactions.Parsers
{
    public class ShakepayParser : TransactionParser<ShakepayColumn>
    {
        protected override Platform Platform => Platform.Shakepay;

        protected override IParsedRowMapping<ITransaction> Mapping => new TransactionMap<ITransaction>
        {
            DateMap = new ColumnMap<DateTime>(DATE, ConvertDate),
            AmountOutMap = new ColumnMap<decimal>(AMOUNT_CREDITED, ConvertAmount),
            CurrencyOutMap = new ColumnMap<Currency?>(CREDIT_CURRENCY, ConvertCurrency),
            AmountInMap = new ColumnMap<decimal>(AMOUNT_DEBITED, ConvertAmount),
            CurrencyInMap = new ColumnMap<Currency?>(DEBIT_CURRENCY, ConvertCurrency),
            TypeMap = new ColumnMap<TransactionType>(TYPE, (value, row) => value switch
            {
                "purchase/sale" => TransactionType.Trade,
                "crypto cashout" => TransactionType.Withdrawal,
                "fiat funding" or "crypto funding" => TransactionType.Deposit,
                "shakingsats" or "other" or "referral reward" => TransactionType.Reward,
                "peer transfer" => row[DIRECTION] switch
                {
                    "credit" => TransactionType.Deposit,
                    "debit" => TransactionType.Withdrawal,
                    _ => TransactionType.Unknown
                },
                _ => TransactionType.Unknown
            }),
            MetadataConstruct = (row) => new
            {
                BuySellRate = row[BUY_SELL_RATE],
                SpotRate = row[SPOT_RATE],
                SourceDestination = row[SOURCE_DESTINATION],
                BlockchainTransactionId = row[BLOCKCHAIN_TRANSACTION_ID]
            }
        };

        public ShakepayParser(string fileContent) : base(fileContent)
        {

        }
    }
}