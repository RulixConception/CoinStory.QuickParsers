using CoinStory.Models;
using CoinStory.Models.Enumerations;
using QuickParser.Classes;

namespace CoinStory.QuickParsers.Transactions
{
    public abstract class TransactionParser<TColumnDef> : ParserBase<Transaction, TColumnDef>
        where TColumnDef : struct, IConvertible
    {
        protected abstract Platform Platform { get; }

        public TransactionParser(string fileContent) : base(fileContent)
        {

        }

        protected override IList<Transaction> PostProcessing(IEnumerable<Transaction> transactions) =>
            transactions.Where(t => t.Type != TransactionType.Ignore).ToList();

        protected override Transaction OnInstantiate() =>
            new Transaction { Platform = Platform, };
    }
}