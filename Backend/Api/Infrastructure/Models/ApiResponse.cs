﻿using System.Net;

namespace Api.Infrastructure.Models;

public class ApiResponse
{
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public List<string>? Errors { get; set; }
}

public class ApiResponse<TResult> : ApiResponse where TResult : class
{
    public TResult? Data { get; set; }
}