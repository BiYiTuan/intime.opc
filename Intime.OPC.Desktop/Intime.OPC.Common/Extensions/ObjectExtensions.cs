using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace System
{
    public static class ObjectEx
    {
        /// <summary>
        ///     释放 COM 对象
        /// </summary>
        /// <param name="comObject"></param>
        public static void ReleaseComObject(this object comObject)
        {
            if (comObject == null)
            {
                return;
            }
            Marshal.ReleaseComObject(comObject);
        }

        public static T DeepClone<T>(this T source)
        {
            if (!typeof (T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T) formatter.Deserialize(stream);
            }
        }

        /// <summary>
        ///     将对象转换成字符串，如果对象为空返回  string.Empty;
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>System.String.</returns>
        public static string ToString2(this object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }
    }
}