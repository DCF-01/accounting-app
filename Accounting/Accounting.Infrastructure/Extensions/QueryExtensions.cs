using System.Linq.Expressions;
using Accounting.Infrastructure.Models;

namespace Accounting.Infrastructure.Extensions;

public static class QueryExtensions
{
    public static IQueryable<MasterCompany> Order(this IQueryable<MasterCompany> MasterCompanysQuery, string sortOrder, int sortColumn)
    {
        Expression<Func<MasterCompany, object>> sortExpression = sortColumn switch
        {
            0 => sortExpression => sortExpression.Name,
            1 => sortExpression => sortExpression.Users.Count(),
            _ => sortExpression => sortExpression.Name
        };

        return sortOrder == "asc"
            ? MasterCompanysQuery.OrderBy(sortExpression)
            : MasterCompanysQuery.OrderByDescending(sortExpression);
    }
}