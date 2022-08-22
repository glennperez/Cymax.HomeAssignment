using Cymax.Console.Client.Models;

namespace Cymax.Console.Client.Services
{
    public interface IService<T> where T : class
    {
        Task<T?> PostDeal(Request input);
    }
}

