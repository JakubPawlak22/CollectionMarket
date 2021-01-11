using CollectionMarket_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts.Validators
{
    public interface IValidatorBase<T> where T : class
    {
        Task<ValidResult> Valid(T entity);
    }
}
