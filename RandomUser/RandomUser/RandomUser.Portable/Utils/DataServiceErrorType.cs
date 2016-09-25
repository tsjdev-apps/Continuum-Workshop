namespace RandomUser.Portable.Utils
{
    public enum DataServiceErrorType
    {
        NoConnection,
        Unauthorized,
        Cancellation,
        NotFound,
        Unknown,
        NoSiteUrl,
        DeserializationFailed,
        ImageLoadingFailed,
        MissingCredentials,
        TransferJobFailed
    }
}