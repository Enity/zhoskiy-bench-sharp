using System.ComponentModel.DataAnnotations;

namespace ZhoskiyBenchSharp.Models
{
    public class Bear
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string KdRatio { get; set; }

        [Required]
        public bool LoveToSuckCocks { get; set; }
    }
}