Funcionalidade: Cadastro de resíduos

  Cenário: Criar resíduo reciclável
    Dado que um cliente válido está cadastrado
    Quando o usuário envia uma requisição POST para "/api/Residuo" com os dados do resíduo reciclável
    Então o sistema deve retornar status 201 Created
    E o corpo da resposta deve conter os dados do resíduo cadastrado
