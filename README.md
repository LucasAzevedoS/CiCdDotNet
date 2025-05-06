# Projeto Coleta Seletiva

[![.NET](https://github.com/LucasAzevedoS/CiCdDotNet/actions/workflows/dotnet.yml/badge.svg)](https://github.com/LucasAzevedoS/CiCdDotNet/actions/workflows/dotnet.yml)

Este projeto tem como objetivo o gerenciamento de coletas seletivas de resíduos. Foi desenvolvido utilizando .NET 8, com um sistema de deploy automatizado via GitHub Actions para os ambientes de Homologação e Produção no Azure Web App.

---

## 🛠️ Tecnologias Utilizadas

As seguintes tecnologias foram utilizadas no desenvolvimento deste projeto:

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [C#](https://learn.microsoft.com/pt-br/dotnet/csharp/)
- [Azure Web App](https://azure.microsoft.com/pt-br/services/app-service/web/)
- [GitHub Actions](https://github.com/features/actions)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)

---

## ⚙️ Pré-requisitos

Antes de executar o projeto localmente, você precisará ter as seguintes ferramentas instaladas em sua máquina:

- [.NET SDK 8.0+](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Git](https://git-scm.com/downloads)
- [Editor de código](https://code.visualstudio.com/) (ex: Visual Studio Code ou Visual Studio)

---

## 🚀 Como rodar localmente

Siga os passos abaixo para executar o projeto em seu ambiente de desenvolvimento:

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/LucasAzevedoS/CiCdDotNet.git
   ```

2. **Navegue até a pasta do projeto:**

   ```bash
   cd ProjetoColeta
   ```

3. **Restaure os pacotes NuGet:**

   ```bash
   dotnet restore
   ```

4. **Execute o projeto:**

   ```bash
   dotnet run --project ProjetoColeta/ProjetoColeta.csproj
   ```

   A API estará rodando nos seguintes endereços:

   - `https://localhost:5108/`
   - `http://localhost:5108/`

---

## 📂 Estrutura de Pastas

A estrutura principal do projeto de testes:

    ProjetoColeta/
    ├── 📂 Controllers/
    │   └── (Conteúdo dos Controllers)
    ├── 📂 Models/
    │   └── (Definição das classes de modelo)
    ├── 📂 Services/
    │   └── (Lógica de negócios e serviços)
    ├── 📂 Data/
    │   └── (Acesso a dados e contexto do EF Core)

    ├── 📄 ProjetoColeta.csproj - Arquivo de projeto .NET
    ├── 📄 Program.cs           - Ponto de entrada da aplicação
    ├── 📄 appsettings.json     - Arquivo de configuração
    └── 📄 ProjetoColeta.sln    - Arquivo de solução do Visual Studio

## ✅ Testes Automatizados

O projeto utiliza **xUnit** para testes automatizados e **FluentAssertions** para asserções mais legíveis. Além disso, os testes estão organizados em diferentes camadas, como autenticação, clientes, coletores e resíduos.

### Executando os testes

Para rodar todos os testes:

2. **Navegue até a pasta do projeto:**

   ```bash
   cd ProjetoColeta.Tests
   ```

3. **Restaure os pacotes NuGet:**

   ```bash
   dotnet restore
   ```

4. **Execute o projeto:**

   ```bash
   dotnet test
   ```

## 🧪 Estrutura do Projeto de Testes

A estrutura principal do projeto de testes é organizada da seguinte forma:

    ProjetoColeta.Tests/
    ├── 📂 Features/
    │   ├── 📄 Auth.feature
    │   ├── 📄 Cliente.feature
    │   ├── 📄 Coletor.feature
    │   └── 📄 Residuo.feature

    ├── 📂 Tests/
    │   ├── 📂 Auth/
    │   │   └── 📄 AuthControllerTests.cs
    │   ├── 📂 Clientes/
    │   │   ├── 📄 CreateClienteControllerTest.cs
    │   │   ├── 📄 ListarPorIdClienteControllerTest.cs
    │   │   └── 📄 ListarTodosClienteControllerTest.cs
    │   ├── 📂 Coletor/
    │   │   └── 📄 CriarColetorColetorControllerTest.cs
    │   └── 📂 Resíduo/
    │       └── 📄 CriarResíduoResíduoControllerTest.cs

    ├── 📂 TestesDeContrato/
    │   ├── 📂 Classes/
    │   │   ├── 📄 AuthContratoTests.cs
    │   │   ├── 📄 ClientesContratoTests.cs
    │   │   ├── 📄 ColetorContratoTests.cs
    │   │   └── 📄 ResíduoContratoTests.cs
    │   └── 📂 Schemas/
    │       ├── 📂 Auth/
    │       │   └── 📄 AuthLoginResponseSchema.json
    │       ├── 📂 Clientes/
    │       │   └── 📄 CreateClienteResponseSchema.json
    │       ├── 📂 Coletor/
    │       │   └── 📄 CreateColetorResponseSchema.json
    │       └── 📂 Resíduo/
    │           └── 📄 CreateResíduoResponseSchema.json

    ├── 📄 ProjetoColeta.Tests.csproj - Arquivo de projeto de testes .NET
    ├── 📄 UnitTest1.cs               - Classe de exemplo de teste
    └── 📄 appsettings.json           - Configuração dos testes
