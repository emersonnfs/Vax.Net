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
    - [Services](#services)
      - [Inicialização de Data](#inicialização-data)
    - [Controllers](#controllers)
      - [Usuário Controller](#usuário-controller)
      - [Status Vacina Controller](#status-vacina-controller)

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

`Microsoft.EntityFrameworkCore.Design`
`Microsoft.EntityFrameworkCore.Tools`
`Oracle.EntityFrameworkCore`

Com esses frameworks foi possível construir as Entidades no banco de dados implementando o DbContext do EntityFrameWork da Microsoft, onde sobrescrevemos a ModelBuilder do framework adequando a modelagem das tabelas a serem persistidas e passamos as tabelas a serem cadastradas que são as seguintes:
```js
  DbSet<Usuario> Usuarios;
  DbSet<Vacina> Vacinas;
  DbSet<Endereco> Enderecos;
  DbSet<StatusVacina> StatusVacinas;
  DbSet<Telefone> Telefones;
```
Dessa forma temos as tabelas a cima cadastradas no banco de dados e com o auxílio da DbContext manipuladas facilmente.

---
### Services
#### Inicialização Data
Para o DataInitializationService, utilizamos da biblioteca do Sistema chamada IServiceProvider para criar um escopo do banco de dados, especificamente para a tabela Vacinas, com isso sempre que o nosso projeto é inicializado, o DataInitializationService vai conferir a tabela Vacinas e caso ela esteja vazia, ele irá implementar 48 vacinas, que são as variações de vacinas que existem no projeto sendo implementado como no exemplo a baixo:
```js
  using (var scope = _serviceProvider.CreateScope())
  {
      var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
  
      if (!dbContext.Vacinas.Any())
      {
          //Vacina BCG
          var vacina = new Vacina 
          { 
              Id = 1, 
              Nome = "BCG", 
              Tipo = TipoVacinaEnum.BCG, 
              Categoria = CategoriaVacinaEnum.Criança, 
              Dose = DoseVacinaEnum.DoseUnica 
          };
          dbContext.Vacinas.Add(vacina);
          /// Demais vacinas
          dbContext.SaveChanges();
    }
  }
```
Dessa forma é possível eu já ter esses dados persistidos quando o projeto for inicializado, que tem um função muito importante enquanto o projeto está em produção, para poder manipular os dados nesse projeto.

---
### Controllers
#### Usuário Controller
Para a UsuarioController nós implementamos a classe Controller do Microsoft.AspNetCore.Mvc e dentro dela criamos os seguintes métodos:
```js
  IActionResult Index();
  IActionResult Cadastrar();
  IActionResult Cadastrar(Usuario usuario);
  int CadastrarEndereco(Endereco endereco);
  int CadastrarTelefone(Telefone telefone);
  IActionResult Editar(int id);
  IActionResult Editar(Usuario usuario);
  IActionResult Remover(int id);
```
`IActionResult Index()` - Retornamos a lista de Usuários incluindo o Endereço e o Telefone.

`IActionResult Cadastrar()` - Retornamos a View de Cadastrar.

`IActionResult Cadastrar(Usuario usuario)` - Nesse método recebemos um Usuário e fazemos o cadastro de Endereço e Telefone que vão retornar o seus respectivos Id's, com isso cadastramos nos campos EnderecoId e TelefoneId esses Id's, e assim cadastramos o usuário e dependendo do gênero do usuário adiconamos 47 ou 48 vacinas relacionadas ao StatusVacina, retornando um direcionamento para página de Index.

`int CadastrarEndereco(Endereco endereco)` - Nesse método recebemos o Endereço que é adicionado e nos retorna o Id dele, para ser Utilizado no cadastro do Usuário.

`int CadastrarTelefone(Telefone telefone)` - Nesse método recebemos o Telefone que é adicionado e nos retorna o Id dele, para ser Utilizado no cadastro do Usuário.

`IActionResult Editar(int id)` - Nesse método é passado o id de determinado Usuário, e caso ele exista retornamos o Objeto Usuário incluindo o Endereço e o Telefone, que vão ser editados.

`IActionResult Editar(Usuario usuario)` - Nesse método se recebe o Usuário atualizado, é feito a busca do Usuário de mesmo Id, e atualizado os campos desse Usuário pelos novos dados, e é retornado um redirecionamento para a página de Index.

`IActionResult Remover(int id)` - Nesse método é recebido o id de um Usuário, caso ele exista, esse Usuário é removido do banco de dados, é emitido a mensagem de "Usuário removido!" de sucesso, e é redirecionado para página de Index;

---
#### Status Vacina Controller
Para a StatusVacinaController nós implementamos a classe Controller do Microsoft.AspNetCore.Mvc e dentro dela criamos os seguintes métodos:
```js
  IActionResult Formulario(int id);
  Dictionary<TipoVacinaEnum, List<FormVacina>> ListarVacinas(List<StatusVacina> statusVacinas, CategoriaVacinaEnum categoriaVacinaEnum);
  DescricaoVacinaAttribute ObterDescricaoVacina(TipoVacinaEnum vacinaEnum);
  IActionResult AtualizarStatus(Dictionary<string, Dictionary<int, bool>> statusVacinas);
  IActionResult Pendentes(int id);
  int ContagemVacinas(List<StatusVacina> listaStatusVacinas, TipoVacinaEnum tipo);
  bool PendenteBCG(List<StatusVacina> listaStatusVacina);
  bool PendenteHepatiteB(List<StatusVacina> listaStatusVacinas);
  bool PendentePentavalente(List<StatusVacina> listaStatusVacinas);
  bool PendentePoliomielite(List<StatusVacina> listaStatusVacinas);
  bool PendentePneumococica(List<StatusVacina> listaStatusVacinas);
  bool PendenteRotavirus(List<StatusVacina> listaStatusVacinas);
  bool PendenteMeningococicaC(List<StatusVacina> listaStatusVacinas);
  bool PendenteFebreAmarela(List<StatusVacina> listaStatusVacinas);
  bool PendenteTripliceViral(List<StatusVacina> listaStatusVacinas);
  bool PendenteHepatiteA(List<StatusVacina> listaStatusVacinas);
  bool PendenteTetraViral(List<StatusVacina> listaStatusVacinas);
  bool PendenteHPV(List<StatusVacina> listaStatusVacinas);  
```
`IActionResult Formulario(int id)` - Nesse método é recebido o id de um Usuário, caso ele exista, vai ser buscado uma lista de StatusVacina que vai conter todas as vacinas desse Usuário, então será feito um mapeamento em primeira instância do CategoriaVacinaEnum( se é criança, adolescente, etc.), em segunda instância do TipoVacinaEnum e dentro dessa segunda instância sera convertido os StatusVacina's em FormVacina, para melhor exibição da informações. Com isso é utilizado a idade do Usuário para saber quais vacinas vão está adequadas a sua idade, e com isso é retornado um Dictionary<string, Dictionary<string, List<object>>>.

`Dictionary<TipoVacinaEnum, List<FormVacina>> ListarVacinas(List<StatusVacina> statusVacinas, CategoriaVacinaEnum categoriaVacinaEnum);` - Nesse método é recebido uma Lista de StatusVacina e um CategoriaVacinaEnum, e nele vai ajustar o Dictionary<string, Dictionary<string, List<object>>> para ter o FormVacina com as informações de descrição e ordenado por tipo e eles nos retorna um Dictionary<TipoVacinaEnum, List<FormVacina>>.

`DescricaoVacinaAttribute ObterDescricaoVacina(TipoVacinaEnum vacinaEnum)` - Nesse método é recebido um TipoVacinaEnum, e nele vai retornar a descrição desse tipo de Vacina.

`IActionResult AtualizarStatus(Dictionary<string, Dictionary<int, bool>> statusVacinas)` - Nesse método é recebido um Dictionary<string, Dictionary<int, bool>>, e vai atualizar com os dados do Formulário, no Status do StatusVacina, e se tudo ocorrer bem ele retorna uma mensagem "Formulário Atualizado com Sucesso!" e um Ok, pois nesse método foi passado as informações via AJAX.

`IActionResult Pendentes(int id)` - Nesse método é recebido um id de um Usuário, caso esse Usuário exista, é buscado uma lista de StatusVacina desse determinado Usuário e será validado com base nos métodos que são do tipo booleano que começam com Pendente... e será adionado em uma Lista de string aqueles que retornarem false.

`int ContagemVacinas(List<StatusVacina> listaStatusVacinas, TipoVacinaEnum tipo)` - Nesse método é recebido uma lista de StatusVacina e um TipoVacinaEnum, e será retornado a quantidade de vezes que o determinado TipoVacinaEnum tem o valor true, e com isso será utilizado na validação das vacinas.

`bool PendenteBCG(List<StatusVacina> listaStatusVacina)`, `bool PendenteHepatiteB(List<StatusVacina> listaStatusVacina)`, `bool PendentePentavalente(List<StatusVacina> listaStatusVacina)`, `bool PendentePoliomielite(List<StatusVacina> listaStatusVacina)`, `bool PendentePneumococica(List<StatusVacina> listaStatusVacina)`, `bool PendenteRotavirus(List<StatusVacina> listaStatusVacina)`, `bool PendenteMeningococicaC(List<StatusVacina> listaStatusVacina)`, `bool PendenteFebreAmarela(List<StatusVacina> listaStatusVacina)`, `bool PendenteTripliceViral(List<StatusVacina> listaStatusVacina)`, `bool PendenteHepatiteA(List<StatusVacina> listaStatusVacina)`, `bool PendenteTetraViral(List<StatusVacina> listaStatusVacina)` e `bool PendenteHPV(List<StatusVacina> listaStatusVacina)` - Nesses métodos são recebidos uma Lista de StatusVacina, e com isso é feito a contagem de vacinas tomadas do tipo no método, e com isso é implementado a lógica de cada uma e se estiver pendente essa Vacina é retornado false, caso contrário retorna true.

---
