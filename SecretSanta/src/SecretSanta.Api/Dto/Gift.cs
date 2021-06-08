namespace SecretSanta.Api.Dto
{
    public class Gift
    {
        public int Id { get; set; }
        public string? Title { get; set;}
        public string? Url { get; set;}
        public string? Desc { get; set;}
        public int? Priority { get; set;}
        public Dto.User? Owner { get; set;} = new();

        public static Gift? ToDto(Data.Gift? gift)
        {
            if (gift is null) return null;
            return new Gift
            {
                Title = gift.Title,
                Id = gift.Id,
                Url = gift.Url,
                Desc = gift.Desc,
                Priority = gift.Priority,
            };
        }

        public static Data.Gift? FromDto(Gift? gift)
        {
            if (gift is null) return null;
            return new Data.Gift
            {
                Title = gift.Title ?? "",
                Id = gift.Id,
                Url = gift.Url ?? "",
                Desc = gift.Desc ?? "",
                Priority = gift.Priority ?? 0,
                Owner = User.FromDto(gift.Owner)
            };
        }
    }
}
