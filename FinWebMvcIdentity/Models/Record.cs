using FinWebMvcIdentity.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FinWebMvcIdentity.Models
{
    public class Record
    {
        public int Id { get; set; }

        [DisplayName("Tipo")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public EType Type { get; set; }

        [DisplayName("Categoria")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int CategoryId { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, ErrorMessage = "O campo não pode ultrapassar 100 caracteres")]
        public string Description { get; set; } = null!;

        [DisplayName("Data do Registro")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }

        [DisplayName("Valor")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Currency)]
        public decimal Value { get; set; }

        [DisplayName("Data do Vencimento/Recebimento")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date)]
        public DateTime? MaturityPaymentDate { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public EStatus Status { get; set; }

        public Category? Category { get; set; }

        public string? User { get; set; }
    }
}