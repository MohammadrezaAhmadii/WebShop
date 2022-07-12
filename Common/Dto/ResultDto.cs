using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto
{
    public class ResultDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
    public class ResultDto<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Date { get; set; }
    }
    public class Error
    {
        public int Code { get; set; }
        public string[] Data { get; set; }
        private Error()
        {
            Code = 0;
            Data = new string[0];
        }
        public static Error None()
        {
            return new Error();
        }
        public static Error WithCode(int code)
        {
            return new Error
            {
                Code = code
            };
        }
        public static Error withData(int code, string[] errorData)
        {
            return new Error
            {
                Code = code,
                Data = errorData
            };
        }
    }
    public class Result
    {
        public bool Success { get; set; }
        public Error Error { get; set; }
        public string Message { get; set; }
        public object ExtraData { get; set; }
        public static Result Successful()
        {
            return new Result
            {
                Success = true,
                Error = Error.None(),
                Message= String.Empty
            };
        }
        public static Result Failed(Error error)
        {
            return new Result
            {
                Success = false,
                Error = error,
                Message = String.Empty
            };
        }
        public static Result Failed(string message)
        {
            return new Result
            {
                Success = false,
                Error = Error.WithCode(1000),
                Message = message
            };
        }
        public static Result Failed(Error error,string message)
        {
            return new Result
            {
                Success = false,
                Error = error,
                Message = message
            };
        }
        public static Result Try(Func<Result> func,Action onException = null)
        {
            ILogger logger = (ILogger)(((HttpContextAccessor)Statistics.WebHost.Services.GetService(typeof(IHttpContextAccessor)))?.HttpContext?.RequestServices?.GetService(typeof(ILogger)));
            try
            {
                return func();
            }
            catch (AppException ex)
            {
                logger?.Error(ex, $"Error code: {ex.Code}\r\n message: {ex.Message}");
                onException?.Invoke();
                return Failed(Error.WithCode(ex.Code));
            }
            catch (Exception exception)
            {
                logger?.Error(exception, "an error occured");
                onException?.Invoke();
                return Failed(Error.WithCode(1000));
            }
        }
        public static async Task<Result> TryAsync(Func<Task<Result>> func, Action onException = null)
        {
            ILogger _logger = (ILogger)(((HttpContextAccessor)Statistics.WebHost.Services.GetService(typeof(IHttpContextAccessor)))?.HttpContext?.RequestServices?.GetService(typeof(ILogger)));
            try
            {
                return await func();
            }
            catch (AppException ex)
            {
                _logger?.Error(ex, $"Error code: {ex.Code}\r\n message: {ex.Message}");
                return Failed(Error.WithCode(ex.Code));
            }
            catch (Exception ex2)
            {
                _logger?.Error(ex2, "an error occured");
                return Failed(Error.WithCode(1000));
            }
        }

    }
    public class AppException : Exception
    {
        public int Code
        {
            get;
            set;
        }

        public new string Message
        {
            get;
            set;
        }

        public AppException(string message)
        {
            Code = 1000;
            Message = message;
        }

        public AppException(int code, string message = "")
        {
            Code = code;
            Message = message;
        }
    }
    public static class Statistics
    {
        public static IWebHost WebHost;
    }
    public class ResultList<T> : Result
    {
        public IEnumerable<T> Item { get; set; }
        public dynamic Data { get; set; }
        public long TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public static ResultList<T>Successful(IEnumerable<T> item, long totalCount=0L, int pageNumber = 0, int pageSize = 0)
        {
            return new ResultList<T>
            {
                Item = item,
                Success = true,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public new static ResultList<T>Failed(Error error)
        {
            return new ResultList<T>
            {
                Item = new List<T>(),
                Success = false,
                Error = error
            };
        }
        public new static ResultList<T>Failed(string message)
        {
            return new ResultList<T>
            {
                Item = new List<T>(),
                Success = false,
                Error = Error.WithCode(1000),
                Message = message
            };
        }
        public new static ResultList<T> Failed(Error error, string message)
        {
            return new ResultList<T>
            {
                Item = new List<T>(),
                Success = false,
                Error = error,
               Message =  message
            };
        }
        public static ResultList<T> Try(Func<ResultList<T>> func, Action onException = null)
        {
            ILogger logger = (ILogger)(((HttpContextAccessor)Statistics.WebHost.Services.GetService(typeof(IHttpContextAccessor)))?.HttpContext?.RequestServices?.GetService(typeof(ILogger)));
            try
            {
                return func();
            }
            catch (AppException ex)
            {
                logger?.Error(ex, $"Error code: {ex.Code}\r\n message: {ex.Message}");
                return Failed(Error.WithCode(ex.Code));
            }
            catch (Exception exception)
            {
                logger?.Error(exception, "an error occured");
                return Failed(Error.WithCode(1000));
            }
        }
        public static async Task<ResultList<T>> TryAsync(Func<Task<ResultList<T>>> func, Action onException = null)
        {
            ILogger _logger = (ILogger)(((HttpContextAccessor)Statistics.WebHost.Services.GetService(typeof(IHttpContextAccessor)))?.HttpContext?.RequestServices?.GetService(typeof(ILogger)));
            try
            {
                return await func();
            }
            catch (AppException ex)
            {
                _logger?.Error(ex, $"Error code: {ex.Code}\r\n message: {ex.Message}");
                onException?.Invoke();
                return Failed(Error.WithCode(ex.Code));
            }
            catch (Exception ex2)
            {
                _logger?.Error(ex2, "an error occured");
                onException?.Invoke();
                return Failed(Error.WithCode(1000));
            }
        }
    }
}
