using System.Collections.Generic;
using System.Threading.Tasks;
using DemoAPI.Models;

namespace DemoAPI.Services
{
    public interface ITargetAssetService
    {
        Task<List<TargetAsset>> GetTargetAssets();
    }
}
