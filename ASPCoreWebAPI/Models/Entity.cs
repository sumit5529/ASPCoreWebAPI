using System.Collections.Generic;

namespace ASPCoreWebAPI.Models
{
    public class Entity : IEntity
    {
        public List<Address>? Addresses { get; set; }
        public List<Date> Dates { get; set; } = new List<Date>();
        public bool Deceased { get; set; }
        public string? Gender { get; set; }
        public string Id { get; set; } = string.Empty;
        public List<Name> Names { get; set; } = new List<Name>();
       
    }
}
