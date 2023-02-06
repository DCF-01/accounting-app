namespace Accounting.Core.Interfaces;

public interface IUserInformation
{
    Guid MasterCompanyId { get; set; }
    bool IsDebug { get; set; }
}