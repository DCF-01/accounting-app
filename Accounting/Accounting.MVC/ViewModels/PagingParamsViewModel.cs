using Accounting.Common;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.MVC.ViewModels
{
    public class PagingParamsViewModel
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        [FromQuery(Name = "order[0][column]")] public int Column { get; set; }
        [FromQuery(Name = "order[0][dir]")] public string Order { get; set; }

        private string _searchTerm;

        [FromQuery(Name = "search[value]")]
        public string SearchTerm
        {
            get => _searchTerm;
            set => _searchTerm = value?.ToLower() ?? string.Empty;
        }

        public PagingModel ToPagingModel() => new (_searchTerm, Start, Length, Column, Order);
    }

    public class DataTablesResponseModel<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public string Error { get; set; }
    }
}