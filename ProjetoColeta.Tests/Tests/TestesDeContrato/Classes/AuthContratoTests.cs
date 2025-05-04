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
  public class AuthContratoTests : IClassFixture<TestFixture>
  {
    private readonly HttpClient _client;
    private readonly string _loginEndpoint = "api/Auth/login"; // Endpoint relativo

    public AuthContratoTests(TestFixture fixture)
    {
      _client = fixture.Client;
    }

    [Fact]
    public async Task PostLogin_RetornaContratoValido()
    {
      // Arrange
      string schemaPath = "./Schemas/Auth/AuthLoginResponseSchema.json";
      string expectedSchemaJson = await File.ReadAllTextAsync(schemaPath);
      JsonSchema expectedSchema = await JsonSchema.FromJsonAsync(expectedSchemaJson);

      var loginPayload = new
      {
        userId = 3,
        username = "gerente01",
        password = "pass123",
        role = "gerente"
      };

      // Act
      var response = await _client.PostAsJsonAsync(_loginEndpoint, loginPayload);

      // Assert
      response.StatusCode.Should().Be(HttpStatusCode.OK);

      var responseContent = await response.Content.ReadAsStringAsync();

      // Validando o contrato com NJsonSchema
      var validationErrors = expectedSchema.Validate(responseContent);
      validationErrors.Should().BeEmpty($"O contrato para POST {_loginEndpoint} não corresponde ao schema. Erros: {string.Join(", ", validationErrors)}");

      // Opcional: Você ainda pode querer verificar propriedades específicas se necessário
      using var doc = JsonDocument.Parse(responseContent);
      doc.RootElement.TryGetProperty("token", out var token).Should().BeTrue();
      token.GetString().Should().NotBeNullOrWhiteSpace();
    }
  }

  public class TestFixture
  {
    public HttpClient Client { get; }

    public TestFixture()
    {
      Client = new HttpClient
      {
        BaseAddress = new Uri("http://localhost:5108/")
      };
    }

    public async Task<string> GetAuthToken()
    {
      var loginPayload = new
      {
        userId = 3,
        username = "gerente01",
        password = "pass123",
        role = "gerente"
      };

      var response = await Client.PostAsJsonAsync("api/Auth/login", loginPayload);
      response.EnsureSuccessStatusCode();
      var json = await response.Content.ReadAsStringAsync();
      using var doc = JsonDocument.Parse(json);
      return doc.RootElement.GetProperty("token").GetString();
    }
  }
}