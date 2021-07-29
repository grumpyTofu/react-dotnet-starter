using WebApi.Entities;
using System;

namespace WebApi.Models
{
    public class ApiResponse<T>
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(bool error, string message, T data = default(T))
        {
            Error = error;
            Message = message;
            Data = data;
        }
    }
}