using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThreeLayerArchitecture.BusinessLayer.Result.Enum;

namespace ThreeLayerArchitecture.BusinessLayer.Result
{
    public class ResultService
    {
        public enResultStatus Status { get; }
        public string? ErrorMessage { get; }

        protected ResultService(enResultStatus status, string? errorMessage)
        {
            Status = status;
            ErrorMessage = errorMessage;
        }

        public static ResultService Success() => new(enResultStatus.Success, null);
        public static ResultService Failure(string? errorMessage) => new(enResultStatus.Failure, errorMessage);
        public static ResultService NotFound() => new(enResultStatus.NotFound, null);
    }

    public class ResultService<T> : ResultService
    {
        public T? Data { get; }

        private ResultService(enResultStatus status, T? data, string? errorMessage) : base(status, errorMessage)
        {
            Data = data;
        }

        public static ResultService<T> Success(T? data) => new(enResultStatus.Success, data, null);
        public new static ResultService<T> Failure(string errorMessage) => new(enResultStatus.Failure, default, errorMessage);

        public new static ResultService<T> NotFound() => new(enResultStatus.NotFound, default, null);
    }
}
