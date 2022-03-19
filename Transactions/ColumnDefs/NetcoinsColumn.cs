using QuickParser.Attributes;

namespace CoinStory.Core.QuickParsers.Transactions.ColumnDefs
{
    public enum NetcoinsColumn
    {
        [Column("Created At Pst")]
        CREATED_AT,

        [Column("Action")]
        ACTION,

        [Column("Increase Amount")]
        INCREASE_AMOUNT,

        [Column("Increase Currency")]
        INCREASE_CURRENCY,

        [Column("Decrease Amount")]
        DECREASE_AMOUNT,

        [Column("Decrease Currency")]
        DECREASE_CURRENCY,

        [Column("Subtotal")]
        SUBTOTAL,

        [Column("Fee")]
        FEE,

        [Column("Rate")]
        RATE,

        [Column("Wallet")]
        WALLET,

        [Column("Memo")]
        MEMO
    }
}