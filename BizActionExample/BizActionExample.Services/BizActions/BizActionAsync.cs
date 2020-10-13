using AutoMapper;
using BizActionExample.Domain.Abstracts;
using GenericBizRunner;
using System;
using System.Diagnostics.Contracts;
using System.Linq;

namespace BizActionExample.Services.BizActions
{
    internal abstract class BizActionAsync : BizActionStatus
    {
        protected BizActionAsync(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }

        protected void AddError(Notification notification)
        {
            if (notification.HasErrorCode)
            {
                if (notification.Errors.Count > 0)
                {
                    throw new InvalidOperationException("Only support one error message per error code");
                }
                if (string.IsNullOrEmpty(notification.ErrorMessage))
                {
                    throw new InvalidOperationException("When set ErrorCode, ErrorMessage must have value");
                }

                AddErrorCode(notification.ErrorCode, notification.ErrorMessage);
                return;
            }

            Contract.Ensures(notification.Errors.All(e => !string.IsNullOrEmpty(e.ErrorMessage) && e.MemberNames.Count() > 0));

            foreach (var error in notification.Errors)
            {
                AddError(error.ErrorMessage, error.MemberNames.ToArray());
            }
        }
    }
}