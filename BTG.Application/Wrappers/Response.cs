﻿namespace BTG.Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data, string message = null)
        {
            Successful = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Successful = false;
            Message = message;
        }

        public bool Successful { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
