Funcionalidade: Gerenciamento de clientes

  Cenário: Cliente é cadastrado com sucesso
    Dado que os dados obrigatórios do cliente são fornecidos corretamente
    Quando o usuário envia uma requisição POST para "/api/Clientes" com os dados do cliente
    Então o sistema deve retornar o status 201 Created
    E o corpo da resposta deve conter os dados do cliente cadastrado

  Cenário: Buscar lista de clientes
    Dado que existem clientes cadastrados no sistema
    Quando o usuário envia uma requisição GET para "/api/Clientes"
    Então o sistema deve retornar status 200 OK
    E o corpo da resposta deve conter uma lista de clientes

  Cenário: Buscar cliente por ID inexistente
    Dado que o ID fornecido não pertence a nenhum cliente
    Quando o usuário envia uma requisição GET para "/api/Clientes/9999"
    Então o sistema deve retornar status 404 Not Found
    E o corpo da resposta deve conter uma mensagem de erro informando que o cliente não foi encontrado
