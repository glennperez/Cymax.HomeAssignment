using System;
using Cymax.Console.Client.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Cymax.Console.Client.Services
{
    public interface IService<T> where T : class
    {
        Task<T> PostDeal(Request input);
    }
}

