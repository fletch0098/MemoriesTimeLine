using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTL.DataAccess.Entities
{
    /// <summary>
    /// A Timeline for storing your memories
    /// </summary>
    [Table("TimeLines")]
    public class TimeLine : BaseEntity
    {
        /// <summary>
        /// The name of your TimeLine
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        /// <summary>
        /// A description of your TimeLine
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Description { get; set; }

        /// <summary>
        /// The foreignkey to the AppUser if its a private TimeLine.  Null = Public
        /// </summary>
        public int? AppUserId { get; set; }

        public TimeLine()
        {
            this.Modified();
        }
    }
}
