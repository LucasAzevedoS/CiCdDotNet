using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ProjetoColeta.Tests.Coletor
{
  public class ColetorControllerTests
  {
    private readonly HttpClient _client;

    public ColetorControllerTests()
    {
      _client = new HttpClient
      {
        BaseAddress = new Uri("http://localhost:5108/") // ajuste se sua API estiver em outra porta
      };
    }

    [Fact]
    public async Task CriarColetor_DeveRetornarCreated_QuandoDadosForemValidos()
    {
      // Arrange
      var random = new Random();
      var idAleatorio = random.Next(1000, 9999);
      var nomeAleatorio = $"coletor_{Guid.NewGuid().ToString().Substring(0, 5)}";
      var localAleatorio = $"local_{Guid.NewGuid().ToString().Substring(0, 5)}";

      var novoColetor = new
      {
        idColetor = idAleatorio, // usa ID explícito
        nomeColetor = nomeAleatorio,
        pontos = new[]
          {
            new
            {
                idPonto = idAleatorio, // igual ao idColetor, para simplificar
                local = localAleatorio,
                capacidadeMaxima = random.Next(50, 500),
                status = true,
                idColetor = idAleatorio // garantir consistência
            }
        }
      };

      // Act
      var response = await _client.PostAsJsonAsync("api/Coletor", novoColetor);

      // Assert
      response.StatusCode.Should().Be(HttpStatusCode.Created);

      var responseBody = await response.Content.ReadFromJsonAsync<JsonElement>();

      responseBody.GetProperty("nomeColetor").GetString().Should().Be(nomeAleatorio);

      var pontos = responseBody.GetProperty("pontos");
      pontos.GetArrayLength().Should().BeGreaterThan(0);

      var ponto = pontos[0];
      ponto.GetProperty("local").GetString().Should().Be(localAleatorio);
    }

  }
}
