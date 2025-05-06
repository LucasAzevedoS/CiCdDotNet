Funcionalidade: Autenticação de usuário

  Cenário: Usuário fornece credenciais válidas
    Dado que o usuário possui credenciais válidas
    Quando ele envia uma requisição POST para "/api/Auth/login" com seu username e password
    Então o sistema deve retornar o status 200 OK
    E o corpo da resposta deve conter um token de autenticação

  Cenário: Usuário fornece credenciais inválidas
    Dado que o usuário fornece um nome de usuário ou senha incorretos
    Quando ele envia uma requisição POST para "/api/Auth/login"
    Então o sistema deve retornar o status 401 Unauthorized
    E o corpo da resposta deve conter uma mensagem de erro indicando falha na autenticação
