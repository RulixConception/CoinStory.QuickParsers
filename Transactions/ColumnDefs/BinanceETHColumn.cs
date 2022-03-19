using QuickParser.Attributes;

namespace CoinStory.Core.QuickParsers.Transactions.ColumnDefs
{
    public enum BinanceETHColumn
    {
        [Column("Date(UTC)")]
        DATE,

        [Column("Token")]
        TOKEN,

        [Column("Amount")]
        AMOUNT
    }
}