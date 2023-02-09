using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.Controllers
{
    [Authorize(Policy = "Users")]
    public class SpecController : BaseController
    {
        private ApplicationDbContext _ctx;
        private readonly ISpecService _specService;

        public SpecController(ApplicationDbContext applicationDbContext, ISpecService specService)
        {
            _ctx = applicationDbContext;
            _specService = specService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var specs = _ctx.Specs.ToList();
            return View(specs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpecPost specPost)
        {
            if (specPost.Name == null)
            {
                throw new Exception("Name cannot be empty");
            }

            List<string> active_columns = new List<string>();
            foreach (var i in specPost.First)
            {
                if (i != null)
                {
                    active_columns.Add(i);
                }
            }

            //checked arr
            List<string> rest = new List<string>();
            for (int i = 0; i < specPost.Rest.Length; i++)
            {
                if (specPost.Rest[i] != null)
                {
                    rest.Add(specPost.Rest[i]);
                }
            }

            var spec = new Spec
            {
                First = active_columns.ToArray(),
                Rest = rest.ToArray(),
                ItemsPerRow = active_columns.Count(),
                Name = specPost.Name
            };

            await _ctx.Specs.AddAsync(spec);
            await _ctx.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Manage(int id)
        {
            try
            {
                var spec = await _ctx.Specs.FindAsync(id);

                return View(spec);
            }
            catch (Exception e)
            {
                return View("Index", e.Message);
            }
        }

        //refactor into svc
        [HttpPost]
        public async Task<IActionResult> Manage(int id, SpecPut specPut)
        {
            if (specPut.Name == null)
            {
                throw new Exception("Name cannot be empty");
            }

            var spec = await _ctx.Specs.FindAsync(id);
            List<string> active_columns = new List<string>();

            foreach (var i in specPut.First)
            {
                if (i != null)
                {
                    active_columns.Add(i);
                }
            }

            //checked arr
            List<string> rest = new List<string>();
            for (int i = 0; i < specPut.Rest.Length; i++)
            {
                if (specPut.Rest[i] != null)
                {
                    rest.Add(specPut.Rest[i]);
                }
            }

            spec.First = active_columns.ToArray();
            spec.Rest = rest.ToArray();
            spec.ItemsPerRow = active_columns.Count();
            spec.Name = specPut.Name;

            await _ctx.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                //delete product
                var spec = _ctx.Specs.Find(id);

                if (spec != null)
                {
                    _ctx.Specs.Remove(spec);
                    await _ctx.SaveChangesAsync();
                    return Ok();
                }

                return StatusCode(404);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}