﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace GenericBizRunner.Internal.DtoAccessors
{
    internal class CopyFromBizData<TBizOut, TDtoOut>
      where TBizOut : class
      where TDtoOut : GenericActionFromBizDto<TBizOut, TDtoOut>, new()
    {
        private readonly TDtoOut _dtoInstance = new TDtoOut();

        public TDtoOut CopyFromBiz(DbContext db, IMapper mapper, object source)
        {
            return _dtoInstance.CopyFromBizData(db, mapper, (TBizOut)source);
        }

        public void SetupSecondaryOutputData(DbContext db, object dto)
        {
            ((TDtoOut)dto).SetupSecondaryOutputData(db);
        }
    }
}