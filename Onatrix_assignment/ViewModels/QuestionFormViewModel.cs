using System.ComponentModel.DataAnnotations;

namespace Onatrix_assignment.ViewModels;

public class QuestionFormViewModel
{

    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "Email address")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Question is required")]
    [Display(Name = "Question")]
    public string Question { get; set; } = null!;
}
