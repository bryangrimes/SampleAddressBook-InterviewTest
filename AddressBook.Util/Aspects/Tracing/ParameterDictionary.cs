
using System.Collections.Generic;
using System.Text;

namespace AddressBook.Util.Aspects.Tracing
{
    internal class ParameterDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var key in Keys)
            {
                sb.AppendFormat("({0} = {1})", key, this[key]);
            }
            return sb.ToString();
        }

    }
}
