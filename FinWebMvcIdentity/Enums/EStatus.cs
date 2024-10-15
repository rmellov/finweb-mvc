using System.ComponentModel.DataAnnotations;

namespace FinWebMvcIdentity.Enums
{
    public enum EStatus
    {
        [Display(Name = "Pendente")]
        Pending,

        [Display(Name = "Concluído")]
        Complete
    }
}
