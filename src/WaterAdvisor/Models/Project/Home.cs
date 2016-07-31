using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaterAdvisor.Models.Project
{
    public class Home
    {
        public Home()
        {
            project = new Project();
            waterFeed = new WaterList();
        }
        public Project project { get; private set; }
        public WaterList waterFeed { get; private set; }
    }
}
