using System.Text;
using UnityEngine;

namespace Extensions
{
    public static class GameObjectExtensions
    {
        // есть миллион разных способов это написать,
        // но я осознанно положусь на оптимизацию StringBuilder
        // и вернусь если будет тормозить
        // первое правило оптимизации - читаемость важнее
        public static string Hierarchy(this GameObject obj)
        {
            if (!obj)
                return "(null)";

            var sb = new StringBuilder();
            var t = obj.transform;

            bool first = true;
            do
            {
                if (first)
                    first = false;
                else
                    sb.Insert(0, "/");

                sb.Insert(0, t.gameObject.name);

                t = t.parent;
            }
            while (t != null);

            return sb.ToString();
        }
    }
}