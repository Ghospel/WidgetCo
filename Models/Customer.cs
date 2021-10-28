using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WidgetAndCo.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}