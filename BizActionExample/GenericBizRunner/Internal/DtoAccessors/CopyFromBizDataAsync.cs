﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GenericBizRunner.Internal.DtoAccessors
{
    internal class CopyFromBizDataAsync<TBizOut, TDtoOut>
         where TBizOut : class
         where TDtoOut : GenericActionFromBizDtoAsync<TBizOut, TDtoOut>, new()
    {
        private readonly TDtoOut _dtoInstance = new TDtoOut();

        public async Task<TDtoOut> CopyFromBizAsync(DbContext db, IMapper mapper, object source)
        {
            return await _dtoInstance.CopyFromBizDataAsync(db, mapper, (TBizOut)source).ConfigureAwait(false);
        }

        public async Task SetupSecondaryOutputDataAsync(DbContext db, object dto)
        {
            await ((TDtoOut)dto).SetupSecondaryOutputDataAsync(db).ConfigureAwait(false);
        }
    }
}