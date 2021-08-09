using System.Threading.Tasks;

using Application.Common.DTOs;

namespace Application.Common.Interfaces
{
    public interface IHttpClient
    {
        Task<ApiResponse> Post(
            string endpoint,
            string requestData,
            string headers
        );
        Task<ApiResponse> Put(
            string endpoint,
            string requestData,
            string headers
        );
        Task<ApiResponse> Delete(
            string endpoint,
            string requestData,
            string headers
        );
    }
}