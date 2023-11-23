# Vax.Net
- MENU
  - [Descrição](#descrição)
  - [Formulário](#formulário)
    - [Models](#models)
      - [Telefone](#telefone)
      - [Endereço](#endereço)
      - [Usuário](#usuário)
      - [Vacina](#vacina)
      - [Status Vacina](#status-vacina)
      - [Formulário Vacina](#formulário-vacina)
    - [Data](#data)
      - [Data Context](#data-context)

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
#### Usuário
Para o Usuário criamos os determinados campos:
```js
  int Id;
  string Nome;
  string Email;
  DateTime DataNascimento;
  GeneroEnum Genero;
  Endereco Endereco;
  Telefone Telefone;
  ICollection<StatusVacina>? StatusVacinas;
```
O GeneroEnum é o gênero biológico do usuário usuário que também acrescemos o "Outro" para quem não se sentir confortável em informar o seu gênero biológico, está consumindo os dados de Endereço e Telefone de classes previamente criadas. O collection é uma coleção de todos os StatusVacinas que o determinado usuário possui, que por razões de lógica é possível ser nula.
#### Vacina
Para a Vacina criamos os determinados campos:
```js
  int Id;
  string Nome;
  TipoVacinaEnum Tipo;
  CategoriaVacinaEnum Categoria;
  DoseVacinaEnum Dose;
  ICollection<StatusVacina>? StatusVacinas;
```
O TipoVacinaEnum é os tipos de Vacinas que são tomadas pelo plano de vacinação brasileiro, onde dentro desse enum foi criado o atributo descrição que tem uma breve descrição da vacina. O CategoriaVacinaEnum diz qual etapa essa vacina faz parte(criança, adolescente, adulto,idoso). O DoseVacinaEnum diz qual é a dose que está sendo tomada. O collection é uma coleção de todos os StatusVacinas que o determinada vacina possui, que por razões de lógica é possível ser nula. 
#### Status Vacina
Para o StatusVacina criamos os determinados campos:
```js
  int Id;
  Vacina Vacina;
  Usuario Usuario;
  bool Status;
```
O StatusVacina recebe as informações do Usuário e da Vacina para cada vacina possível dentro do sistema, e tem o campo de Status que é um booleano que se for verdadeiro diz que a vacina foi tomada e se for falso que ela não foi tomada, que para a lógica do projeto vai ser fundamental para indicar quais vacinas ainda não foram tomadas.
#### Formulário Vacina
Para o FormVacina temos os determinados campos:
```js
  int Id;
  string Nome;
  string Descricao;
  DoseVacinaEnum Dose;
  bool Status;
```
O FormVacina é um formulário que vai ser utilizado para exibir o formulário de vacinas a ser preenchido, e junto com o FormVacina existe o CategoriaVacinaViewModel que possui os determinados campos:
```js
  string CategoriaNome;
  Dictionary<string, List<FormVacina>> Tipos;
```
O CategoriaVacinaViewModel existe para auxiliar na exibição do formulário de forma ordenada na View do Formulário.

---
### Data
#### Data Context
Para a persistência em banco de dados, estamos usando o Oracle DataBase que é disponibilizado pela FIAP, para comunicar com o banco de dados utilizamos dos seguintes frameworks:
**Microsoft.EntityFrameworkCore.Design**
**Microsoft.EntityFrameworkCore.Tools**
**Oracle.EntityFrameworkCore**
