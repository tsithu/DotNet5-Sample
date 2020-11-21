using System;
using System.Collections.Generic;
namespace DotNet5WebApi.Library
{
    public interface IParser<T>
    {   
        string File { get; init; }
        Validator Validator { get; init; }
        IEnumerable<T> Parse(Action<string, dynamic> callback = null);
    }
}