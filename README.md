# SinacorTestDev
Esta é uma aplicação para a candidatura de vaga como Desenvolvedor Fullstack na empresa B3 / Sinacor.

Sistema para cadastrar e consultar tarefas, utilizando uma ASPNET Core WebAPI e Angular app.

Seguindo as orientações do teste, nessa aplicação é possível ver a listagem de tarefas, cadastrar novas tarefas, editar e buscar pelo nome das tarefas.

A seguir algumas ferramentas tecnologias utilizadas para implementar essas regras solicitadas:

### Frameworks
- Dotnet Core 6
- Angular CLI: 17
- Node: 20.10.0
- Package Manager: npm 10.2.3

### Bibliotecas:
- Bootstrap - para estilização de alguns componentes, como formularios e botões;
- EntityFramework - para fazer a persistência das informações no banco;
- Serilog - para registrar logs de erros na aplicação;
- RabbitMQ - para auxiliar no workflow de troca de status das tarefas;

### Design Patterns
- SOLID
- Repository Pattern
- Dependcy Injection
- ExceptionMiddleware

### IDEs
- Visual Studio 2022 Community - p/ webapi
- Visual Studio Code - p/ app client

### Como Executar:

Siga os seguintes passos para rodar a aplicação:

1. Clone este repositório:

```bash
  git clone https://github.com/alexandre1sakata/SinacorTestDev.git
  cd SinacorTestDev

```

#### Front-end - app client
2. Instale as dependências

```bash
  cd SinacorTestDev.Client
  npm install
```

3. Inicie a aplicação do client

```bash
  npm start
```


#### Back-end - webapi
1. Restaurar o pacotes

```bash
  cd ..
  cd SinacorTestDev.WebAPI
  dotnet restore
```

2. Se achar necessário, testar o build da api
```bash
  dotnet build
```

3. Inicie a aplicação da api

```bash
  dotnet run
```
