using QuickParser.Attributes;

namespace CoinStory.Core.QuickParsers.Transactions.ColumnDefs
{
    public enum CoinbaseColumn
    {
        [Column("Timestamp")]
        TIMESTAMP,

        [Column("Transaction Type")]
        TYPE,

        [Column("Asset")]
        ASSET,

        [Column("Quantity Transacted")]
        QUANTITY,

        [Column("Spot Price Currency")]
        SPOT_PRICE_CURRENCY,

        [Column("Spot Price at Transaction")]
        SPOT_PRICE,

        [Column("Subtotal")]
        SUBTOTAL,

        [Column("Total (inclusive of fees)")]
        TOTAL,

        [Column("Fees")]
        FEES,

        [Column("Notes")]
        NOTES
    }
}