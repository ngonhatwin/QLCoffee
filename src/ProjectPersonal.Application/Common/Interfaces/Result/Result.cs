using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Common.Interfaces.Result
{
    public class Result<T> : IResult<T>, IError
    {
        [JsonProperty(Order = 1)]
        public int Code { get; set; }
        [JsonProperty(Order = 3)]
        public T Data { get; set; }
        [JsonProperty(Order = 2)]
        public string Message { get; set; }
        [JsonProperty(Order = 4)]
        public List<string> Errors { get; set; }
        public Result() { }
        public Result(int code, string message)
        {
            Code = code;
            Message = message;
        }
        public Result(int code, string message, T data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
        public Result(int code, string message, T data,  List<string> errors) 
        { 
            Code = code;
            Message = message;    
            Errors = errors;
            Data = data;
        }
    }
}
