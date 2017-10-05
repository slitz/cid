using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace ChargerID.Business.Models
{
    public static class ResponseHelper
    {
        public static HttpResponseMessage CreateResponse<T>(
            HttpRequestMessage httpRequestMessage,
            HttpStatusCode httpStatusCode,
            T model,
            PublicApiCommonErrorCode errorCode,
            string message = null,
            DebugInfo overrideDebugInfo = null)
            where T : class
        {

            PublicApiHttpResponse<T> response = new PublicApiHttpResponse<T>()
            {
                Code = EnumUtils<PublicApiCommonErrorCode>.GetName(errorCode),
                Message = message ?? EnumUtils<PublicApiCommonErrorCode>.GetDescription(errorCode),
                Data = model
            };

            if (overrideDebugInfo != null)
            {
                response.DebugInfo = overrideDebugInfo;
            }
            else
            {
                response.DebugInfo = null;
            }

            return httpRequestMessage.CreateResponse(httpStatusCode, response, JsonMediaTypeFormatter.DefaultMediaType);
        }
    }
}