using System.Threading.Tasks;
using Application.Common.DTOs;
using Application.Common.Interfaces;

namespace Infrastructure.HttpClient
{
    public class FakeHttpClient: IHttpClient
    {
        public Task<ApiResponse> Post(string endpoint, string requestData, string headers)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse> Put(string endpoint, string requestData, string headers)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse> Delete(string endpoint, string requestData, string headers)
        {
            throw new System.NotImplementedException();
        }
    }
}