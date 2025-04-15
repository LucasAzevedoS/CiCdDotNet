using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProjetoColeta.Models;

namespace ProjetoColetaModels;

public class ColetaModel
{

    public int IdColeta { get; set; }
  
    public DateTime DataColeta { get; set; }

    public int IdPonto  { get; set; }
    public PontoModel Ponto { get; set; }
}