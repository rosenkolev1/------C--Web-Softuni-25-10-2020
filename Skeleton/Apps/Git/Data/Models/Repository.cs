using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Git.Data.Models
{
    public class Repository
    {
        [Key]
        public string Id { get; set; }
        [MaxLength(10)]
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        [ForeignKey("Owner")]
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<Commit> Commits { get; set; }
    }
}