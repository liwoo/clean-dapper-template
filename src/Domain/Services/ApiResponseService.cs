namespace Domain.Services
{
    public class ApiResponseService
    {
        int StatusCode { get; }

        public ApiResponseService(int statusCode)
        {
            StatusCode = statusCode;
        }

        public bool IsUnreturnableResponse
        {
            get { return !IsReturnableResponse; }
        }

        public bool IsReturnableResponse
        {
            get
            {
                return
                    (StatusCode > 199 && StatusCode < 300)
                    || (StatusCode > 399 && StatusCode < 501);
            }
        }
    }
}