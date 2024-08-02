using System;

namespace Blanketmen.Hypnos.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public byte[] Serialize<T>(T obj)
        {
            try
            {
#if UNITY_32 || UNITY_64
                string json = UnityEngine.JsonUtility.ToJson(obj);
#else
                string json = System.Text.Json.JsonSerializer.Serialize(obj);
#endif
                return System.Text.Encoding.UTF8.GetBytes(json);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public T Deserialize<T>(byte[] data)
        {
            try
            {
                string json = System.Text.Encoding.UTF8.GetString(data);
#if UNITY_32 || UNITY_64
                return UnityEngine.JsonUtility.FromJson<T>(json);
#else
                return System.Text.Json.JsonSerializer.Deserialize<T>(json);
#endif
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}