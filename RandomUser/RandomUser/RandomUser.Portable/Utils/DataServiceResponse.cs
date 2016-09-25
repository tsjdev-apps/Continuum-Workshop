namespace RandomUser.Portable.Utils
{
    public class DataServiceResponse<T>
    {
        public DataServiceResponse(T repsonse)
        {
            IsSuccess = true;
            Response = repsonse;
        }

        public DataServiceResponse(DataServiceError error)
        {
            IsSuccess = false;
            Error = error;
        }

        public DataServiceResponse<T> SetSubResponseError<TResponseType>(DataServiceResponse<TResponseType> response)
        {
            if (response.IsSuccess)
                return this;

            Error = response.Error;
            return this;
        }

        public bool IsSuccess { get; private set; }
        public T Response { get; set; }
        public DataServiceError Error { get; set; }
    }
}