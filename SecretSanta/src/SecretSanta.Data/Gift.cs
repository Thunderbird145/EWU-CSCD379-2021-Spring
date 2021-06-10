using System.Collections.Generic;

namespace SecretSanta.Data
{
    public class Gift
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Url { get; set;}
        public string? Desc { get; set;}
        public int Priority { get; set;} = 0;
        public int OwnerID { get; set;} = 0;
    }
}
