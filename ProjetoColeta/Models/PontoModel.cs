using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoColeta.Models;

public class PontoModel
{
    public int IdPonto { get; set; }
    public string Local { get; set; }
    public double CapacidadeMaxima { get; set; }
    public bool Status { get; set; }
    public int IdColetor { get; set; }
    // public ColetorModel Coletores { get; set; }
}