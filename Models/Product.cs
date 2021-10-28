using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WidgetAndCo.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        public string Image { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}