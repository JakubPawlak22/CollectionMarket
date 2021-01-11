using CollectionMarket_API.Contracts.Validators;
using CollectionMarket_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services.Validators
{
    public abstract class BaseValidator<T> : IValidatorBase<T> where T : class
    {
        protected StringBuilder _stringBuilder;
        protected bool _isValid;
        protected T _entity;
        public async Task<ValidResult> Valid(T entity)
        {
            Init(entity);
            await Valid();
            return CreateValidationResult();
        }

        protected abstract Task Valid();

        protected void Init(T entity)
        {
            _entity = entity;
            _stringBuilder = new StringBuilder(); 
            _isValid = true;
        }
        protected ValidResult CreateValidationResult()
        {
            string message = _stringBuilder.ToString();
            var result = new ValidResult
            {
                Message = message,
                IsValid = _isValid
            };
            return result;
        }

        protected void AddError(string errorMessage)
        {
            _isValid = false;

            if (_stringBuilder.Length > 0)
                _stringBuilder.Append(", ");
            _stringBuilder.AppendLine(errorMessage);
        }
    }
}
