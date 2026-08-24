using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Common.Result
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public IReadOnlyList<Error> Errors { get; } = default!;// one or more error propagation

        protected Result(bool isSuccess, IReadOnlyList<Error> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Ok() => new(true, []);
        public static Result Failure(IReadOnlyList<Error> errors) => new(false, errors);
        public static Result Failure(Error error) => new(false, [error]);
    }
    public class Result<T> : Result
    {
        public T? Data { get; }

        private Result(bool isSuccess, T? data, IReadOnlyList<Error> errors) : base(isSuccess, errors)
        {
            Data = data;
        }
        private Result(IReadOnlyList<Error> errors) : base(false, errors)
        {

        }



        public static Result<T> Ok(T data) => new(true, data, []);
        public static new Result<T> Failure(IReadOnlyList<Error> errors) => new(errors);
        public static new Result<T> Failure(Error error) => new([error]);
    }


}
