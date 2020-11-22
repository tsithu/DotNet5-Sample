using System;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DotNet5WebApi.Library
{
    public class Validator
    {
        public void Verify<T>(T data){
            // (bool isValid, String errors)
            var isValid = true;
            var sbErrors = new StringBuilder();

            // TODO: implement validation process

            if (!isValid) 
            {
                throw new ValidationException(sbErrors.ToString());
            }
        }
    }
}