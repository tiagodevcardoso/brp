# Author

Author: Tiago Pereira Cardoso

## Services

SEQ: Log da aplicação
```
http://localhost:1080/#/events?range=1d
```

API - Person
```
http://localhost:5000/swagger/index.html
```

API - Consumer Service MassTransit
```
http://localhost:5001
```

API - Publish Service MassTransit
```
http://localhost:5002/swagger/index.html
```

## Configuration
Deixe setado como Startup Project o "docker-compose" como projeto principal

## Migrations

Vá em "Package Manager Console":
Criar uma migration nova digite o código abaixo:

```
Add-Migration -OutputDir Migrations [NomeMigration] -Project BRL.Infrastructure.Data -StartupProject BRP.Services.API.Person -Context DbContextSystemUsers
```

Atualizar o banco de dados sob uma migration digite o código abaixo:

```
Update-Database -Project BRL.Infrastructure.Data -StartupProject BRP.Services.API.Person
```

## Nuget

Para atualizar todas DLL dos projetos do sistema pelo Package Manager Console

```
Update-Package 
```

## Send value API Publish Masstransit

Criado serviço Consumer e Publish usando o MassTransit, esses serviços que foram criados para serem usado no Serviço de API onde temos o cadastro da Pessoa. 

Para criarmos uma nova pessoa iremos usar o publish para ser enviado na Fila do RabbitMQ usando o Masstransit que nele vai identificar que tipo de serviço iremos usar em nossa API
Decidir fazer um MassTransit Generico para que possamos reconhecer para qual serviço iremos usar do tipo do atributo do nosso elemento GET, POST, DELETE, PUT.

Exemplo POST

```
{
  "api": "http://api/v1/person/add",
  "parameters": "",
  "method": "POST",
  "body": "{}" //JSON em string
}
```

Exemplo PUT

```
{
  "api": "http://api/v1/person/add",
  "parameters": "",
  "method": "PUT",
  "body": "{}" //JSON em string
}
```

Exemplo DELETE

```
{
  "api": "http://api/v1/person/add",
  "parameters": "id=[value]", // Substitua o valor [value] para o id registrado no sistema
  "method": "DELETE",
  "body": ""
}
```

## Manager User-Secrets

API Person
```
{
  "CONNECTION_STRING": "mongodb://root:root@mongo:27017/",
  "SEQ_URI": "http://seq",
  "RABBITMQ_URI": "amqp://rabbitmq",
  "CORS": "http://api_consumer,http://api_publish"
}
```

API - Consumer Service MassTransit
```
{
  "SEQ_URI": "http://seq",
  "RABBITMQ_URI": "amqp://rabbitmq",
  "CORS": "http://api_consumer,http://api_publish"
}
```

API - Publish Service MassTransit
```
{
  "SEQ_URI": "http://seq",
  "RABBITMQ_URI": "amqp://rabbitmq",
  "CORS": "http://api_consumer,http://api_publish"
}
```