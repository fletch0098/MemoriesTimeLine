using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTL.DataAccess.Entities
{
    [Table("TimeLines")]
    public class TimeLine : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Description { get; set; }

        public int? AppUserId { get; set; }

        public TimeLine()
        {
            this.Modified();
        }
    }
}
