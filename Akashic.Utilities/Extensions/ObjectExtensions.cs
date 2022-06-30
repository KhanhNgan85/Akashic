/*****************************************************************************/
/* Build  : 11-Mar-2016                                                      */
/* Author : www.kn.team                                                      */
/*****************************************************************************/
using System.Runtime.Serialization;

namespace Akashic.Utilities.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsSerializable(this object obj)
        {
            var result = true;
            if (obj is ISerializable)
                return result;
            else
                result = Attribute.IsDefined(obj.GetType(), typeof(SerializableAttribute));
            return result;
        }       
    }
}
