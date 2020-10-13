namespace BizActionExample.Domain.Models.Response
{
    public class ResponseResult<T> : OperationResult
    {
        public T Data { get; set; }
    }
}