using System.Collections.Generic;
using DotNet5WebApi.Library;

namespace DotNet5WebApi.Library
{
    public interface IParser<T>
    {   
        string File { get; init; }
        Validator Validator { get; init; }
        List<T> Parse();
    }
}