﻿using GenericBizRunner.PublicButHidden;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GenericBizRunner
{
    internal abstract class ActionServiceBase
    {
        protected ActionServiceBase(bool requiresSaveChanges, IWrappedBizRunnerConfigAndMappings wrappedConfig)
        {
            RequiresSaveChanges = requiresSaveChanges;
            WrappedConfig = wrappedConfig;
        }

        /// <summary>
        /// This contains info on whether SaveChanges (with validation) should be called after a successful business logic has run
        /// </summary>
        private bool RequiresSaveChanges { get; }

        protected IWrappedBizRunnerConfigAndMappings WrappedConfig { get; }

        /// <summary>
        /// This handled optional save to database with various validation and/or handlers
        /// Note: if it did save successfully to the database it alters the message
        /// </summary>
        /// <param name="db"></param>
        /// <param name="bizStatus"></param>
        /// <returns></returns>
        protected void SaveChangedIfRequiredAndNoErrors(DbContext db, IBizActionStatus bizStatus)
        {

            if (!bizStatus.HasErrors && RequiresSaveChanges)
            {
                bizStatus.CombineStatuses(db.SaveChangesWithOptionalValidation(
                    bizStatus.ShouldValidateSaveChanges(WrappedConfig.Config), WrappedConfig.Config));
                WrappedConfig.Config.UpdateSuccessMessageOnGoodWrite(bizStatus, WrappedConfig.Config);
            }
        }

        /// <summary>
        /// This handled optional save to database with various validation and/or handlers
        /// Note: if it did save successfully to the database it alters the message
        /// </summary>
        /// <param name="db"></param>
        /// <param name="bizStatus"></param>
        /// <returns></returns>
        protected async Task SaveChangedIfRequiredAndNoErrorsAsync(DbContext db, IBizActionStatus bizStatus)
        {
            if (!bizStatus.HasErrors && RequiresSaveChanges)
            {
                bizStatus.CombineStatuses(await db.SaveChangesWithOptionalValidationAsync(
                    bizStatus.ShouldValidateSaveChanges(WrappedConfig.Config), WrappedConfig.Config)
                        .ConfigureAwait(false));
                WrappedConfig.Config.UpdateSuccessMessageOnGoodWrite(bizStatus, WrappedConfig.Config);
            }
        }
    }
}