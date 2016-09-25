using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RandomUser.Portable.DTO;
using RandomUser.Portable.Interfaces.Service;
using RandomUser.Portable.Model;
using RandomUser.Portable.Utils;

namespace RandomUser.Portable.Service
{
    public class DataService : IDataService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IMappingService _mappingService;

        private const string BaseUrl = "https://randomuser.me/api/";
        private const string UsersCall = "?results={0}&nat=de";

        public DataService(IHttpClientService httpClientService, IMappingService mappingService)
        {
            _httpClientService = httpClientService;
            _mappingService = mappingService;
        }

        public async Task<DataServiceResponse<IEnumerable<User>>> GetUsersAsync(int count)
        {
            try
            {
                var requestUrl = BaseUrl + string.Format(UsersCall, count);
                var endpoint = new Uri(requestUrl);
                var cToken = new CancellationToken();

                var response = await _httpClientService.GetStringAsync(endpoint, cToken);

                var rootObject =
                    await Task.Run(() => JsonConvert.DeserializeObject<RootObject>(response.Content), cToken);
                var users = await Task.Run(() => _mappingService.MapUsers(rootObject.Results), cToken);

                return new DataServiceResponse<IEnumerable<User>>(users);
            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"DataService.cs | GetUsersAsync | {ex}");
                return new DataServiceResponse<IEnumerable<User>>(new DataServiceError(DataServiceErrorType.NoConnection, "Request to Endpoint failed"));
            }
            catch (WebException ex)
            {
                Debug.WriteLine($"DataService.cs | GetUsersAsync | {ex.Response?.ResponseUri} : {ex}");
                return new DataServiceResponse<IEnumerable<User>>(new DataServiceError(DataServiceErrorType.NoConnection, "Request to Endpoint failed"));
            }
            catch (JsonSerializationException ex)
            {
                Debug.WriteLine($"DataService.cs | GetUsersAsync | {ex}");
                return new DataServiceResponse<IEnumerable<User>>(new DataServiceError(DataServiceErrorType.DeserializationFailed, "Deserialization Failed"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DataService.cs | GetUsersAsync | {ex}");
                return new DataServiceResponse<IEnumerable<User>>(new DataServiceError(DataServiceErrorType.Unknown, "An unexpected error occured"));
            }
        }
    }
}