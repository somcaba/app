using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Yacaba.Web.ViewModels.Authorization;

public class LogoutViewModel {
    [BindNever]
    public required String RequestId { get; init; }
}
