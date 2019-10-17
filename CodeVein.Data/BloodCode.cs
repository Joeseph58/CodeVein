using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeVein.Data
{
    public enum BuildStyle { Fighter, Ranger, Caster }



    public class BloodCode
    {

        [Key]

        public int BloodCodeId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        public BuildStyle BuildStyle { get; set; }
        [Required]
        public string BcName { get; set; }
        public string BcSkills { get; set; }
        public string BcDescription { get; set; }
        public string BcLocation { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
