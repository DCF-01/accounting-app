using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.Controllers;

public class ProductController : BaseController
    {
        readonly ApplicationDbContext _ctx;
        readonly UserManager<User> _userManager;
        private readonly IProductService _productService;

        public ProductController(ApplicationDbContext context, UserManager<User> userManager,
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