using System.ComponentModel.DataAnnotations;

namespace Yacaba.Web.ViewModels.Authorization;

public class AuthorizeViewModel {
    [Display(Name = "Application")]
    public required String ApplicationName { get; init; }

    [Display(Name = "Scope")]
    public required String Scope { get; init; }
}
