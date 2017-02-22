using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TodoApi.Models
{
    public class TodoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Key { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Owner { get; set; }
    }
}
