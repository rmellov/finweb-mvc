using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinWebMvc.Models
{
    public class Record
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Descrição é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo descrição não pode ultrapassar 100 caracteres")]
        public string Description { get; set; } = null!;

        [DisplayName("Valor")]
        [Required(ErrorMessage = "Valor é obrigatório")]
        [DataType(DataType.Currency)]
        public decimal Value { get; set; }

        [DisplayName("Data do Registro")]
        [Required(ErrorMessage = "Data do registro é obrigatório")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;        

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "Tipo é obrigatório")]
        public ETypeRecord Type { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Status é obrigatório")]
        public bool Done { get; set; }

        [DisplayName("Data da baixa")]
        [Required(ErrorMessage = "Data da baixa é obrigatório")]
        [DataType(DataType.DateTime)]
        public DateTime? FinishedAt { get; set; }

        [DisplayName("Categoria")]
        [Required(ErrorMessage = "Categoria inválida")]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }

    public enum ETypeRecord
    {
        Income = 1,
        Expense = 2
    }    
}
