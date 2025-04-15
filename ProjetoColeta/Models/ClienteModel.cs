using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetoColeta.Models;


public class ClienteModel
{
    public int IdCliente { get; set; }
    public string Nome { get; set; }
    public string Password { get; set; } 
    public string Cpf { get; set; }
    public int NumeroCasa { get; set; } 
    
    // public ICollection<ResiduoModel> Residuos { get; set; }
}