using AutoMapper;
using System;

namespace GenericBizRunner.PublicButHidden
{
    /// <summary>
    /// This is used to find all ToBiz Dtos
    /// </summary>
    internal interface IGenericActionToBizDto { }

    /// <summary>
    /// This is the abstract class that is used by GenericActionToBizDto class
    /// It contains a method to alter the AutoMapper mapping configuration
    /// </summary>
    /// <typeparam name="TBizIn"></typeparam>
    /// <typeparam name="TDtoIn"></typeparam>
    public abstract class GenericActionToBizDtoSetup<TBizIn, TDtoIn> : IGenericActionToBizDto
        where TBizIn : class
        where TDtoIn : GenericActionToBizDtoSetup<TBizIn, TDtoIn>
    {
        /// <summary>
        /// Override this to provide your own IMappingExpression to the TBizOut to TDtoOut mapping
        /// </summary>
        protected internal virtual Action<IMappingExpression<TBizIn, TDtoIn>> AlterDtoMapping => null;
    }
}