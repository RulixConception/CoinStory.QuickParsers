using QuickParser.Attributes;

namespace CoinStory.Core.QuickParsers.Transactions.ColumnDefs
{
    public enum ShakepayColumn
    {
		[Column("Transaction Type")]
		TYPE,

		[Column("Date")]
		DATE,

		[Column("Amount Debited")]
		AMOUNT_DEBITED,

		[Column("Debit Currency")]
		DEBIT_CURRENCY,

		[Column("Amount Credited")]
		AMOUNT_CREDITED,

		[Column("Credit Currency")]
		CREDIT_CURRENCY,

		[Column("Buy / Sell Rate")]
		BUY_SELL_RATE,

		[Column("Direction")]
		DIRECTION,

		[Column("Spot Rate")]
		SPOT_RATE,

		[Column("Source / Destination")]
		SOURCE_DESTINATION,

		[Column("Blockchain Transaction ID")]
		BLOCKCHAIN_TRANSACTION_ID
	}
}