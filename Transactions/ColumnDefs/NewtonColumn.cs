using QuickParser.Attributes;

namespace CoinStory.Core.QuickParsers.Transactions.ColumnDefs
{
    public enum NewtonColumn
    {
        [Column("Date")]
        DATE,

        [Column("Type")]
        TYPE,

        [Column("Received Quantity")]
        RECEIVED_QUANTITY,

        [Column("Received Currency")]
        RECEIVED_CURRENCY,

        [Column("Sent Quantity")]
        SENT_QUANTITY,

        [Column("Sent Currency")]
        SENT_CURRENCY,

        [Column("Fee Amount")]
        FEE_AMOUNT,

        [Column("Fee Currency")]
        FEE_CURRENCY,

        [Column("Tag")]
        TAG
    }
}