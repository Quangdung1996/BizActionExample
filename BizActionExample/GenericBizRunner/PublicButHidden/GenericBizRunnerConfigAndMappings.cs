﻿using AutoMapper;
using GenericBizRunner.Configuration;
using System;

namespace GenericBizRunner.PublicButHidden
{
    /// <summary>
    /// This holds the BizRunner configuration and the AutoMapper profiles for the ToBiz and FromBiz mappings
    /// </summary>
    public interface IWrappedBizRunnerConfigAndMappings
    {
        /// <summary>
        /// This holds the config for BizRunner
        /// </summary>
        IGenericBizRunnerConfig Config { get; }

        /// <summary>
        /// This holds the mappings from DTOs input to the business logic
        /// </summary>
        IMapper ToBizIMapper { get; }

        /// <summary>
        /// This holds the mappings from the business logic output to DTOs
        /// </summary>
        IMapper FromBizIMapper { get; }
    }

    /// <inheritdoc />
    public class WrappedBizRunnerConfigAndMappings : IWrappedBizRunnerConfigAndMappings
    {
        internal WrappedBizRunnerConfigAndMappings(IGenericBizRunnerConfig config, MapperConfiguration toBizMapping, MapperConfiguration fromBizMapping)
        {
            Config = config ?? throw new ArgumentNullException(nameof(config));
            ToBizIMapper = toBizMapping?.CreateMapper() ?? throw new ArgumentNullException(nameof(toBizMapping));
            FromBizIMapper = fromBizMapping?.CreateMapper() ?? throw new ArgumentNullException(nameof(fromBizMapping));
        }

        /// <inheritdoc />
        public IGenericBizRunnerConfig Config { get; }

        /// <inheritdoc />
        public IMapper ToBizIMapper { get; }

        /// <inheritdoc />
        public IMapper FromBizIMapper { get; }
    }
}