using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ProjetoColeta.Tests.Clientes
{
  public class BuscarClientePorIdTests
  {
    private readonly HttpClient _client;

    public BuscarClientePorIdTests()
    {
      _client = new HttpClient
      {
        BaseAddress = new Uri("http://localhost:5108/")
      };
    }

    [Fact]
    public async Task BuscarClientePorId_DeveRetornarOk_QuandoClienteExistir()
    {
      // Arrange
      var idExistente = 1; // Altere para um ID que exista na base de testes

      // Act
      var response = await _client.GetAsync($"api/Clientes/{idExistente}");

      // Assert
      response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task BuscarClientePorId_DeveRetornarNotFound_QuandoClienteNaoExistir()
    {
      // Arrange
      var idInexistente = 0;

      // Act
      var response = await _client.GetAsync($"api/Clientes/{idInexistente}");
      var json = await response.Content.ReadFromJsonAsync<JsonElement>();

      // Assert
      response.StatusCode.Should().Be(HttpStatusCode.NotFound);
      json.GetProperty("title").GetString().Should().Be("Not Found");
    }

  }
}
