using System.ComponentModel.DataAnnotations;

namespace NetFlow.Data.Models.Enums
{
    public enum Grade
    {
        [Display(Name = "Exellent")]
        A = 6,
        [Display(Name = "Very Good")]
        B = 5,
        [Display(Name = "Good")]
        C = 4,
        [Display(Name = "Worst")]
        D = 3,
        [Display(Name = "Very Worst ")]
        F = 2,
    }
}
