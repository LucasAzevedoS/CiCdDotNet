using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ProjetoColeta.Tests.Auth
{
  public class AuthControllerTests : IClassFixture<TestFixture>
  {
    private readonly HttpClient _client;

    public AuthControllerTests(TestFixture fixture)
    {
      _client = fixture.Client;
    }

    [Fact]
    public async Task Login_DeveRetornarToken_QuandoCredenciaisForemValidas()
    {
      var loginPayload = new
      {
        userId = 3,
        username = "gerente01",
        password = "pass123",
        role = "gerente"
      };

      var response = await _client.PostAsJsonAsync("api/Auth/login", loginPayload);

      response.StatusCode.Should().Be(HttpStatusCode.OK);

      var json = await response.Content.ReadAsStringAsync();
      using var doc = JsonDocument.Parse(json);

      doc.RootElement.TryGetProperty("token", out var token).Should().BeTrue();
      token.GetString().Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Login_DeveRetornarUnauthorized_QuandoSenhaForInvalida()
    {
      var loginPayload = new
      {
        userId = 1,
        username = "operador01",
        password = "senhaErrada",
        role = "operador"
      };

      var response = await _client.PostAsJsonAsync("api/Auth/login", loginPayload);

      response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Login_DeveRetornarBadRequest_QuandoPayloadForInvalido()
    {
      var loginPayload = new
      {
        // Campo username ausente
        password = "pass123",
        role = "operador"
      };

      var response = await _client.PostAsJsonAsync("api/Auth/login", loginPayload);

      response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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
  }
}
