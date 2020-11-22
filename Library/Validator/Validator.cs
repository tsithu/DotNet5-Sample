using System;
using System.Text;
namespace DotNet5WebApi.Library
{
    public abstract class Validator
    {
        public virtual (bool, string) Verify(dynamic record){
            var isValid = true;
            var sbErrors = new StringBuilder();

            if (record != null) {
                // TODO: implement default validation process
            }

            return (isValid, sbErrors.ToString());
        }
    }
}