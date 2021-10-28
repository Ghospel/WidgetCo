using System.ComponentModel.DataAnnotations.Schema;

namespace WidgetAndCo.Models
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string id { get; set; }
        public string Text { get; set; }
    }
}