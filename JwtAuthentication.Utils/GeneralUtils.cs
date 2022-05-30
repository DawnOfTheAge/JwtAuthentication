using System.Collections;
using System.Reflection;

namespace JwtAuthentication.Utils
{
    public static class GeneralUtils
    {
        public static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(a1, a2);
        }

        public static List<string?> GetAllPublicConstantValues(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                .Select(x => (string?)x.GetRawConstantValue())
                .ToList();
        }
    }
}
