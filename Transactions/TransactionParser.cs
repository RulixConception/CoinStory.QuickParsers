using CoinStory.Models.Enumerations;
using CoinStory.Models.Interfaces;
using QuickParser.Classes;

namespace CoinStory.Core.QuickParsers.Transactions
{
    public abstract class TransactionParser<TColumnDef> : ParserBase<ITransaction, TColumnDef>
        where TColumnDef : struct, IConvertible
    {
        protected abstract Platform Platform { get; }

        public TransactionParser(string fileContent) : base(fileContent)
        {

        }

        protected override List<ITransaction> PostProcessing(IEnumerable<ITransaction> transactions)
        {
            return transactions.Where(t => t.Type != TransactionType.Ignore).ToList();
        }

        protected override object[] GetParams() => new object[] { Platform };
    }
}