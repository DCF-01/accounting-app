namespace Accounting.MVC.Controllers;

public class ProductController : BaseController
    {
        readonly ApplicationDbContext _ctx;
        readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;

        public ProductController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            IProductService productService)
        {
            _ctx = context;
            _userManager = userManager;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var result = await _productService.GetAsync();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductPost productPost)
        {
            try
            {
                if (productPost.ProductImage != null && productPost.ProductImage.Length > 0)
                {
                    var bytes = await productPost.ProductImage.GetBytesAsync();
                    productPost.CompressedProductImage = bytes.CompressBytes();
                }
                await _productService.CreateAsync(productPost);
                
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return NotFound($"Error: {e}");
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var product = await _ctx.Products.FindAsync(id);

                //remove product and range of related cartproducts
                if (product != null)
                {
                    _ctx.Products.Remove(product);
                    await _ctx.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //Display edited product
        [HttpGet]
        public async Task<IActionResult> Manage(int id)
        {
            try
            {
                //get product with related Groups
                /*var product = await _ctx.Products.Include(product => product.Groups)
                    .Include(product => product.Spec)
                    .Include(product => product.Variations)
                    .Where(product => product.ProductId == id)
                    .FirstOrDefaultAsync();

                var all_specs = await _ctx.Specs
                    .Select(s => s.Name).ToListAsync();

                var all_variations = await _ctx.Variations
                    .Select(item => item.Name).ToListAsync();

                var current_variations = await _ctx.Variations
                    .Where(item => product.Variations.Contains(item))
                    .Select(item => item.Name).ToListAsync();

                var byte_arr_img = product.Img;

                string img = Convert.ToBase64String(byte_arr_img);

                var item = new ProductViewModel
                {
                    Name = product.Name,
                    SKU = product.SKU,
                    Description = product.Description,
                    ShortDescription = product.ShortDescription,
                    Price = product.RetailPrice,
                    SalePrice = product.WholesalePrice,
                    OnSale = product.OnSale.ToString(),
                    InStock = product.InStock.ToString(),
                    CurrentSpec = product.Spec.Name,
                    GetSpecs = all_specs,
                    GetImg = img,
                    GetVariations = all_variations,
                    CurrentVariations = current_variations
                };

                //current active Groups
                item.GetGroups = new List<string>();

                //all available Groups
                item.Groups = _ctx.Groups.Select(item => item.Name).ToArray();

                if (product.Groups != null)
                {
                    foreach (var i in product.Groups)
                    {
                        item.GetGroups.Add(i.Name);
                    }
                }*/
                var result = await _productService.GetAsync(id);

                return View(result);

            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }

        //Update product route
        [HttpPost]
        public async Task<IActionResult> Manage(ProductPut productPut)
        {
            try
            {
                await _productService.UpdateAsync(productPut);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return BadRequest("Bad request: " + e.Message);
            }
        }
    }