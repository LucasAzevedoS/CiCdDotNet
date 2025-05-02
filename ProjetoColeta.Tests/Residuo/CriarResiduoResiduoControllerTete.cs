using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ProjetoColeta.Tests.Residuos
{
  public class ResiduoControllerTests
  {
    private readonly HttpClient _client;

    public ResiduoControllerTests()
    {
      _client = new HttpClient
      {
        BaseAddress = new Uri("http://localhost:5108/")
      };
    }

    [Fact]
    public async Task CriarResiduoReciclavel_DeveRetornarCreated_QuandoDadosForemValidos()
    {
      // Autenticação: login para obter token JWT
      var loginPayload = new
      {
        userId = 3,
        username = "gerente01",
        password = "pass123",
        role = "gerente"
      };

      var loginResponse = await _client.PostAsJsonAsync("api/Auth/login", loginPayload);
      loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);

      var loginJson = await loginResponse.Content.ReadAsStringAsync();
      using var loginDoc = JsonDocument.Parse(loginJson);
      var token = loginDoc.RootElement.GetProperty("token").GetString();

      token.Should().NotBeNullOrWhiteSpace();

      // Adiciona o token ao header Authorization
      _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

      // Pré-condição: criar cliente válido
      var random = new Random();
      var idCliente = random.Next(1000, 9999);
      var cliente = new
      {
        idCliente = idCliente,
        nome = $"cliente_{Guid.NewGuid().ToString().Substring(0, 5)}",
        password = "teste123",
        CPF = $"{random.Next(100000000, 999999999)}01",
        numeroCasa = random.Next(1, 999)
      };

      var clienteResponse = await _client.PostAsJsonAsync("api/Clientes", cliente);
      clienteResponse.StatusCode.Should().Be(HttpStatusCode.Created);

      // Criação do resíduo reciclável
      var nomeResiduo = $"plastico_{Guid.NewGuid().ToString().Substring(0, 4)}";
      var residuo = new
      {
        idResiduo = 0,
        nome = nomeResiduo,
        peso = random.Next(1, 20),
        ehReciclavel = true,
        idCliente = idCliente
      };

      var response = await _client.PostAsJsonAsync("api/Residuo", residuo);

      response.StatusCode.Should().Be(HttpStatusCode.Created); // ou HttpStatusCode.OK

      var responseBody = await response.Content.ReadFromJsonAsync<JsonElement>();

      responseBody.GetProperty("nome").GetString().Should().Be(nomeResiduo);
      responseBody.GetProperty("ehReciclavel").GetBoolean().Should().BeTrue();
      responseBody.GetProperty("idCliente").GetInt32().Should().Be(idCliente);
    }
  }
}
