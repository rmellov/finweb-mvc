using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinWebMvc.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Descrição é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo descrição não pode ultrapassar 100 caracteres")]
        public string Description { get; set; } = null!;

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "Categoria é obrigatório")]
        public ETypeCategory Type { get; set; }
    }

    public enum ETypeCategory
    {
        Income = 1,
        Expense = 2
    }
}
