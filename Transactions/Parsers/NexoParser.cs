using CoinStory.Core.QuickParsers.Transactions.ColumnDefs;
using CoinStory.Models;
using CoinStory.Models.Enumerations;
using CoinStory.QuickParsers.Transactions;
using QuickParser.Classes;
using QuickParser.Interfaces;
using static CoinStory.Core.QuickParsers.Transactions.ColumnDefs.NexoColumn;
using static CoinStory.QuickParsers.Helpers.DefaultTransforms;

namespace CoinStory.Core.QuickParsers.Transactions.Parsers
{
    public class NexoParser : TransactionParser<NexoColumn>
    {
        protected override Platform Platform => Platform.Nexo;

        // *************************************************************
        // IMPORTANT NOTES:
        // 1. The 'Amount' column of transaction NXTsTb2zqVVzu was manually changed from 'USDX 37.38148044' to 'LTC 0.27168000'
        //    This was done because the CSV doesn't contain the 'amount in' value for exchanges
        // 2. The 'USD Equivalent' column of transaction NXTaNvsG9zHEy was manually changed from '$36.89' to '$37.38'
        //    This was done because the USD Equivalent is the amount converted to USD which isn't the actual 'AmountIn' value
        // *************************************************************
        protected override IParsedRowMapping<Transaction> Mapping => new TransactionMap<Transaction>
        {
            IdentifierMap = new ColumnMap<string>(TRANSACTION),
            DateMap = new ColumnMap<DateTime>(DATE, (value) => ConvertDate(value, "Central European Standard Time")), // Time uses CEST timezone
            CurrencyOutMap = new ColumnMap<Currency?>(CURRENCY, (value, row) => row[TYPE] switch
            {
                "Deposit" or "FixedTermInterest" or "Interest" => ConvertCurrency(value),
                "Exchange" => ConvertCurrency(value.Split('/')[1]),
                _ => null
            }),
            AmountOutMap = new ColumnMap<decimal>(AMOUNT, (value, row) =>
            {
                if (row[TYPE] == "Exchange")
                {
                    string outCurrency = row[CURRENCY].Split('/')[1];
                    string outAmount = outCurrency == "USDX" ? row[USD_EQUIVALENT] : row[AMOUNT].Replace(outCurrency, ""); // READ NOTES

                    return ConvertAmount(outAmount);
                }
                else
                {
                    return row[TYPE] switch
                    {
                        "Deposit" or "FixedTermInterest" or "Interest" => ConvertAmount(value),
                        _ => 0
                    };
                }
            }),
            CurrencyInMap = new ColumnMap<Currency?>(CURRENCY, (value, row) => row[TYPE] switch
            {
                "Withdrawal" => ConvertCurrency(value),
                "Exchange" => ConvertCurrency(value.Split('/')[0]),
                _ => null
            }),
            AmountInMap = new ColumnMap<decimal>(AMOUNT, (value, row) =>
            {
                if (row[TYPE] == "Exchange")
                {
                    string inCurrency = row[CURRENCY].Split('/')[0];
                    string inAmount = inCurrency == "USDX" ? row[USD_EQUIVALENT] : row[AMOUNT].Replace(inCurrency, ""); // READ NOTES

                    return ConvertAmount(inAmount);
                }
                else
                {
                    return row[TYPE] switch
                    {
                        "Withdrawal" => ConvertAmount(value),
                        _ => 0
                    };
                }
            }),
            MetadataConstruct = (row) => new
            {
                USDEquivalent = row[USD_EQUIVALENT],
                Details = row[DETAILS],
                OutstandingLoan = row[OUTSTANDING_LOAN]
            },
            TypeMap = new ColumnMap<TransactionType>(TYPE, (value) => value switch
            {
                "Deposit" => TransactionType.Deposit,
                "Withdrawal" => TransactionType.Withdrawal,
                "Interest" => TransactionType.InterestEarned,
                "Exchange" => TransactionType.Trade,
                "FixedTermInterest" => TransactionType.InterestEarned,
                "LockingTermDeposit" or "UnlockingTermDeposit" => TransactionType.Ignore,
                _ => TransactionType.Unknown
            })
        };

        public NexoParser(string fileContent) : base(fileContent)
        {

        }
    }
}