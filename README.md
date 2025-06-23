# ConsultaCEP

Este repositório contém uma API RESTful desenvolvida com **ASP.NET Core**, utilizando **DTOs**, **AutoMapper**, **Entity Framework Core** e integração com a API pública **ViaCEP** para gerenciamento de **clientes**, seus **contatos** e **endereços**.

---

## 📦 Estrutura do Projeto

- `DTOs` – Objetos de transferência de dados (entrada/saída).
- `Entities` – Entidades do domínio.
- `Services` – Lógica de negócios com interfaces desacopladas.
- `Controllers` – Endpoints RESTful.
- `AutoMapper` – Mapeamento entre entidades e DTOs.

---

## ConsultaCEP - Configuração Inicial

1. **Configurar a string de conexão**

   Abra o arquivo `appsettings.json` e configure a seção `ConnectionStrings` com os detalhes do seu banco de dados My SQL:

   ```json
   "ConnectionStrings": {
      "AppDbConnectionString": "Server=localhost; Database=ConsultaCEP; User=root; Password=root;"
    }
   ```

2. **Rodar as migrações**

   No terminal, execute os seguintes comandos para aplicar as migrações e configurar o banco de dados:

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
   
---

![image](https://github.com/user-attachments/assets/053975a9-a26d-4fe8-930c-899f8d8bcec7)

![image](https://github.com/user-attachments/assets/95b40fd4-165f-4821-bc09-a74ead966477)

![image](https://github.com/user-attachments/assets/abb195e0-25b5-493d-bbf5-f76495bf1183)


