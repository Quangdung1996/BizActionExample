namespace GenericServices.AspNetCore
{
    /// <summary>
    /// This is used to return a message in the response
    /// </summary>
    public class WebApiStatusAndResult<T>
    {
        /// <summary>
        /// This is used to create a Message-plus-results  response from GenericBizRunner
        /// </summary>
        /// <param name="status"></param>
        public WebApiStatusAndResult(GenericBizRunner.IStatusGeneric status, T results)
        {
            Status = status.HasErrors;
            Message = status.Message;
            Results = results;
        }

        /// <summary>
        /// Contains the message taken from the status
        /// </summary>
        public bool Status { get; }

        /// <summary>
        /// Contains the message taken from the status
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The data sent by the Web API
        /// </summary>
        public T Results { get; }
    }
}