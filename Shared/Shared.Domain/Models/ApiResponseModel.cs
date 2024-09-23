using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Models
{
    public class ApiResponseModel<T>
    {
        public bool Success { get; set; }
        public string[]? Message { get; set; }
        public T? Data { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StatusCode { get; set; }

        public ApiResponseModel()
        {

        }
        public ApiResponseModel(T data, bool success, int statusCode, params string[] message)
        {
            Success = success;
            Message = message;
            Data = data;
            StatusCode = statusCode;
            CreatedAt = DateTime.UtcNow;
        }
        public static ApiResponseModel<T> CreateSuccess<T>(T data)
        {
            return new ApiResponseModel<T>(data, true, 200, null);
        }

        public static ApiResponseModel<T> CreateFailure<T>(params string[] message)
        {
            return new ApiResponseModel<T>(default(T), false, 400, message);
        }

        public static ApiResponseModel<T> CreateNotFound<T>(params string[] message)
        {
            return new ApiResponseModel<T>(default(T), false, 404, message);
        }

        public static ApiResponseModel<T> CreateServerError<T>(params string[] message)
        {
            return new ApiResponseModel<T>(default(T), false, 500, message);
        }
    }
}
