using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManagement.Application.Common.Result
{
    public class Error
    {
        public string Title { get; } = default!;
        public string Code { get; } = default!;

        public string Description { get; } = default!;
        public ErrorType Type { get; }

        private Error(string title, string code, string description, ErrorType type)
        {
            Title = title;
            Code = code;
            Description = description;
            Type = type;
        }

        public static Error Failure(string title = "General Failure", string code = "General.Failure", string description = "unexpected error ocuared") => new(title, code, description, ErrorType.Failure);
        public static Error Validation(string title = "Validation Error", string code = "General.ValidationError", string description = "unexpected validation error ocuared") => new(title, code, description, ErrorType.Validation);
        public static Error NotFound(string title = "Not Found", string code = "General.NotFound", string description = "can't find the requested data") => new(title, code, description, ErrorType.NotFound);
        public static Error Conflict(string title = "General Conflict", string code = "General.Conflict", string description = "unexpected conflict ocuared") => new(title, code, description, ErrorType.Conflict);
        public static Error Unauthorized(string title = "Unauthorized access", string code = "General.unauthorized", string description = "Access restricted") => new(title, code, description, ErrorType.Unauthorized);
        public static Error Forbidden(string title = "Forbidden Access", string code = "General.Forbidden", string description = "Forbidden Access") => new(title, code, description, ErrorType.Forbidden);

    }

    public enum ErrorType
    {
        Failure,
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
    }
}
