using GenericBizRunner.Internal;
using GenericBizRunner.PublicButHidden;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GenericBizRunner
{
    internal class ActionServiceInOnlyAsync<TBizInterface, TBizIn> : ActionServiceBase
    {
        public ActionServiceInOnlyAsync(bool requiresSaveChanges, IWrappedBizRunnerConfigAndMappings wrappedConfig)
           : base(requiresSaveChanges, wrappedConfig)
        {
        }

        public async Task RunBizActionDbAndInstanceAsync(DbContext db, TBizInterface bizInstance, object inputData)
        {
            var toBizCopier = DtoAccessGenerator.BuildCopier(inputData.GetType(), typeof(TBizIn), true, true, WrappedConfig.Config.TurnOffCaching);
            var bizStatus = (IBizActionStatus)bizInstance;

            //The SetupSecondaryData produced errors
            if (bizStatus.HasErrors) return;

            var inData = await toBizCopier.DoCopyToBizAsync<TBizIn>(db, WrappedConfig.ToBizIMapper, inputData).ConfigureAwait(false);

            await ((IGenericActionInOnlyAsync<TBizIn>)bizInstance).BizActionAsync(inData).ConfigureAwait(false);

            //This handles optional call of save changes
            await SaveChangedIfRequiredAndNoErrorsAsync(db, bizStatus).ConfigureAwait(false);
            if (bizStatus.HasErrors)
                await toBizCopier.SetupSecondaryDataIfRequiredAsync(db, bizStatus, inputData).ConfigureAwait(false);
        }
    }
}