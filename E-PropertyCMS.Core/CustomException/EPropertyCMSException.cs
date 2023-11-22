
using System;
using Microsoft.Extensions.Logging;

namespace E_PropertyCMS.Core.CustomException
{
    [Serializable]
	public class EPropertyCMSException : Exception
	{
        private readonly ILogger _logger;

        public int ErrorCode { get; set; }
        public string AdditionalInformation { get; set; }

        public EPropertyCMSException()
        {
        }

        public EPropertyCMSException(string message)
            : base(message)
        {
        }

        public EPropertyCMSException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public EPropertyCMSException(string message, int errorCode, string additionalInfo)
             : base(message)
        {
            ErrorCode = errorCode;
            AdditionalInformation = additionalInfo;
        }

        public void LogException()
        {
            _logger.LogError($"Exception: {Message}, ErrorCode: {ErrorCode}, AdditionalInfo: {AdditionalInformation}");
        }
    }
}

