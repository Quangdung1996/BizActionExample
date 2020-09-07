using GenericBizRunner.Configuration;

namespace GenericBizRunner
{
    /// <summary>
    /// This interface defines all various features for error reporting and status items that the business logic must implement
    /// </summary>
    public interface IBizActionStatus : IStatusGeneric
    {
        /// <summary>
        /// This method is used by GenericBzRunner to work out whether a call to saveChanges should also validate the data
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        bool ShouldValidateSaveChanges(IGenericBizRunnerConfig config);
    }
}