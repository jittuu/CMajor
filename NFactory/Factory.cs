using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFactory {
    public static class Factory {
        private static IDictionary<string, object> _definitions = new Dictionary<string, object>();

        public static T Create<T>(string definition) {
            var action = _definitions[definition] as Func<T>;
            return action.Invoke();
        }

        public static void Define<T>(string definition, Func<T> action) {
            _definitions[definition] = action;
        }
    }
}
