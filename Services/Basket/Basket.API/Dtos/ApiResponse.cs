﻿using System.Net;

namespace Basket.API.Dtos
{
    public class ApiResponse
    {
        // success respone
        public class APISucessResponse
        {
            public APISucessResponse() { }

            public APISucessResponse(HttpStatusCode statusCode, string message, dynamic data)
            {
                StatusCode = statusCode;
                Message = message;
                Data = data;
            }

            public HttpStatusCode StatusCode
            { get; set; }
            public string Message { get; set; }

            public dynamic Data { get; set; }
        }

        // error response
        public class APIErrorResponse
        {
            public APIErrorResponse() { }

            public APIErrorResponse(Guid id, HttpStatusCode statusCode, string message, List<string> errors)
            {
                Guid Id = id;
                StatusCode = statusCode;
                Message = message;
                Errors = errors;
            }
            public Guid Id { get; set; }

            public HttpStatusCode StatusCode
            { get; set; }
            public string Message { get; set; }

            public List<string> Errors { get; set; }
        }
    }
}
