using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using NJsonSchema;
using System.IO;

namespace ProjetoColeta.Tests.TestesDeContrato.Classes
{
  public class ResiduoContratoTests : IClassFixture<TestFixture>
  {
    private readonly HttpClient _client;
    private readonly string _residuoEndpoint = "api/Residuo"; // Endpoint relativo
    private readonly string _clientesEndpoint = "api/Clientes"; // Endpoint relativo
    private readonly string _authLoginEndpoint = "api/Auth/login"; // Endpoint de login

    public ResiduoContratoTests(TestFixture fixture)
    {
      _client = fixture.Client;
    }

    [Fact]
    public async Task PostResiduo_RetornaContratoValido_AoCriarResiduo()
    {
      // Arrange
      string schemaPath = "./Schemas/Residuo/CreateResiduoResponseSchema.json";
      string expectedSchemaJson = await File.ReadAllTextAsync(schemaPath);
      JsonSchema expectedSchema = await JsonSchema.FromJsonAsync(expectedSchemaJson);

      // Autenticação: login para obter token JWT
      var loginPayload = new
      {
        userId = 3,
        username = "gerente01",
        password = "pass123",
        role = "gerente"
      };

      var loginResponse = await _client.PostAsJsonAsync(_authLoginEndpoint, loginPayload);
      loginResponse.EnsureSuccessStatusCode();
      var loginJson = await loginResponse.Content.ReadAsStringAsync();
      using var loginDoc = JsonDocument.Parse(loginJson);
      var token = loginDoc.RootElement.GetProperty("token").GetString();
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

      var clienteResponse = await _client.PostAsJsonAsync(_clientesEndpoint, cliente);
      clienteResponse.EnsureSuccessStatusCode();

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

      // Act
      var response = await _client.PostAsJsonAsync(_residuoEndpoint, residuo);

      // Assert
      response.StatusCode.Should().Be(HttpStatusCode.Created); // ou HttpStatusCode.OK

      var responseContent = await response.Content.ReadAsStringAsync();

      // Validando o contrato com NJsonSchema
      var validationErrors = expectedSchema.Validate(responseContent);
      validationErrors.Should().BeEmpty($"O contrato para POST {_residuoEndpoint} não corresponde ao schema. Erros: {string.Join(", ", validationErrors)}");

      // Opcional: Você ainda pode querer verificar propriedades específicas se necessário
      using var responseDoc = JsonDocument.Parse(responseContent);
      responseDoc.RootElement.GetProperty("nome").GetString().Should().Be(nomeResiduo);
      responseDoc.RootElement.GetProperty("ehReciclavel").GetBoolean().Should().BeTrue();
      responseDoc.RootElement.GetProperty("idCliente").GetInt32().Should().Be(idCliente);
    }
  }
}