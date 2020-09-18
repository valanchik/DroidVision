using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YoloDetection
{
    public static class Extensions
    {
        public static bool TryAdd<TKey,TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key)) return false;

            dict.Add(key, value);

            return true;
        }        
    }
}
