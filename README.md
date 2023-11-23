# Vax.Net
- MENU
  - [Descrição](#descrição)
  - [Formulário](#formulário)
    - [Models](#models)
      - [Telefone](#telefone)
      - [Endereço](#endereço)

  ## Descrição


  ---

  ## Formulário
  
  ### Models
  
  #### Telefone
  Para o Telefone criamos os determinados campos:
  ```js
    int Id;
    int Ddd;
    int Numero;
  ```
  Eles são consumidos pelo o Usuário.
  #### Endereço
   Para o Endereço criamos os determinados campos:
  ```js
    int Id;
    EstadoEnum Estado;
    string Cidade;
    string Logradouro;
  ```
  O EstadoEnum é um enum com as siglas dos Estados brasileiro.O Endereço é consumido pelo o Usuário.
  
