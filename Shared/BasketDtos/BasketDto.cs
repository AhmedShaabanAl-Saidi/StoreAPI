namespace Shared.BasketDtos
{
    public record BasketDto
    {
        public string Id { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; }
    }
}
