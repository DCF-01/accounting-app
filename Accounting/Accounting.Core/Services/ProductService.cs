using Accounting.Common;
using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repositories.Interfaces;
using AutoMapper;

namespace Accounting.Core.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IUserInformation _userInformation;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IUserInformation userInformation, IMapper mapper)
    {
        _productRepository = productRepository;
        _userInformation = userInformation;
        _mapper = mapper;
    }
    public async Task<PagedResult<ProductGet>> GetPagedAsync(PagingModel pagingModel)
    {
        var result = await _productRepository.GetPagedAsync(_userInformation.MasterCompanyId, pagingModel);

        return new PagedResult<ProductGet>(_mapper.Map<IEnumerable<ProductGet>>(result.Items), result.FilteredCount);
    }

    public async Task<ProductGet> GetAsync(int id = -1)
    {
        var result = await _productRepository.GetAsync(_userInformation.MasterCompanyId, id);
        return _mapper.Map<ProductGet>(result);
    }

    public async Task CreateAsync(ProductPost productPost)
    {
        await _productRepository.CreateAsync(_userInformation.MasterCompanyId, _mapper.Map<Product>(productPost));
    }

    public async Task UpdateAsync(ProductPut productPut)
    {
        await _productRepository.UpdateAsync(_userInformation.MasterCompanyId, _mapper.Map<Product>(productPut));
    }
}