namespace RandomUser.Portable.Utils
{
    public class HttpClientServiceResponse<T>
    {
        public bool IsSuccess => Content != null;
        public HttpClientServiceError Error { get; set; }
        public T Content { get; set; }
        public int HttpStatusCode { get; set; }
    }
}