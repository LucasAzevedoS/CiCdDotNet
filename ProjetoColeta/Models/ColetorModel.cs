using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoColeta.Models;

public class ColetorModel
{
    public int IdColetor { get; set; } 
    public string NomeColetor { get; set; }
    public ICollection<PontoModel> Pontos { get; set; }
}
