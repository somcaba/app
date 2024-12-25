using System.ComponentModel.DataAnnotations;

namespace Yacaba.Web.ViewModels.Authorization;

public class AuthorizeViewModel {
    [Display(Name = "Application")]
    public String ApplicationName { get; set; } = default!;

    [Display(Name = "Scope")]
    public String Scope { get; set; } = default!;
}
