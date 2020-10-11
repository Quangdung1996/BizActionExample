using GenericBizRunner.Internal;
using GenericBizRunner.PublicButHidden;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GenericBizRunner
{
    internal class ActionServiceInOutAsync<TBizInterface, TBizIn, TBizOut> : ActionServiceBase
    {
        public ActionServiceInOutAsync(bool requiresSaveChanges, IWrappedBizRunnerConfigAndMappings wrappedConfig)
            : base(requiresSaveChanges, wrappedConfig)
        {
        }

        public async Task<TOut> RunBizActionDbAndInstanceAsync<TOut>(DbContext db, TBizInterface bizInstance,
            object inputData)
        {
            var toBizCopier = DtoAccessGenerator.BuildCopier(inputData.GetType(), typeof(TBizIn), true, true, WrappedConfig.Config.TurnOffCaching);
            var fromBizCopier = DtoAccessGenerator.BuildCopier(typeof(TBizOut), typeof(TOut), false, true, WrappedConfig.Config.TurnOffCaching);
            var bizStatus = (IBizActionStatus)bizInstance;

            //The SetupSecondaryData produced errors
            if (bizStatus.HasErrors) return default(TOut);

            var inData = await toBizCopier.DoCopyToBizAsync<TBizIn>(db, WrappedConfig.ToBizIMapper, inputData).ConfigureAwait(false);

            var result = await ((IGenericActionAsync<TBizIn, TBizOut>)bizInstance).BizActionAsync(inData).ConfigureAwait(false);

            //This handles optional call of save changes
            await SaveChangedIfRequiredAndNoErrorsAsync(db, bizStatus).ConfigureAwait(false);
            if (bizStatus.HasErrors) return default(TOut);

            var data = await fromBizCopier.DoCopyFromBizAsync<TOut>(db, WrappedConfig.FromBizIMapper, result).ConfigureAwait(false);
            return data;
        }
    }
}