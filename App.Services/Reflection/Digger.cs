using System.Collections.Concurrent;
using System.Reflection;

namespace App.Services.Reflection
{
    public static class Digger
    {
        private static readonly ConcurrentDictionary<(Type, string), PropertyInfo> PropertyCache = new ConcurrentDictionary<(Type, string), PropertyInfo>();

        /// <summary>
        /// Belirtilen nesnedeki bir özelliğe, verilen değeri atar. Özelliğin yazılabilir olduğundan emin olur.
        /// </summary>
        /// <param name="source">Değer atanacak nesne.</param>
        /// <param name="propertyName">Değerin atanacağı özelliğin adı.</param>
        /// <param name="value">Özelliğe atanacak değer.</param>
        /// <exception cref="ArgumentNullException">Eğer <paramref name="source"/> veya <paramref name="propertyName"/> parametrelerinden biri null ise.</exception>
        public static void SetObjectValue(object source, string propertyName, object value)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (propertyName == null) throw new ArgumentNullException(nameof(propertyName));

            var type = source.GetType();
            var key = (type, propertyName);

            var property = PropertyCache.GetOrAdd(key, k => k.Item1.GetProperty(k.Item2));

            if (property != null && property.CanWrite)
            {
                property.SetValue(source, value);
            }
        }
    }
}