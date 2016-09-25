using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RandomUser.Portable.ExtensionMethods
{
    public static class CollectionExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            return collection == null ? new ObservableCollection<T>() : new ObservableCollection<T>(collection);
        }

        public static bool HasItems<T>(this IEnumerable<T> collection)
        {
            return collection.IsNotNull() && collection.Any();
        }

        public static bool IsEmtpy<T>(this IEnumerable<T> collection)
        {
            return collection.IsNotNull() && !collection.Any();
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
            {
                action(item);
            }
        }

        public static void AddRange<T>(this IList<T> list, IEnumerable<T> collection)
        {
            foreach (var item in collection)
                list.Add(item);
        }

        public static void Filter<T>(this IList<T> collection, IEnumerable<T> filteredCollection)
        {
            var enumerable = filteredCollection as T[] ?? filteredCollection.ToArray();
            var itemsToRemove = collection.Except(enumerable).ToArray();

            foreach (var item in itemsToRemove)
                collection.Remove(item);

            foreach (var item in enumerable.Where(item => !collection.Contains(item)))
                collection.Add(item);
        }
    }
}