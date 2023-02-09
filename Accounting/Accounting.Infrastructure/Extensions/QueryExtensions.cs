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
    
    public static IQueryable<Currency> Order(this IQueryable<Currency> currenciesQuery, string sortOrder, int sortColumn)
    {
        Expression<Func<Currency, object>> sortExpression = sortColumn switch
        {
            0 => sortExpression => sortExpression.Name.ToLower(),
            1 => sortExpression => sortExpression.Value,
            2 => sortExpression => sortExpression.Products.Count,
            _ => sortExpression => sortExpression.Name
        };

        return sortOrder == "asc"
            ? currenciesQuery.OrderBy(sortExpression)
            : currenciesQuery.OrderByDescending(sortExpression);
    }
    
    public static IQueryable<Spec> Order(this IQueryable<Spec> specsQuery, string sortOrder, int sortColumn)
    {
        Expression<Func<Spec, object>> sortExpression = sortColumn switch
        {
            _ => sortExpression => sortExpression.Name
        };

        return sortOrder == "asc"
            ? specsQuery.OrderBy(sortExpression)
            : specsQuery.OrderByDescending(sortExpression);
    }
    
    public static IQueryable<Variation> Order(this IQueryable<Variation> variationsQuery, string sortOrder, int sortColumn)
    {
        Expression<Func<Variation, object>> sortExpression = sortColumn switch
        {
            _ => sortExpression => sortExpression.Name
        };

        return sortOrder == "asc"
            ? variationsQuery.OrderBy(sortExpression)
            : variationsQuery.OrderByDescending(sortExpression);
    }
    
    public static IQueryable<VAT> Order(this IQueryable<VAT> vatQuery, string sortOrder, int sortColumn)
    {
        Expression<Func<VAT, object>> sortExpression = sortColumn switch
        {
            0 => sortExpression => sortExpression.Name,
            1 => sortExpression => sortExpression.Value,
            _ => sortExpression => sortExpression.Name
        };

        return sortOrder == "asc"
            ? vatQuery.OrderBy(sortExpression)
            : vatQuery.OrderByDescending(sortExpression);
    }
    
    public static IQueryable<Group> Order(this IQueryable<Group> GroupQuery, string sortOrder, int sortColumn)
    {
        Expression<Func<Group, object>> sortExpression = sortColumn switch
        {
            0 => sortExpression => sortExpression.Name,
            1 => sortExpression => sortExpression.Products.Count(),
            _ => sortExpression => sortExpression.Name
        };

        return sortOrder == "asc"
            ? GroupQuery.OrderBy(sortExpression)
            : GroupQuery.OrderByDescending(sortExpression);
    }
    
    public static IQueryable<Product> Order(this IQueryable<Product> productQuery, string sortOrder, int sortColumn)
    {
        Expression<Func<Product, object>> sortExpression = sortColumn switch
        {
            0 => sortExpression => sortExpression.SKU,
            1 => sortExpression => sortExpression.Name,
            2 => sortExpression => sortExpression.WholesalePrice,
            3 => sortExpression => sortExpression.RetailPrice,
            4 => sortExpression => sortExpression.OnSale,
            5 => sortExpression => sortExpression.InStock,
            _ => sortExpression => sortExpression.Name
        };

        return sortOrder == "asc"
            ? productQuery.OrderBy(sortExpression)
            : productQuery.OrderByDescending(sortExpression);
    }
}