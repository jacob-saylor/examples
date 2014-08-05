using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ConsoleSandbox.Helpers
{
    public static class ObjectJoin
    {
        /// <summary>
        /// Attempts to combine values from two different objects with the same property names into one object
        /// </summary>
        /// <param name="firstObject">The object in which we will return with combined values</param>
        /// <param name="secondObject">The object in which we will be pulling values from</param>
        /// <param name="sourceOverwrite">If we overwrite the value from the first object if one already exists (Default is true)</param>
        /// <returns>The first object with combined values from the second object</returns>
        public static object JoinObjects(object firstObject, object secondObject, bool sourceOverwrite = true)
        {
            if(firstObject == null || secondObject == null)
            {
                throw new ArgumentException("One of the objects passed to JoinObjects is null");
            }

            // Read in first object information
            var firstObjectHash = GetObjectPropertyHash(firstObject);

            // Read in the second object information
            var secondObjectHash = GetObjectPropertyHash(secondObject);

            foreach (var item in firstObjectHash)
            {
                if (secondObjectHash.ContainsKey(item.Key) && (secondObjectHash[item.Key] != null || sourceOverwrite))
                {
                    SetObjectProperty(ref firstObject, item.Key, secondObjectHash[item.Key]);
                }                
            }
            return firstObject;
        }

        /// <summary>
        /// Retrieves the parameter and value list of an inputed object
        /// </summary>
        /// <param name="obj">The object which to identify the properties and values</param>
        /// <returns>A dictionary of the parameter names as the key, and property values as values</returns>
        private static Dictionary<string,object> GetObjectPropertyHash(object obj)
        {
            Type objType = obj.GetType();
            Dictionary<string, object> hashTable = new Dictionary<string, object>();
            foreach (PropertyInfo property in objType.GetProperties())
            {
                hashTable.Add(property.Name, property.GetValue(obj, null));
            }
            return hashTable;
        }

        /// <summary>
        /// Attempts to set a value to a property dynamically 
        /// </summary>
        /// <param name="obj">The object which you are dynamically setting a property to</param>
        /// <param name="propertyName">The name of the objects property to assign the value to</param>
        /// <param name="value">The value to assign to the object's property</param>
        private static void SetObjectProperty(ref object obj, string propertyName, object value)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
            if (propertyInfo != null && value != null && propertyInfo.GetSetMethod() != null)
            {
                try
                {
                    propertyInfo.SetValue(obj, value);
                }
                catch
                {
                    throw new Exception(string.Format("An error occured when attempting to set the value of {0} of type {1}",propertyName,obj.ToString()));
                }
            }
        }
    }
}
