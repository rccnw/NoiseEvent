using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class NoiseEventEntity : BaseEntity
    {
        public Guid NoiseEventId { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
