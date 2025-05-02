using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace ProjetoColeta.Tests.Clientes
{
  public class ListarClientesTests
  {
    private readonly HttpClient _client;

    public ListarClientesTests()
    {
      _client = new HttpClient
      {
        BaseAddress = new Uri("http://localhost:5108/")
      };
    }

    [Fact]
    public async Task ListarClientes_DeveRetornarOk_QuandoExistiremClientes()
    {
      // Act
      var response = await _client.GetAsync("api/Clientes");

      // Assert
      response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
  }
}
