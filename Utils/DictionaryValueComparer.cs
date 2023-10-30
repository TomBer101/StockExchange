using RitzpaStockExchange.Models;
using System.Diagnostics.CodeAnalysis;

namespace RitzpaStockExchange.Utils
{
    public class DictionaryValueComparer : IEqualityComparer<Dictionary<string, Holding>>
    {
        public bool Equals(Dictionary<string, Holding>? x, Dictionary<string, Holding>? y)
        {
            foreach (var item in x)
            {
                if(y.ContainsKey(item.Key) == false || y[item.Key].Amount != item.Value.Amount || y[item.Key].Balance != item.Value.Balance)
                    return false;
            }

            return true;
        }

        public int GetHashCode([DisallowNull] Dictionary<string, Holding> obj)
        {
            unchecked
            {
                int hash = 17;

                foreach (var kvp in obj)
                {
                    hash = hash * 23 + kvp.Key.GetHashCode();
                    hash = hash * 23 + kvp.Value.GetHashCode();
                }

                return hash;
            }
        }
    }
}
