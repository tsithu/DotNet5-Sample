using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace DotNet5WebApi.Models
{
    public abstract class ModelBase
    {       
        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}