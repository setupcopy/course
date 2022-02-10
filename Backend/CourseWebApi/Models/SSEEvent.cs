using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseWebApi.Models
{
    public class SSEEvent
    {
        public string Name { get; set; }
        public object Data { get; set; }
        public string Id { get; set; }
        public int? Retry { get; set; }

        public SSEEvent(string name,object data)
        {
            Name = name;
            Data = data;
        }
    }
}
