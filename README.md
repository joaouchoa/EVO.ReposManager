# 📦 GitHub Repositories Manager API

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet) ![C#](https://img.shields.io/badge/C%23-9.0-green) ![Entity Framework](https://img.shields.io/badge/Entity%20Framework-SQLite-lightgrey) ![MediatR](https://img.shields.io/badge/MediatR-CQRS-blue) ![FluentValidation](https://img.shields.io/badge/FluentValidation-Validation-brightgreen) ![Swagger](https://img.shields.io/badge/Swagger-API%20Documentation-yellowgreen)

API desenvolvida em .NET 8 utilizando **Clean Architecture** para gerenciar repositórios do GitHub, permitindo favoritar e consultar repositórios diretamente na API pública do GitHub.

## 🚀 Tecnologias Utilizadas

- **.NET 8** - Framework base
- **Entity Framework Core** - Integração com banco de dados SQLite
- **MediatR** - Implementação do padrão CQRS
- **FluentValidation** - Validação de entradas e regras de negócio
- **SQLite** - Banco de dados leve e rápido para persistência local
- **Swagger** - Documentação interativa da API

## 🛠️ Padrões de Projetos

- **Clean Architecture** - Arquitetura limpa com separação clara de responsabilidades
- **Service Collection Extension** - Padrão para injeção de dependências
- **CQRS** (Command Query Responsibility Segregation) - Separação de comandos e consultas via MediatR
- **Repository Pattern** - Padrão de repositório para acesso ao banco de dados
- **DTOs** (Data Transfer Objects) - Uso de objetos de transferência de dados para padronizar as respostas da API

## 🎯 Funcionalidades da API

### Endpoints:

- **GET** `/api/Repos/GetRepositoriesByUserName/`  
  🔍 Recupera os repositórios de um usuário no GitHub pelo nome de usuário.

- **GET** `/api/Repos/GetRepositoriesByName/{repositoryName}`  
  🔍 Recupera os repositórios pelo nome do repositório no GitHub.

- **POST** `/api/Repos/CreateFavoriteRepository`  
  ⭐ Adiciona um repositório aos favoritos no banco de dados local.

- **DELETE** `/api/Repos/DeleteFavoriteRepositoryById/`  
  ❌ Remove um repositório dos favoritos.

- **GET** `/api/Repos/GetFavoriteRepositories`  
  🔄 Retorna todos os repositórios favoritos.

### Swagger

A documentação da API foi gerada com **Swagger**, permitindo uma interface interativa para testar os endpoints diretamente no navegador. Acesse o Swagger na rota `/swagger` após rodar a aplicação.

### Paginação

Para melhorar a performance, todos os endpoints utilizam paginação. O cliente deve enviar o número da página e a quantidade de registros por página. Na primeira requisição, enviar o número de página como 1.

### Validação

Todos os endpoints são 100% validados utilizando FluentValidation, garantindo respostas adequadas para problemas de entrada.

## 📂 Estrutura do Projeto

- **API**: Camada de apresentação com os endpoints.
- **Application**: Regras de negócio e serviços (contendo DTOs, CQRS, e validações).
- **Infrastructure**: Camada de acesso a dados e integração com a API do GitHub.
- **Domain**: Entidades e interfaces de domínio.

## 🚧 Funcionalidades Futuras

🔧 **Front-end (MVC ou Angular)**  
🔧 **Autenticação**  
🔧 **Camadas de Testes**

## 💻 Requisitos do projeto

- Tela "Meu repositório" com informações dos repositórios da minha conta do GitHub  
- Tela "Detalhes do repositório"  
- Tela "Outro repositório" onde o usuário pode pesquisar outros repositórios  
- (BÔNUS) Recurso de repositórios favoritos ✅  
- Implementar regras de negócios na camada de aplicação ✅  
- Implementar o consumo de API na camada de infraestrutura ✅  
- (BÔNUS) Implemente o recurso favorito de acordo com os últimos itens e insira o botão para fazê-lo na tela de detalhes do repositório  
- Seja escrito em ASP.NET MVC ou WebApi(SPA), C# +4.0 ✅  
- Solução Visual Studio +2017 ✅  
- Pronto para executar pressionando F5 ✅

## 📝 Licença

Este projeto é parte de um desafio de codificação e não está sob uma licença específica.
