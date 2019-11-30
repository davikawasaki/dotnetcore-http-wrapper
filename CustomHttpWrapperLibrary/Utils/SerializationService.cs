using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace CustomHttpWrapperLibrary.Utils
{
    /// <summary>
    /// Service to serialize and deserialize object to/from JSON.
    /// </summary>
    public class SerializationService
    {
        /// <summary>
        /// Get an object and serialize it to JSON format according to a specific <see cref="Encoding"/>.
        /// </summary>
        /// <param name="objData">Dynamic object with <see cref="System.Runtime.Serialization.DataMemberAttribute"/> properties to be serialized.</param>
        /// <param name="objType">Object's <see cref="Type"/> used for identification in <see cref="DataContractJsonSerializer"/>.</param>
        /// <param name="serializationEnc">(Optional) Type of <see cref="Encoding"/> used to read the object in a <see cref="StreamReader"/>.</param>
        /// <returns>Object serialized into a JSON format in a string.</returns>
        public static string SerializeObjectToJSON(object objData, Type objType, Encoding serializationEnc = null)
        {
            try
            {
                string objDataAsJson = "";
                if (serializationEnc == null) serializationEnc = Encoding.UTF8;

                // Serialize the object from a serializer and get a memory stream
                DataContractJsonSerializer ser = new DataContractJsonSerializer(objType);
                using (MemoryStream ms = new MemoryStream())
                {
                    // Write the serialized object to a memory stream
                    ser.WriteObject(ms, objData);
                    ms.Position = 0;

                    // Get a stream reader and read the serialized object from a memory stream to a text string.
                    using (StreamReader sr = new StreamReader(ms, serializationEnc))
                    {
                        objDataAsJson = sr.ReadToEnd();
                        sr.Close();
                    }

                    ms.Close();
                }

                // Finally return the serialized object in the required JSON format
                return objDataAsJson;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} [ERROR] Unable to serialize object to JSON. Exception details: {ex.ToString()}");
                throw ex;
            }
        }

        /// <summary>
        /// Get a JSON string and deserialize it to a dynamic &lt;T&gt; object or an override <see cref="Type"/> one.
        /// </summary>
        /// <typeparam name="T">Dynamic type param to be extracted its respective properties.</typeparam>
        /// <param name="json">JSON data in a string format.</param>
        /// <param name="serializationEnc">(Optional) Type of <see cref="Encoding"/> used to put the object bytes to a buffer in a <see cref="MemoryStream"/>.</param>
        /// <param name="objTypeOverride">(Optional) Override of object <see cref="Type"/> to be used as object reference to <see cref="DataContractJsonSerializer"/>.</param>
        /// <returns>Dynamic object deserialized from a JSON string.</returns>
        public static T DeserializeObjectFromJSON<T>(string json, Encoding serializationEnc = null, Type objTypeOverride = null)
        {
            try
            {
                if (serializationEnc == null) serializationEnc = Encoding.UTF8;

                using (MemoryStream ms = new MemoryStream(serializationEnc.GetBytes(json)))
                {
                    // Deserialization from JSON
                    DataContractJsonSerializer dser = objTypeOverride == null ? new DataContractJsonSerializer(typeof(T)) : new DataContractJsonSerializer(objTypeOverride);
                    return (T)dser.ReadObject(ms);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now} [ERROR] Unable to deserialize object from JSON. Exception details: {ex.ToString()}");
                throw ex;
            }
        }
    }
}
