using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Recrutation_exercise.Model
{
    [Table("Product")]
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ProductId { get; set; }
        
        [Required]
        [StringLength(100)]
        public String Name { get; set; }

        [Required]
        public Decimal Price { get; set; }

    }
}
