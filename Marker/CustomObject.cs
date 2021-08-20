using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Marker
{
    public class CustomObject
    {
        public static readonly CustomObject Empty = new CustomObject(null);
        public object Value { get; }
        private readonly Type _type;
        public CustomObject(object value)
        {
            Value = value;
            _type = value?.GetType();
        }
        public override bool Equals(object obj)
        {
            return obj is CustomObject cObj && Value.Equals(cObj.Value);
        }
        public override int GetHashCode() => Value.GetHashCode();
        public T GetValue<T>()
        {
            if (!HasValue<T>()) return default;
            return (T)Value;
        }
        public bool HasValue<T>()
        {
            return _type == typeof(T);
        }
        public bool TryGetValue<T>(out T val)
        {
            val = GetValue<T>();
            return HasValue<T>();
        }
    }
}
