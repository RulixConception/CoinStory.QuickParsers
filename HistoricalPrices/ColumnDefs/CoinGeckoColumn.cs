using QuickParser.Attributes;

namespace CoinStory.Core.QuickParsers.HistoricalPrices.ColumnDefs
{
    public enum CoinGeckoColumn
    {
        [Column("snapped_at")]
        SNAPPED_AT,

        [Column("price")]
        PRICE,

        [Column("market_cap")]
        MARKET_CAP,

        [Column("total_volume")]
        TOTAL_VOLUME
    }
}