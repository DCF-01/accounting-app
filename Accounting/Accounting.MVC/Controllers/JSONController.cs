using Accounting.Core.Interfaces;
using Accounting.Core.Requests;
using Accounting.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.Controllers
{
    [Authorize(Policy = "Users")]
    public class JsonController : BaseController
    {
        private readonly IMasterCompanyService _masterCompanyService;
        private readonly IUserService _userService;
        private readonly IVariationService _variationService;
        private readonly ISpecService _specService;
        private readonly IGroupService _groupService;
        private readonly IProductService _productService;
        private readonly IVATService _vatService;
        private readonly IBankService _bankService;
        private readonly IBankAccountService _bankAccountService;
        private readonly ICurrencyService _currencyService;
        private readonly ICompanyService _companyService;

        public JsonController(
            IMasterCompanyService masterCompanyService,
            IUserService userService,
            IVariationService variationService,
            ISpecService specService,
            IGroupService groupService,
            IProductService productService,
            IVATService vatService,
            IBankService bankService,
            IBankAccountService bankAccountService,
            ICurrencyService currencyService,
            ICompanyService companyService
            )
        {
            _masterCompanyService = masterCompanyService;
            _userService = userService;
            _variationService = variationService;
            _specService = specService;
            _groupService = groupService;
            _productService = productService;
            _vatService = vatService;
            _bankService = bankService;
            _bankAccountService = bankAccountService;
            _currencyService = currencyService;
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> User(PagingParamsViewModel pagingParams)
        {
            var result = await _userService.GetPagedAsync(pagingParams.ToPagingModel());

            return Ok(
                new DataTablesResponseModel<UserGet>
                {
                    Draw = pagingParams.Draw,
                    RecordsTotal = result.TotalCount,
                    RecordsFiltered = result.FilteredCount,
                    Data = result.Items,
                    Error = string.Empty
                }
            );
        }

        [HttpGet]
        public async Task<IActionResult> Product(PagingParamsViewModel pagingParams)
        {
            var result = await _productService.GetPagedAsync(pagingParams.ToPagingModel());
                
            return Ok(
                new DataTablesResponseModel<ProductGet>
                {
                    Draw = pagingParams.Draw,
                    RecordsTotal = result.TotalCount,
                    RecordsFiltered = result.FilteredCount,
                    Data = result.Items,
                    Error = string.Empty
                }
            );
        }

        [HttpGet]
        public async Task<IActionResult> Group(PagingParamsViewModel pagingParams)
        {
            var result = await _groupService.GetPagedAsync(pagingParams.ToPagingModel());

            return Ok(
                new DataTablesResponseModel<GroupGet>
                {
                    Draw = pagingParams.Draw,
                    RecordsTotal = result.TotalCount,
                    RecordsFiltered = result.FilteredCount,
                    Data = result.Items,
                    Error = string.Empty
                }
            );
        }

        [HttpGet]
        public async Task<IActionResult> Spec(PagingParamsViewModel pagingParams)
        {
            var result = await _specService.GetPagedAsync(pagingParams.ToPagingModel());

            return Ok(
                new DataTablesResponseModel<SpecGet>
                {
                    Draw = pagingParams.Draw,
                    RecordsTotal = result.TotalCount,
                    RecordsFiltered = result.FilteredCount,
                    Data = result.Items,
                    Error = string.Empty
                }
            );
        }

        public async Task<IActionResult> Variation(PagingParamsViewModel pagingParams)
        {
            var result = await _variationService.GetPagedAsync(pagingParams.ToPagingModel());

            return Ok(
                new DataTablesResponseModel<VariationGet>
                {
                    Draw = pagingParams.Draw,
                    RecordsTotal = result.TotalCount,
                    RecordsFiltered = result.FilteredCount,
                    Data = result.Items,
                    Error = string.Empty
                }
            );
        }

        [HttpGet]
        public async Task<IActionResult> MasterCompany(PagingParamsViewModel pagingParams)
        {
            var result = await _masterCompanyService.GetPagedAsync(pagingParams.ToPagingModel());
            return Ok(
                new DataTablesResponseModel<MasterCompanyGet>()
                {
                    Draw = pagingParams.Draw,
                    RecordsTotal = result.TotalCount,
                    RecordsFiltered = result.FilteredCount,
                    Data = result.Items,
                    Error = string.Empty
                }
            );
        }
        
        [HttpGet]
        public async Task<IActionResult> Vat(PagingParamsViewModel pagingParams)
        {
            var result = await _vatService.GetPagedAsync(pagingParams.ToPagingModel());
            return Ok(
                new DataTablesResponseModel<VATGet>()
                {
                    Draw = pagingParams.Draw,
                    RecordsTotal = result.TotalCount,
                    RecordsFiltered = result.FilteredCount,
                    Data = result.Items,
                    Error = string.Empty
                }
            );
        }
        
        [HttpGet]
        public async Task<IActionResult> Bank(PagingParamsViewModel pagingParams)
        {
            var result = await _bankService.GetPagedAsync(pagingParams.ToPagingModel());
            return Ok(
                new DataTablesResponseModel<BankGet>()
                {
                    Draw = pagingParams.Draw,
                    RecordsTotal = result.TotalCount,
                    RecordsFiltered = result.FilteredCount,
                    Data = result.Items,
                    Error = string.Empty
                }
            );
        }
        
        [HttpGet]
        public async Task<IActionResult> BankAccount(PagingParamsViewModel pagingParams)
        {
            var result = await _bankAccountService.GetPagedAsync(pagingParams.ToPagingModel());
            return Ok(
                new DataTablesResponseModel<BankAccountGet>()
                {
                    Draw = pagingParams.Draw,
                    RecordsTotal = result.TotalCount,
                    RecordsFiltered = result.FilteredCount,
                    Data = result.Items,
                    Error = string.Empty
                }
            );
        }
        
        [HttpGet]
        public async Task<IActionResult> Currency(PagingParamsViewModel pagingParams)
        {
            var result = await _currencyService.GetPagedAsync(pagingParams.ToPagingModel());
            return Ok(
                new DataTablesResponseModel<CurrencyGet>()
                {
                    Draw = pagingParams.Draw,
                    RecordsTotal = result.TotalCount,
                    RecordsFiltered = result.FilteredCount,
                    Data = result.Items,
                    Error = string.Empty
                }
            );
        }

        [HttpGet]
        public async Task<IActionResult> Company(PagingParamsViewModel pagingParams)
        {
            var result = await _companyService.GetPagedAsync(pagingParams.ToPagingModel());
            return Ok(
                new DataTablesResponseModel<CompanyGet>()
                {
                    Draw = pagingParams.Draw,
                    RecordsTotal = result.TotalCount,
                    RecordsFiltered = result.FilteredCount,
                    Data = result.Items,
                    Error = string.Empty
                }
            );
        }
    }
}