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
            set
            {
                if (value && _errors != null && _errors.Any()) return;
                _isSuccess = value;
            }
            get => _isSuccess;
        }

        public void AddError(string remarks, Exception e)
        {
            AddError(remarks);
            AddError(e.Message);
        }

        public void AddError(string remarks)
        {
            _errors ??= new List<string>();

            _errors.Add(remarks);
            _isSuccess = false;
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