using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChargerID.LocatorServices.Exceptions
{
    public class MissingParameterException : HttpResponseException
    {
        private string ParameterName;

        public MissingParameterException(string parameterName)
            : base(new HttpResponseMessage(HttpStatusCode.BadRequest))
        {
            ParameterName = parameterName;
        }

        public override string Message
        {
            get
            {
                return String.Format("{0} is required", ParameterName);
            }
        }
    }
}