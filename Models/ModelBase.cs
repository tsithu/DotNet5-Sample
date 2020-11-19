using System;

namespace DotNet5WebApi.Models
{
    public abstract class ModelBase
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ModifiedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
    }
}