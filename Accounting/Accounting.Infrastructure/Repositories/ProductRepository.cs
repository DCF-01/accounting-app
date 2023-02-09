using System.Diagnostics;
using System.Text.RegularExpressions;
using Accounting.Common;
using Accounting.Infrastructure.Data;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _ctx;

    public ProductRepository(ApplicationDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<PagedResult<Product>> GetPagedAsync(Guid masterCompanyId, PagingModel pagingModel)
    {
        var query =
            _ctx.Products
                .Include(prd => prd.Spec)
                .Include(prd => prd.Currency)
                .Include(prd => prd.VAT)
                .Where(prd =>
                    prd.SKU.Contains(pagingModel.SearchTerm) ||
                    prd.Name.Contains(pagingModel.SearchTerm) ||
                    prd.ShortDescription.Contains(pagingModel.SearchTerm) ||
                    prd.RetailPrice.ToString().Contains(pagingModel.SearchTerm) ||
                    prd.WholesalePrice.ToString().Contains(pagingModel.SearchTerm)
                )
                .Where(prd => prd.MasterCompanyId == masterCompanyId);

        var result =
            await query
                .Order(pagingModel.SortOrder, pagingModel.SortColumn)
                .Skip(pagingModel.Page * pagingModel.PageSize)
                .Take(pagingModel.PageSize)
                .ToListAsync();

        return new PagedResult<Product>(result, await query.CountAsync());
    }

    public async Task CreateAsync(Guid masterCompanyId, Product product)
    {
        var s = new Stopwatch();
        s.Start();


        product.MasterCompanyId = masterCompanyId;
        product.Spec =
            await _ctx.Specs
                .Where(s => s.MasterCompanyId == masterCompanyId && s.SpecId == product.Spec.SpecId)
                .SingleOrDefaultAsync();

        product.VAT =
            await _ctx.VATs
                .Where(v => v.MasterCompanyId == masterCompanyId && v.VATId == product.VAT.VATId)
                .SingleOrDefaultAsync();

        product.Currency =
            await _ctx.Currencies
                .Where(c => c.MasterCompanyId == masterCompanyId && c.CurrencyId == product.Currency.CurrencyId)
                .SingleOrDefaultAsync();

        product.Groups = await _ctx.Groups.Where(grp => product.Groups.Contains(grp)).ToListAsync();
        product.Variations = await _ctx.Variations.Where(vr => product.Variations.Contains(vr)).ToListAsync();
        _ctx.Products.Add(product);
        await _ctx.SaveChangesAsync();
        s.Stop();
        var r = s.ElapsedMilliseconds;
    }

    public async Task UpdateAsync(Guid masterCompanyId, Product product)
    {
        var grpKeys = product.Groups.Select(x => x.GroupId).ToList();

        var updatedProduct =
            await _ctx.Products
                .Include(prd => prd.Groups)
                .Include(prd => prd.Variations)
                .Include(prd => prd.Spec)
                .Include(prd => prd.Currency)
                .Include(prd => prd.VAT)
                .Where(prd =>
                    prd.MasterCompanyId == masterCompanyId &&
                    prd.ProductId == product.ProductId
                )
                .SingleOrDefaultAsync();

        if (updatedProduct == null)
        {
            throw new ArgumentException($"Product doesn't exist.");
        }

        _ctx.Products.Update(updatedProduct);

        updatedProduct.MasterCompanyId = masterCompanyId;
        updatedProduct.Spec =
            await _ctx.Specs
                .Where(s =>
                    s.MasterCompanyId == masterCompanyId &&
                    s.SpecId == product.Spec.SpecId
                )
                .SingleOrDefaultAsync();

        updatedProduct.VAT =
            await _ctx.VATs
                .Where(v =>
                    v.MasterCompanyId == masterCompanyId &&
                    v.VATId == product.VAT.VATId
                )
                .SingleOrDefaultAsync();

        updatedProduct.Currency =
            await _ctx.Currencies
                .Where(c =>
                    c.MasterCompanyId == masterCompanyId &&
                    c.CurrencyId == product.Currency.CurrencyId
                )
                .SingleOrDefaultAsync();


        updatedProduct.Groups.Clear();

        var groups = await _ctx.Groups
            .Where(grp => grp.MasterCompanyId == masterCompanyId).ToListAsync();

        foreach (var grp in groups)
        {
            if (grpKeys.Contains(grp.GroupId))
            {
                updatedProduct.Groups.Add(grp);
            }
        }

        await _ctx.SaveChangesAsync();
    }


    public async Task<Product> GetAsync(Guid masterCompanyId, int productId = -1)
    {
        Product product = new();


        var masterCompanyGroups = _ctx.Groups.Where(x => x.MasterCompanyId == masterCompanyId);
        var masterCompanySpecs = _ctx.Specs.Where(x => x.MasterCompanyId == masterCompanyId);
        var masterCompanyVaTs = _ctx.VATs.Where(x => x.MasterCompanyId == masterCompanyId);
        var masterCompanyVariations = _ctx.Variations.Where(x => x.MasterCompanyId == masterCompanyId);
        var masterCompanyCurrencies = _ctx.Currencies.Where(x => x.MasterCompanyId == masterCompanyId);

        if (productId >= 0)
        {
            product =
                await _ctx.Products
                    .Include(p => p.Groups)
                    .Include(p => p.Variations)
                    .Where(p => p.ProductId == productId)
                    .Where(p => p.MasterCompanyId == masterCompanyId)
                    .SingleOrDefaultAsync();

            SetProductId(product);
            
            product.Groups = await FormatGroups(masterCompanyGroups, product);
            product.Variations = await FormatVariations(masterCompanyVariations, product);
        }
        else
        {
            product.Groups = await masterCompanyGroups.ToListAsync();
            product.Variations = await masterCompanyVariations.ToListAsync();
        }

        product.Specs = await masterCompanySpecs.ToListAsync();
        product.VATs = await masterCompanyVaTs.ToListAsync();
        product.Variations = await masterCompanyVariations.ToListAsync();
        product.Currencies = await masterCompanyCurrencies.ToListAsync();

        return product;
    }

    private void SetProductId(Product product)
    {
        foreach (var grp in product.Groups)
        {
            grp.ProductId = product.ProductId;
        }

        foreach (var vr in product.Variations)
        {
            vr.ProductId = product.ProductId;
        }
    }

    private async Task<ICollection<Group>> FormatGroups(IQueryable<Group> masterCompanyGroups, Product product)
    {
        var allGroups = await masterCompanyGroups.ToListAsync();
        allGroups.AddRange(product.Groups);

        return allGroups.DistinctBy(grp => grp.Name).ToList();
    }
    
    private async Task<ICollection<Variation>> FormatVariations(IQueryable<Variation> masterCompanyVariations, Product product)
    {
        var allVariations = await masterCompanyVariations.ToListAsync();
        allVariations.AddRange(product.Variations);

        return allVariations.DistinctBy(vr => vr.Name).ToList();
    }
}