namespace Commerceee.Helper
{
    public class ProductResponse
    {
        public List<Core.Entities.Product> products { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }

    }
}
