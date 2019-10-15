using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeVein.Data
{
    public class Builds
    {
        [Key]
        public int BuildId { get; set; }
        [Required]
        public BuildStyle BuildStyle { get; set; }
        [Required]
        public string BuildName { get; set; }
        [Required]
        public string BuildCode { get; set; } //needs to be a drop down to select from bloodcode list by codes name
        [Required]                            
        public string BuildWeapon { get; set; } //needs to be a drop down to select from Weapon list by weapons name
        [Required]
        public string BuildSkills { get; set; }
        [Required]
        public string BuildDescription { get; set; } //Needs to be a formbox with a max of 800 characters
        public Guid OwnerId { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
