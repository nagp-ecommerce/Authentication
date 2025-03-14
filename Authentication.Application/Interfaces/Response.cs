﻿namespace ProductService.Application.Interfaces
{
    public record Response
    {
        public bool Success { get; init; }
        public string Message { get; init; } = string.Empty;
    }
}