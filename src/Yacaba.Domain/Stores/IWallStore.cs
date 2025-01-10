using Yacaba.Core.Stores;
using Yacaba.Domain.Models;
using Yacaba.Domain.Requests;

namespace Yacaba.Domain.Stores {
    public interface IWallStore : IStore<Wall, Int64, WallCreateRequest, WallUpdateRequest> { }
}
