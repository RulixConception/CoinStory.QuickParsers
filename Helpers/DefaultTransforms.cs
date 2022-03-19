using CoinStory.Models.Enumerations;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CoinStory.QuickParsers.Helpers
{
    public static class DefaultTransforms
    {
        public static DateTime ConvertDate(string value) => TimeZoneInfo.ConvertTimeToUtc(DateTime.Parse(value.Trim()), TimeZoneInfo.Local);

        public static DateTime ConvertDate(string value, string timeZoneName) => TimeZoneInfo.ConvertTimeToUtc(DateTime.Parse(value.Trim()), TimeZoneInfo.FindSystemTimeZoneById(timeZoneName));

        public static DateTime ConvertDate(long ticks) => DateTimeOffset.FromUnixTimeMilliseconds(ticks).UtcDateTime;

        public static decimal ConvertAmount(string value)
        {
            string fixDecimalSymbol = value.Replace(",", CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
            string cleanedValue = Regex.Replace(fixDecimalSymbol, @"[^0-9.,-eE]", "");

            return !string.IsNullOrWhiteSpace(cleanedValue)
                ? decimal.Parse(cleanedValue, NumberStyles.Float, CultureInfo.InvariantCulture)
                : 0m;
        }

        public static Currency? ConvertCurrency(string value)
        {
            if (value == "BETH") return Currency.ETH;
            if (value == "NANO") return Currency.XNO;
            if (value == "NEXONEXO") return Currency.NEXO;
            if (value == "USDX") return Currency.USD;
            if (value == "CGLD") return Currency.CELO;
            if (value == "LDBTC" || value == "LDDOGE" || value == "LDUSDT" || value == "LDBNB") return null;

            return !string.IsNullOrWhiteSpace(value) ? Enum.Parse<Currency>(value.Trim(), true) : null;
        }

        public static string UseAsIs(string value) => value;
    }
}