namespace Cymax.Console.Client.Models;

/// <summary>
/// Base class for All company response
/// </summary>
public interface ICompanyResponse
{
    public int Deal { get; set; }
    public string CompanyName { get; set; }
}