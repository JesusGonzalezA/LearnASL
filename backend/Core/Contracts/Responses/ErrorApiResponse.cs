
namespace Core.Contracts.Responses
{
    public class ErrorApiResponse<T>
    {
        public ErrorApiResponse(T data)
        {
            Errors = data;
        }

        public T Errors { get; set; }
    }
}