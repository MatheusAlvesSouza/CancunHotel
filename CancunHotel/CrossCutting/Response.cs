namespace CancunHotel.Api.CrossCutting
{
    public class Response<T>
    {
        public bool IsSuccess { get; }
        public T? Result { get; }
        public string? Error { get; }

        private Response(T result)
        {
            IsSuccess = true;
            Result = result;
        }

        private Response(string error) 
        {
            IsSuccess = false;
            Error = error;
        }

        public static Response<T> Success(T result) 
        {
            return new Response<T>(result);
        }

        public static Response<T> Failure(string error)
        {
            return new Response<T>(error);
        }
    }
}
