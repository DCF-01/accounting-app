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
    
    public static IQueryable<Bank> Order(this IQueryable<Bank> bankQuery, string sortOrder, int sortColumn)
    {
        Expression<Func<Bank, object>> sortExpression = sortColumn switch
        {
            0 => sortExpression => sortExpression.Name,
            1 => sortExpression => sortExpression.SWIFT,
            2 => sortExpression => sortExpression.BankAccounts.Count(),
            _ => sortExpression => sortExpression.Name
        };

        return sortOrder == "asc"
            ? bankQuery.OrderBy(sortExpression)
            : bankQuery.OrderByDescending(sortExpression);
    }
    
    public static IQueryable<BankAccount> Order(this IQueryable<BankAccount> bankAccountsQuery, string sortOrder, int sortColumn)
    {
        Expression<Func<BankAccount, object>> sortExpression = sortColumn switch
        {
            0 => sortExpression => sortExpression.Name,
            1 => sortExpression => sortExpression.Number,
            2 => sortExpression => sortExpression.IBAN,
            _ => sortExpression => sortExpression.Name
        };

        return sortOrder == "asc"
            ? bankAccountsQuery.OrderBy(sortExpression)
            : bankAccountsQuery.OrderByDescending(sortExpression);
    }
}