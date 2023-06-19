using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class Extensions
    {
        public static T GetRandomElement<T> (this IEnumerable<T> collection)
        {
            T output = default;

            if (collection.Count() != 0)
                output = collection.ElementAt(Random.Range(0, collection.Count()));

            return output;
        }

        public static T GetElementByGUIDFromCollection<T>(this IEnumerable<T> collection, string guid) where T : IGUIdable
        {
            T output = default;

            foreach (T item in collection)
            {
                if (item.GUID == guid)
                {
                    output = item;
                    break;
                }
            }

            return output;
        }
    }
}
