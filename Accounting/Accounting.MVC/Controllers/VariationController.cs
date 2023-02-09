using Accounting.Core.Requests;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.Controllers {
    [Authorize(Policy = "Users")]
    public class VariationController : BaseController {
        readonly ApplicationDbContext _ctx;

        public VariationController(ApplicationDbContext context) {
            _ctx = context;
        }

        [HttpGet]
        public IActionResult Index() {

            var variations = _ctx.Variations.ToList();


            return View(variations);
        }
        [HttpGet]
        public IActionResult Create() {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(VariationPost variationPost) {
            var variation = new Variation {
                Name = variationPost.Name,
                Options = variationPost.Options
            };
            await _ctx.Variations.AddAsync(variation);
            await _ctx.SaveChangesAsync();

            return View();
        }


        [HttpDelete]
        public IActionResult Delete([FromRoute] int id) {
            try {
                //delete product
                var variation = _ctx.Variations.Find(id);

                if (variation != null) {
                    _ctx.Variations.Remove(variation);
                    _ctx.SaveChanges();
                    return Ok();
                }
                else {
                    return StatusCode(500);
                }
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

    }
}
