using BlazorEcommerce.Server.Data;

namespace BlazorEcommerce.Server.Services.ProductService
{
	public class ProductService : IProductService
	{
		private readonly DataContext _context;

		public ProductService(DataContext context)
		{
			_context = context;
		}

        public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _context.Products.Where(x => x.Featured).Include(p=>p.Variants).ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        {
			var response = new ServiceResponse<Product>();
			var product = await _context.Products
					.Include(x => x.Variants)
					.ThenInclude(x => x.ProductType)
					.FirstOrDefaultAsync(p=>p.Id == productId);


			if(product == null)
            {
				response.Success = false;
				response.Message = "Sorry,This Product does not exist.";
            }
            else
            {
				response.Data = product;
            }
			return response; 
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
		{
			var response = new ServiceResponse<List<Product>>
			{
				Data = await _context.Products.Include(x=>x.Variants).ToListAsync()
			};
			return response;
		}

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategories(string categoryUrl)
        {
			var response = new ServiceResponse<List<Product>>
			{
				Data = await _context.Products.Where(x => x.Category.Url.ToLower().Equals(categoryUrl.ToLower())).Include(x => x.Variants).ToListAsync()
			};
			return response; 
        }

		public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
		{
			var products = await FindProductBySearchText(searchText);
			List<string> result = new List<string>();

			foreach (var product in products)
            {
                if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
					result.Add(product.Title);
                }
				if(product.Description != null)
                {
					var punctutation = product.Description.Where(char.IsPunctuation).Distinct().ToArray();
					var words = product.Description.Split().Select(s=>s.Trim(punctutation));


                    foreach (var  word in words)
                    {
						if(word.Contains(searchText,StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
                        {
							result.Add(word);
                        }
                    }
                }
            }
			return new ServiceResponse<List<string>> { Data = result };	
		}


		public async Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText,int page)
        {
            var pageResults = 2f;
            
            var pageCount = Math.Ceiling((await FindProductBySearchText(searchText)).Count / pageResults);
             
            var products = await _context.Products.Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                            ||
                            p.Description.ToLower().Contains(searchText.ToLower())

                            ).Include(p => p.Variants).Skip((page - 1) * (int)pageResults)
                            .Take((int)pageResults)
                            .ToListAsync();
            var response = new ServiceResponse<ProductSearchResult>
            {
                Data = new ProductSearchResult
                {
                    Products = products,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };
            return response;
        }

        private async Task<List<Product>> FindProductBySearchText(string searchText)
        {
            return await _context.Products.Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                            ||
                            p.Description.ToLower().Contains(searchText.ToLower())

                            ).Include(p => p.Variants).ToListAsync();
        }
    }
}
