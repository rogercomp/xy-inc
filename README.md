# xy-inc

Aplicação de Manutenibilidade de POI

Utilizado linguagem C#, ASP NET Core 2, Entity Framework, SQL Server(Express), Code Fisrt, LINQ, MVC 6

Caso não possua o VS 20017 code faça o download no link abaixo:

https://code.visualstudio.com/


Abrir o projeto XYApp

Abrir a pasta Data na raiz do projeto edite o arquivo POIContexto.cs identificando e modificando a linha abaixo:

  private readonly string strConexao = "Server=SERVIDOR;Database=DB_XYAPPPOI;Trusted_Connection=True;MultipleActiveResultSets=true";
      
  onde SERVIDOR será o nome do servidor SQL SERVER
  
  compilar e executar o projeto.
  
 Teste de Unidade
 
 Utilizado XUnit/Bogus/ para realizar testes de unidade
 
 Abrir pasta do projeto Tests por linha de comando 
 
 executar o comando dotnet test e verificar a execução dos testes
