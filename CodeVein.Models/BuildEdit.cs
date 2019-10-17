﻿using CodeVein.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeVein.Models
{
    public class BuildEdit
    {

        public int BuildId { get; set; }
        public BuildStyle BuildStyle { get; set; }
        public string BuildName { get; set; }
        public string BuildCode { get; set; }  //needs to be a drop down to select from bloodcode list
        public string BuildWeapon { get; set; } //needs to be a drop down to select from Weapon list
        public string BuildSkills { get; set; }
        public string BuildDescription { get; set; } //Needs to be a formbox with a max of 800 characters



    }
}
