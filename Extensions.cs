using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloDetection
{
    public static class Extensions
    {
        public static bool TryAdd<Key,Value>(this Dictionary<Key, Value> dict, Key key, Value value)
        {
            if (dict.ContainsKey(key)) return false;

            dict.Add(key, value);

            return true;
        }
    }
}
