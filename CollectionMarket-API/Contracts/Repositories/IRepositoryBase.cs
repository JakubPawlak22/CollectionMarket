﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IList<T>> GetAll();
        Task<T> GetById(int id);
        Task<bool> Exists(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Save();

    }
}
