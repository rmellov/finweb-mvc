using FinWebMvcIdentity.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinWebMvcIdentity.Models
{
    public class Category
    {
        public int Id { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, ErrorMessage = "O campo não pode ultrapassar 100 caracteres")]
        public string Description { get; set; } = null!;

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public EType Type { get; set; }

        public string? User { get; set; }
    }
}