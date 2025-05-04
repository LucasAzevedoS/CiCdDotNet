using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using NJsonSchema;
using System.IO;

namespace ProjetoColeta.Tests.TestesDeContrato.Classes
{
  public class ClientesContratoTests : IClassFixture<TestFixture>
  {
    private readonly HttpClient _client;
    private readonly string _clientesEndpoint = "api/Clientes"; // Endpoint relativo

    public ClientesContratoTests(TestFixture fixture)
    {
      _client = fixture.Client;
    }

    [Fact]
    public async Task PostClientes_RetornaContratoValido_AoCriarCliente()
    {
      // Arrange
      string schemaPath = "./Schemas/Clientes/CreateClienteResponseSchema.json";
      string expectedSchemaJson = await File.ReadAllTextAsync(schemaPath);
      JsonSchema expectedSchema = await JsonSchema.FromJsonAsync(expectedSchemaJson);

      var novoCliente = new
      {
        idCliente = new Random().Next(1000, 9999), // Gera ID aleatório
        nome = $"rian_{Guid.NewGuid().ToString().Substring(0, 5)}",
        password = "rian123",
        CPF = $"{new Random().Next(100000000, 999999999)}01",
        numeroCasa = 10
      };

      // Act
      var response = await _client.PostAsJsonAsync(_clientesEndpoint, novoCliente);

      // Assert
      response.StatusCode.Should().Be(HttpStatusCode.Created); // O código de status esperado é 201 (Created)

      var responseContent = await response.Content.ReadAsStringAsync();

      var validationErrors = expectedSchema.Validate(responseContent);
      validationErrors.Should().BeEmpty($"O contrato para POST {_clientesEndpoint} não corresponde ao schema. Erros: {string.Join(", ", validationErrors)}");

      // Opcional: Você pode adicionar mais asserções se necessário, como verificar o header 'Location'
      response.Headers.Location.Should().NotBeNull();
    }
  }
}