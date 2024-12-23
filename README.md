# Agenda de contatos
Api para avaliação 

- Endpoints de criação de usuário e autenticação OAuth concluído
- Endpoints de cadastro de contatos concluído
- Endpoint de acesso ViaCep concluído
- Projeto de teste unitário concluído com alguns testes.


Front end
- ``Faltou criar.``

🚀 Começando
Realize o download do projeto ou realize a clonagem

📋 Pré-requisitos
.Net 8

🔧 Instalação

- No Visual Studio abrir a solução ContactBook.sln
- Acessar o arquivo "appsettings" dentro do projeto Uex.ContactBook.Api
- Editar a connection string com o banco de dados na propriedade "DefaultConnection"
- Marcar o projeto "Uex.ContactBook.Api" como "Default" e executar a aplicação em http. A migração irá criar a base de dados automaticamente.

- Acessar o Swagger ou importar workspace no Postman desktop para testes de integração
- http://localhost:5114/swagger/index.html
- https://www.postman.com/winter-water-234852/contactbook/overview

No Postman há uma pasta "Integration Tests" com um fluxo completo de criar usuário, autenticar, criar contato e excluir tudo.
Para autenticação pelo Swagger clicar em Authorize e informar "Bearer +token". No postman está automatizado, sendo necessário apenas executar o endpoint de login para o postman armazenar o token e passar nas próximas chamadas.

🛠️ Arquitetura
Api Rest
Clean Arquiteture

🛠️ Técnicas
Clean code
SOLID
DDD - Domain Driven Design

🛠️ Padrões
Fluent Validation
Repository

✒️ Autor
Wilham Ezequiel de Sousa

📄 Licença
MIT
