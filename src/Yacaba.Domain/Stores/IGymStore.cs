using Yacaba.Core.Stores;
using Yacaba.Domain.Models;
using Yacaba.Domain.Requests;

namespace Yacaba.Domain.Stores {
    public interface IGymStore : IStore<Gym, Int64, GymCreateRequest, GymUpdateRequest> { }
}
