using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeVein.Data
{
    public class Weapons
    {
        [Key]
        public int WeaponId { get; set; }
        [Required]
        public string WeaponName { get; set; }
        [Required]
        public string WeaponType { get; set; }
        [Required]
        public string WeaponStats { get; set; }
        [Required]
        public string WeaponLocation { get; set; }
        public Guid OwnerId { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }


    }
}
