using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoColeta.Models;

public class ResiduoModel
{
    [Key]
    public int IdResiduo { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "O nome do resíduo deve ter no máximo 100 caracteres.")]
    public string Nome { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "O peso não pode ser negativo.")]
    public double Peso { get; set; }

    [Required]
    public bool EhReciclavel { get; set; } 

    [ForeignKey("IdCliente")]
    public int IdCliente { get; set; }

    // public ClienteModel Cliente { get; set; } 
}