## Sobre o Projeto

O `BasicWebServer` é um servidor web básico desenvolvido em C#. Este projeto é inspirado no artigo ["Writing a Web Server from Scratch"](https://www.codeproject.com/Articles/859108/Writing-a-Web-Server-from-Scratch) e foi criado para fins educacionais, com o objetivo de demonstrar conceitos fundamentais de redes e servidores web. Ele é capaz de lidar com conexões simultâneas e responder a solicitações HTTP simples.

## Tecnologias Utilizadas

![Ubuntu](https://img.shields.io/badge/Ubuntu-E95420?style=for-the-badge&logo=ubuntu&logoColor=white)
![Visual Studio Code](https://img.shields.io/badge/Visual%20Studio%20Code-0078d7.svg?style=for-the-badge&logo=visual-studio-code&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

## Instalação

1. Clone o repositório:
    ```bash
    git clone https://github.com/seu-usuario/BasicWebServer.git
    ```
2. Navegue até o diretório do projeto:
    ```bash
    cd BasicWebServer
    ```
3. Restaure as dependências:
    ```bash
    dotnet restore
    ```
4. Compile o projeto:
    ```bash
    dotnet build
    ```
5. Inicie o projeto:
    ```bash
    dotnet run --project BasicWebServer.Console
    ```

## Uso

Após iniciar o projeto, o servidor estará ouvindo no endereço `http://localhost/` e nos endereços IP do host local. Você pode acessar o servidor através de um navegador web ou utilizar ferramentas como `curl` ou `Postman` para enviar requisições HTTP.

## Autores

Este projeto foi criado para fins educacionais por [Marcelo](https://github.com/Mmarcelinho).

## Licença

Este projeto não possui uma licença específica e é fornecido apenas para fins de aprendizado e demonstração.
