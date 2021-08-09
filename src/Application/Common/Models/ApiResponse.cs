namespace Application.Common.DTOs
{
    public class ApiResponse
    {
        public ApiResponse(int status, string body)
        {
            StatusCode = status;
            StringBody = body;
        }

        public int StatusCode { get; }
        public string StringBody { get; }
    }
}