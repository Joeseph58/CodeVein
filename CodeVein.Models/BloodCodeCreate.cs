using CodeVein.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeVein.Models
{
    //public enum BuildStyle { add_Fighter, Ranger, Caster }

    public class BloodCodeCreate
    {
        public int BloodCodeId { get; set; }
        public BuildStyle BuildStyle { get; set; }
        public string BcName { get; set; }
        public string BcSkills { get; set; }
        public string BcDescription { get; set; }
        public string BcLocation { get; set; }
    }
}
