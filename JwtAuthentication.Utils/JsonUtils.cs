using Newtonsoft.Json;

namespace JwtAuthentication.Utils
{
    public static class JsonUtils
    {
        public static bool Object2Json(string filename, object oObject, out string result)
        {
            result = string.Empty;

            try
            {
                string json = JsonConvert.SerializeObject(oObject);

                File.WriteAllText(filename, json);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public static bool Json2Object<T>(string filename, out T oObject, out string result)
        {
            result = string.Empty;

            try
            {
                if (!File.Exists(filename))
                {
                    result = $"'{filename}' Does Not Exist";

                    oObject = default;
                    return false;
                }

                string json = File.ReadAllText(filename);

                if (string.IsNullOrEmpty(json))
                {
                    result = $"'{filename}' Does Not Contain JSON Contence";

                    oObject = default;
                    return false;
                }

                oObject = JsonConvert.DeserializeObject<T>(json);
                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                oObject = default;
                return false;
            }
        }
    }
}
