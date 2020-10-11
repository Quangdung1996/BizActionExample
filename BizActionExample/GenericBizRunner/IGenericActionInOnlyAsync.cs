using System.Threading.Tasks;

namespace GenericBizRunner
{
    public interface IGenericActionInOnlyAsync<in TIn> : IBizActionStatus
    {
        /// <summary>
        /// Async method containing business logic that will be called
        /// </summary>
        /// <param name="inputData"></param>
        Task BizActionAsync(TIn inputData);
    }
}