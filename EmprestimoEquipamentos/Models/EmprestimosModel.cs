using System.ComponentModel.DataAnnotations;

namespace EmprestimoEquipamentos.Models
{
    public class EmprestimosModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do recebedor!")]
        public string Recebedor { get; set; }

        [Required(ErrorMessage = "Digite o nome do fornecedor!")]
        public string Fornecedor { get; set; }

        [Required(ErrorMessage = "Digite o nome do Equipamento!")]
        public string EquipamentoEmprestado { get; set; }

        public DateTime dataUltimaAtualizacao { get; set; } = DateTime.Now;
    }
}
