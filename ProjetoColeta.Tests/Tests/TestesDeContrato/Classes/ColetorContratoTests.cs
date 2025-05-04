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
using System.Collections.Generic;

namespace ProjetoColeta.Tests.TestesDeContrato.Classes
{
  public class ColetorContratoTests : IClassFixture<TestFixture>
  {
    private readonly HttpClient _client;
    private readonly string _coletorEndpoint = "api/Coletor"; // Endpoint relativo

    public ColetorContratoTests(TestFixture fixture)
    {
      _client = fixture.Client;
    }

    [Fact]
    public async Task PostColetor_RetornaContratoValido_AoCriarColetor()
    {
      // Arrange
      string schemaPath = "./Schemas/Coletor/CreateColetorResponseSchema.json";
      string expectedSchemaJson = await File.ReadAllTextAsync(schemaPath);
      JsonSchema expectedSchema = await JsonSchema.FromJsonAsync(expectedSchemaJson);

      var random = new Random();
      var idAleatorio = random.Next(1000, 9999);
      var nomeAleatorio = $"coletor_{Guid.NewGuid().ToString().Substring(0, 5)}";
      var localAleatorio = $"local_{Guid.NewGuid().ToString().Substring(0, 8)}";

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
      var response = await _client.PostAsJsonAsync(_coletorEndpoint, novoColetor);

      // Assert
      response.StatusCode.Should().Be(HttpStatusCode.Created); // Código de status esperado: 201 (Created)

      var responseContent = await response.Content.ReadAsStringAsync();

      var validationErrors = expectedSchema.Validate(responseContent);
      validationErrors.Should().BeEmpty($"O contrato para POST {_coletorEndpoint} não corresponde ao schema. Erros: {string.Join(", ", validationErrors)}");

      // Opcional: Verificar o header 'Location'
      response.Headers.Location.Should().NotBeNull();
    }
  }
}