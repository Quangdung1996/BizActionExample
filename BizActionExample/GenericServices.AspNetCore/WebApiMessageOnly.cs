using GenericBizRunner;

namespace GenericServices.AspNetCore
{
    /// <summary>
    /// This is used to return a message in the response
    /// </summary>
    public class WebApiMessageOnly
    {
        /// <summary>
        /// This is used to create a Message-only response from GenericServices
        /// </summary>
        /// <param name="status"></param>
        public WebApiMessageOnly(StatusGeneric.IStatusGeneric status)
        {
            Message = status.Message;
        }

        /// <summary>
        /// This is used to create a Message-only response from GenericBizRunner
        /// </summary>
        /// <param name="status"></param>
        public WebApiMessageOnly(IStatusGeneric status)
        {
            Message = status.Message;
        }

        /// <summary>
        /// Contains the message taken from the status
        /// </summary>
        public string Message { get; }
    }
}