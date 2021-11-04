using System;
using System.Collections.Generic;
using System.Linq;

namespace FetchAndSaveUdemyCouponsHandler.Shared.Dtos
{
    public class BaseResult
    {
        private bool _isSuccess;
        private List<string> _errors;

        public BaseResult()
        {
            
        }

        public BaseResult(string error)
        {
           AddError(error); 
        }
        
        public bool IsSuccess
        {
            set => _isSuccess = value;
            get
            {
                if (_errors != null && _errors.Any()) return false;
                return _isSuccess;
            }
        }

        public void AddError(string remarks)
        {
            _errors ??= new List<string>();

            _errors.Add(remarks);
        }

        public List<string> GetErrors()
        {
            return _errors == null ? null : new List<string>(_errors);
        }

        public string GetFormattedError()
        {
            if (_errors == null || !_errors.Any()) return null;
            
            return string.Join(Environment.NewLine, _errors);
        }
    }
}