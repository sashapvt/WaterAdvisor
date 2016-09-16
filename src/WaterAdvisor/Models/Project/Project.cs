using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class Project : ProjectBase
    {
        // User
        [StringLength(450)]
        public string UserId { get; set; }

        // Input water quality
        public Water WaterIn { get; set; }

        // Indexer
        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }
    }
}
