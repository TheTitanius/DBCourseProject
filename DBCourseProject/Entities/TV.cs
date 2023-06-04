using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBCourseProject.Entities
{
    public class TV
    {
        public int TVid { get; set; }
        public string Name { get; set; }
        public uint Price { get; set; }
        public TVType TVType { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
