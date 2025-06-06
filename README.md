﻿
# LojaDoManoel - API de Empacotamento de Pedidos

Este projeto é uma API para gerenciar pedidos e realizar o empacotamento automático de produtos em caixas disponíveis, otimizando o uso de espaço.

---

## Pré-requisitos

- [Docker](https://docs.docker.com/get-docker/) instalado e funcionando na sua máquina.
- Opcional: [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) para rodar localmente sem Docker (não obrigatório).

---

## Como rodar a aplicação com Docker Compose

Clone este repositório:

git clone https://github.com/gabrrkl7/ProcessoL2.git

Rode o comando para containerizar a aplicação e subir o banco:

docker-compose up --build

cd src/LojaDoManoel.Api
Certifique-se que o arquivo docker-compose.yml está configurado corretamente para a API.

Rode o comando para rodar o projeto:

dotnet run

A aplicação estará disponível em http://localhost:5026/swagger


## Como rodar os testes
Se preferir rodar localmente (com .NET SDK instalado):

cd src/LojaDoManoel.Tests

dotnet test
Se estiver usando Docker Compose, pode configurar um serviço específico para testes ou rodar dentro do container.

Estrutura do projeto
LojaDoManoel.Api — Projeto principal com serviços, modelos, controllers e acesso a dados.

LojaDoManoel.Tests — Projeto com testes unitários usando xUnit.

Dockerfile — Configuração para containerizar a API.

docker-compose.yml — Orquestração dos containers da API e banco de dados.

Descrição técnica
O serviço EmpacotadorService recebe um pedido com produtos e tenta empacotá-los nas caixas disponíveis otimizando espaço.

Utiliza Entity Framework Core com banco relacional para persistência.

Para testes, utiliza banco em memória (provider Microsoft.EntityFrameworkCore.InMemory) garantindo testes rápidos e isolados.

## Comandos úteis
Build e rodar:

docker-compose up --build


Rodar testes localmente:

dotnet test


Parar containers:

docker-compose down


## Contato
Gabriel - Desenvolvedor
E-mail: gabriel.dev09@gmail.com
GitHub: https://github.com/gabrrkl7

