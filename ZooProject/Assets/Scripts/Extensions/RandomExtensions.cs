using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Extensions
{
    public static class RandomExtensions
    {
        /// <summary>
        /// returns [0,x] [0,y] [0,z]
        /// </summary>
        /// <param name="bounds"></param>
        /// <returns></returns>
        public static Vector3 Randomize(this Vector3 bounds)
        {
            var x = Math.Max(0, bounds.x);
            var y = Math.Max(0, bounds.y);
            var z = Math.Max(0, bounds.z);

            x = Randomize(x);
            y = Randomize(y);
            z = Randomize(z);

            return new Vector3(x, y, z);
        }

        private static float Randomize(float positive)
        {
            if (positive > 0)
                return Random.Range(-positive, positive);
            else
                return 0;
        }

        // не является часто выполняемой функцией, можно не оптимизировать
        public static T ChooseRandomly<T>(this IEnumerable<T> list) where T : class
        {
            if (list == null)
                return default(T); // тут можно поспорить конечно, но я не люблю бросать исключения
            if (list.Count() == 0)
                return default(T); // то же

            var i = Random.Range(0, list.Count());
            return list.ElementAt(i);
        }
    }
}