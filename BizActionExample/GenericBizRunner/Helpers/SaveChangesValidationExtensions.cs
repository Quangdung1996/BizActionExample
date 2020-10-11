using GenericBizRunner.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GenericBizRunner.Helpers
{
    public static class SaveChangesValidationExtensions
    {
        public static async Task<IStatusGeneric> SaveChangesWithOptionalValidationAsync(this DbContext context,
            bool shouldValidate, IGenericBizRunnerConfig config)
        {
            return shouldValidate
                ? await context.SaveChangesWithValidationAsync(config).ConfigureAwait(false)
                : await context.SaveChangesWithExtrasAsync(config).ConfigureAwait(false);
        }

        public static IStatusGeneric SaveChangesWithOptionalValidation(this DbContext context,
            bool shouldValidate, IGenericBizRunnerConfig config)
        {
            return shouldValidate
                ? context.SaveChangesWithValidation(config)
                : context.SaveChangesWithExtras(config);
        }

        public static async Task<IStatusGeneric> SaveChangesWithValidationAsync(this DbContext context, IGenericBizRunnerConfig config = null)
        {
            var status = context.ExecuteValidation();
            return status.HasErrors
                ? status
                : await context.SaveChangesWithExtrasAsync(config, true);
        }

        public static IStatusGeneric SaveChangesWithValidation(this DbContext context, IGenericBizRunnerConfig config = null)
        {
            var status = context.ExecuteValidation();
            return status.HasErrors
                ? status
                : context.SaveChangesWithExtras(config, true);
        }


        private static IStatusGeneric SaveChangesWithExtras(this DbContext context,
            IGenericBizRunnerConfig config, bool turnOffChangeTracker = false)
        {
            var status = config?.BeforeSaveChanges != null
                ? config.BeforeSaveChanges(context)
                : new StatusGenericHandler();
            if (status.HasErrors)
                return status;

            if (turnOffChangeTracker)
                context.ChangeTracker.AutoDetectChangesEnabled = false;
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                var exStatus = config?.SaveChangesExceptionHandler(e, context);
                if (exStatus == null) throw;       //error wasn't handled, so rethrow
                status.CombineStatuses(exStatus);
            }
            finally
            {
                context.ChangeTracker.AutoDetectChangesEnabled = true;
            }

            return status;
        }

        private static async Task<IStatusGeneric> SaveChangesWithExtrasAsync(this DbContext context,
            IGenericBizRunnerConfig config, bool turnOffChangeTracker = false)
        {
            var status = config?.BeforeSaveChanges != null
                ? config.BeforeSaveChanges(context)
                : new StatusGenericHandler();
            if (status.HasErrors)
                return status;

            if (turnOffChangeTracker)
                context.ChangeTracker.AutoDetectChangesEnabled = false;
            try
            {
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                var exStatus = config?.SaveChangesExceptionHandler(e, context);
                if (exStatus == null) throw;       //error wasn't handled, so rethrow
                status.CombineStatuses(exStatus);
            }
            finally
            {
                context.ChangeTracker.AutoDetectChangesEnabled = true;
            }

            return status;
        }

        private static IStatusGeneric ExecuteValidation(this DbContext context)
        {
            var status = new StatusGenericHandler();
            foreach (var entry in
                context.ChangeTracker.Entries()
                    .Where(e =>
                        (e.State == EntityState.Added) ||
                        (e.State == EntityState.Modified)))
            {
                var entity = entry.Entity;
                var valProvider = new ValidationDbContextServiceProvider(context);
                var valContext = new ValidationContext(entity, valProvider, null);
                var entityErrors = new List<ValidationResult>();
                if (!Validator.TryValidateObject(
                    entity, valContext, entityErrors, true))
                {
                    status.AddValidationResults(entityErrors);
                }
            }

            return status;
        }
    }
}