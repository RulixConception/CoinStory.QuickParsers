using QuickParser.Attributes;

namespace CoinStory.Core.QuickParsers.Transactions.ColumnDefs
{
    public enum BinanceMiscColumn
    {
        [Column("User_ID")]
        USER_ID,

        [Column("UTC_Time")]
        TIME,

        [Column("Account")]
        ACCOUNT,

        [Column("Operation")]
        OPERATION,

        [Column("Coin")]
        COIN,

        [Column("Change")]
        CHANGE,

        [Column("Remark")]
        REMARK
    }
}