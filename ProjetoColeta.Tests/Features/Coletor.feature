Funcionalidade: Cadastro de coletor

  Cenário: Criar um novo coletor com ponto de coleta válido
    Dado que os dados do coletor e seus pontos de coleta foram preenchidos corretamente
    Quando o usuário envia uma requisição POST para "/api/Coletor"
    Então o sistema deve retornar status 201 Created
    E o corpo da resposta deve conter os dados do coletor com seus pontos
