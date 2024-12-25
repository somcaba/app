using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Yacaba.Web.Server.ViewModels.Authorization;

public class LogoutViewModel
{
    [BindNever]
    public string RequestId { get; set; }
}
