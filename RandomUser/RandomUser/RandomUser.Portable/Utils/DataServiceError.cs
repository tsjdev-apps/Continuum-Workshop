using System;

namespace RandomUser.Portable.Utils
{
    public class DataServiceError
    {
        public int ErrorCode { get; set; }
        public DataServiceErrorType ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        public Exception InnerExecption { get; set; }

        public DataServiceError(int httpErrorCode, string errorMessage)
        {
            ErrorCode = httpErrorCode;
            MapHttpStatusCodeToDataServiceErrorType(httpErrorCode);
            ErrorMessage = errorMessage;
        }

        public DataServiceError(int httpErrorCode, string errorMessage, Exception innerExeption)
        {
            ErrorCode = httpErrorCode;
            MapHttpStatusCodeToDataServiceErrorType(httpErrorCode);
            ErrorMessage = errorMessage;
            InnerExecption = innerExeption;
        }


        public DataServiceError(DataServiceErrorType errorType, string errorMessage)
        {
            ErrorCode = (int)errorType;
            ErrorType = errorType;
            ErrorMessage = errorMessage;
        }


        private void MapHttpStatusCodeToDataServiceErrorType(int httpErrorCode)
        {
            switch (httpErrorCode)
            {
                case 502:
                    ErrorType = DataServiceErrorType.Unknown;
                    break;
                case 400:
                    ErrorType = DataServiceErrorType.Unknown;
                    break;
                case 504:
                    ErrorType = DataServiceErrorType.NoConnection;
                    break;
                case 500:
                    ErrorType = DataServiceErrorType.Unknown;
                    break;
                case 405:
                    ErrorType = DataServiceErrorType.Unknown;
                    break;
                case 404:
                    ErrorType = DataServiceErrorType.NotFound;
                    break;
                case 501:
                    ErrorType = DataServiceErrorType.Unknown;
                    break;
                case 408:
                    ErrorType = DataServiceErrorType.NoConnection;
                    break;
                case 503:
                    break;
                case 401:
                    ErrorType = DataServiceErrorType.Unauthorized;
                    break;
                default:
                    ErrorType = DataServiceErrorType.Unknown;
                    break;
            }
        }
    }

}