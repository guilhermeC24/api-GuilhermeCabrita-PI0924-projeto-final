# Api - ProjetoFinal

## Sobre o projeto

Este projeto consiste numa API REST desenvolvida em ASP.NET Core 8 para a gestão de um sistema de reservas de cinema.

A API permite gerir filmes, utilizadores e reservas, incluindo autenticação com JWT, cache em memória e comunicação com um serviço externo de pagamentos simulado com Mountebank.

---

## Tecnologias utilizadas

- ASP.NET Core 8
- C#
- Swagger
- JWT
- HttpClientFactory
- Polly
- Memory Cache
- Mountebank

---

## Como executar

### 1. Abrir o projeto

Abrir o ficheiro:

```
ApiDarioProjetoFinal.sln
```

### 2. Executar o Mountebank

Na pasta `imposter` executar:

```
mb --configfile mountebank.json
```

### 3. Executar a API

Executar o projeto no Visual Studio (F5).

Depois abrir o Swagger.

---

## Login

Para obter um token JWT utilizar:

```
POST /api/Autenticacao/login
```

Body:

```json
{
  "email": "admin@cinema.pt",
  "password": "123456"
}
```

Depois copiar o token e clicar em **Authorize** no Swagger:

```
Bearer {token}
```

---

## Funcionalidades

- Login com JWT
- CRUD de Filmes
- CRUD de Utilizadores
- CRUD de Reservas
- Pagamentos através de serviço externo
- Cache em memória
- Retry, Circuit Breaker e Fallback com Polly

---

## Autor

Guilherme Cabrita

Projeto desenvolvido para a UFCD 10792.
