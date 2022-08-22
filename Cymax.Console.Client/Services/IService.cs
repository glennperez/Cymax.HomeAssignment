using Cymax.Console.Client.Models;

namespace Cymax.Console.Client.Services
{
    /// <summary>
    /// Base Service Interface to extend the Composition of the services.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IService<T> where T : class
    {
        Task<T?> PostDeal(Request input);
    }
}

