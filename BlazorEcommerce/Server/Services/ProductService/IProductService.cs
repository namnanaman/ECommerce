namespace BlazorEcommerce.Server.Services.ProductService
{
	public interface IProductService
	{
		Task<ServiceResponse<List<Product>>> GetProductsAsync();

		Task<ServiceResponse<Product>> GetProductAsync(int productId);

		Task<ServiceResponse<List<Product>>> GetProductsByCategories(string categoryUrl);

		Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText,int page);
		Task<ServiceResponse<List<String>>> GetProductSearchSuggestions(string searchText);

		Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
	} 
}
