# Sistema de Locadora

## Um sistema completo de locadora feito para uma etapa do processo seletivo da empresa stefanini.

### Banco de dados
- SQL Servcer

### Backend (API)
- C# - .NET 6 
- DDD
- CQRS com MediatR
- EF Core
- Swagger
- Logging
- Fluent Validation
- Automapper
- Testes:
  - Unitários
  - Integração
  
### Frontend (Web)
  - React
  - Typescript
  - Ant Design
  - Axios
  - React Toastify
  
### Docker
- Docker Compose

### Como executar o sistema?
  - Primeiramente certifique-se que tenha Docker instalado na sua máquina:
     - Você consegue saber CMD (command prompt)
     - Executando o comando "docker --version", caso ele retorne a versão, então seu Docker esta instalado.
  - Clone ou baixe o arquivo deste repositório, clicando no botão "code": <br />
    ![image](https://user-images.githubusercontent.com/60172584/178804772-1ef6fc4d-2418-49c8-b87b-2bfc815e3318.png)
  - Após o sistema devidamente baixado na sua máquina, entre na sua pasta raiz, sendo ela essa: <br />
    ![image](https://user-images.githubusercontent.com/60172584/178804850-cd9d2655-251b-4c4f-af36-ef2f775d427d.png)
  - Na pasta informada clique com o botão direito no mouse e selecione a opção "Open in Terminal"
  - Com o terminal aberto, execute o comando "docker compose up" e pronto, só precisa esperar
  - Link que o sistema (web) estara sendo executado: http://localhost:3001
  - Link do swagger com cada endpoint documentado: http://localhost:5000/swagger/index.html

### Como subir o ambiente para executar os testes de integração?
 - Entre na pasta Backend/Tests.Integration
 - Execute o comando "docker compose up" assim que o ambiente subir, você pode executar normalmente os testes de integração
