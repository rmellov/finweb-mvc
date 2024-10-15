using System.ComponentModel.DataAnnotations;

namespace FinWebMvcIdentity.Enums
{
    public enum EType
    {
        [Display(Name = "Despesa")]
        Expense,

        [Display(Name = "Receita")]
        Income
    }
}