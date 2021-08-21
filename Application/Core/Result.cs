using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Application.Core
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public T Value { get; set; }

        public int StatusCode { get; set; }

        public Dictionary<string, string[]> Errors = new Dictionary<string, string[]>();

        public static Result<T> Success(T value) => new Result<T> {IsSuccess = true, Value = value, StatusCode = StatusCodes.Status200OK};

        public static Result<T> Success(T value, int statusCode) => new Result<T> {IsSuccess = true, Value = value, StatusCode = statusCode};

        public static Result<T> Failure(string error) => new Result<T> {IsSuccess = false, Errors = new Dictionary<string, string[]>{{"error", new []{error}}}, StatusCode = StatusCodes.Status500InternalServerError};

        public static Result<T> Failure(string error, int statusCode) => new Result<T> {IsSuccess = false, Errors = new Dictionary<string, string[]>{{"error", new []{error}}}, StatusCode = statusCode};

        public static Result<T> Failure(Dictionary<string, string[]> errors) => new Result<T> {IsSuccess = false, Errors = errors, StatusCode = StatusCodes.Status500InternalServerError};

        public static Result<T> Failure(Dictionary<string, string[]> errors, int statusCode) => new Result<T> {IsSuccess = false, Errors = errors, StatusCode = statusCode};
    }
}