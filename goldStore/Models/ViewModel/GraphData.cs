using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goldStore.Models.ViewModel
{
    public class GraphData
    {
        public string label { get; set; }
        public string value { get; set; }

        public GraphData(string label, string value)
        {
            this.label = label;
            this.value = value;
        }

        public GraphData()
        {
        }
    }
}