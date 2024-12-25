using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Yacaba.Web.ViewModels.Authorization;

public class LogoutViewModel {
    [BindNever]
    public String RequestId { get; set; } = default!;
}
