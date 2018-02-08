# xy-inc

Aplicação de Manutenibilidade de POI

Utilizado linguagem C#, ASP NET Core 2, Entity Framework, SQL Server(Express), Code Fisrt, LINQ, MVC 6

Fazer o download do vs2017 code no link abaixo:
https://code.visualstudio.com/


Abrir o projeto 

Abrir a pasta Data na raiz do projeto edite o arquivo POIContexto.cs identificando e modificando a linha abaixo:

  private readonly string strConexao = "Server=SERVIDOR;Database=DB_XYAPPPOI;Trusted_Connection=True;MultipleActiveResultSets=true";
      
  onde SERVIDOR será o nome do servidor SQL SERVER
  
  compilar e executar o projeto.
