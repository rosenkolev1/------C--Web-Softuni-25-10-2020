using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Git.Data.Models
{
    public class Commit
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public string RepositoryId { get; set; }
        public virtual Repository Repository { get; set; }

    }
}