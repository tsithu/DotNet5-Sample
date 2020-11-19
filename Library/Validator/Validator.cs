using System;
using System.Text;

namespace DotNet5WebApi.Library
{
    public class Validator
    {
        public (bool isValid, String errors) Verify<T>(T data){
            var isValid = true;
            var sbErrors = new StringBuilder();


            return (isValid, sbErrors.ToString());
        }
    }
}