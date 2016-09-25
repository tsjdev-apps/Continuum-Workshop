using System;
using System.Threading;
using System.Threading.Tasks;
using RandomUser.Portable.Utils;

namespace RandomUser.Portable.Interfaces.Service
{
    public interface IHttpClientService
    {
        Task<HttpClientServiceResponse<string>> GetStringAsync(Uri uri, CancellationToken c);
    }
}