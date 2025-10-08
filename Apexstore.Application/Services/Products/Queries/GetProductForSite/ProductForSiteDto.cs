namespace ApexStore.Application.Services.Products.Queries.GetProductForSite
{
    public class ProductForSiteDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ImageSrc { get; set; }
        public int Star { get; set; }
        public int Price { get; set; }
        public string Slug { get; set; }
    }

}
