using QuickParser.Attributes;

namespace CoinStory.Core.QuickParsers.Transactions.ColumnDefs
{
    public enum NexoColumn
    {
		[Column("Transaction")]
		TRANSACTION,

		[Column("Type")]
		TYPE,

		[Column("Currency")]
		CURRENCY,

		[Column("Amount")]
		AMOUNT,

		[Column("USD Equivalent")]
		USD_EQUIVALENT,

		[Column("Details")]
		DETAILS,

		[Column("Outstanding Loan")]
		OUTSTANDING_LOAN,

		[Column("Date / Time")]
		DATE
	}
}