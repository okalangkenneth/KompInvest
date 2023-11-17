using System.ComponentModel.DataAnnotations;

namespace KompInvest.Models
{
    public class Resource
    {
        [Key]
        public int ResourceID { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [Required, StringLength(1000)]
        public string Description { get; set; }

        [Required, StringLength(50)]
        public string ResourceType { get; set; }

        [Required, Url]
        public string Link { get; set; }

        [StringLength(200)]
        public string ThumbnailImage { get; set; }
    }

}
