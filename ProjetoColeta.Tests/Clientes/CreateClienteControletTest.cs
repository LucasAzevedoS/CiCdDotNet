using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ProjetoColeta.Tests.Clientes
{
  public class ClientesControllerTests
  {
    private readonly HttpClient _client;

    public ClientesControllerTests()
    {
      _client = new HttpClient
      {
        BaseAddress = new Uri("http://localhost:5108/")
      };
    }

    [Fact]
    public async Task CriarCliente_DeveRetornarCreated_QuandoDadosForemValidos()
    {
      // Arrange
      var novoCliente = new
      {
        idCliente = new Random().Next(1000, 9999), // Gera ID aleatório
        nome = $"rian_{Guid.NewGuid().ToString().Substring(0, 5)}",
        password = "rian123",
        CPF = $"{new Random().Next(100000000, 999999999)}01",
        numeroCasa = 10
      };

      // Act
      var response = await _client.PostAsJsonAsync("api/Clientes", novoCliente);

      // Assert
      response.StatusCode.Should().Be(HttpStatusCode.Created);
    }


  }
}
