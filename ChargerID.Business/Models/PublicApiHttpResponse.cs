using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ChargerID.Business.Models
{
    public class PublicApiBaseHttpResponse
    {
        [JsonProperty("code")]
        [Required]
        public string Code { get; set; }

        [JsonProperty("message")]
        [Required]
        public string Message { get; set; }
    }

    public class PublicApiHttpResponseWithData<T> : PublicApiBaseHttpResponse
    {
        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        [Required]
        public T Data { get; set; }
    }

    [JsonObject("publicApiHttpResponse")]
    public class PublicApiHttpResponse<T> : PublicApiHttpResponseWithData<T>
    {
        [JsonProperty("debugInfo", NullValueHandling = NullValueHandling.Ignore)]
        public DebugInfo DebugInfo { get; set; }
    }

    [JsonObject("debugInfo")]
    public class DebugInfo
    {
        [JsonProperty("requestingHost")]
        public string RequestingHost { get; set; }

        [JsonProperty("serverName")]
        public string ServerName { get; set; }

        [JsonProperty("responseTimeMs")]
        public int ResponseTimeMs { get; set; }
    }

    public enum PublicApiCommonErrorCode : int
    {
        [Description("Success")]
        SUCCESS = 0,

        [Description("Something went wrong")]
        FAILURE = 1,

        [Description("Forbidden")]
        FORBIDDEN = 2,

        [Description("Bad request")]
        BAD_REQUEST = 3,

        [Description("Something went wrong")]
        INTERNAL_SERVER_ERROR = 4,

        [Description("Unauthorized")]
        UNAUTHORIZED = 5,

        [Description("Not Found")]
        NOT_FOUND = 6
    }

    public class EnumUtils<T>
    {
        public static string GetName(T enumValue)
        {
            return Enum.GetName(typeof(T), enumValue);
        }

        public static string GetDescription(T enumValue)
        {
            FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

            if (fi != null)
            {
                object[] attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return string.Empty;
        }
    }
}