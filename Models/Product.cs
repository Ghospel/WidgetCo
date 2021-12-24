using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        public string Image { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}