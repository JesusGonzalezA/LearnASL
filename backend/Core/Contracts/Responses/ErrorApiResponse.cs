
namespace Core.Contracts.Responses
{
    public class ErrorApiResponse<T>
    {
        public ErrorApiResponse(T data)
        {
            errors = data;
        }

        public T errors { get; set; }
    }
}