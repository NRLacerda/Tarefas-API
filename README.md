<h1>Tarefas API - .NET & Entity Framework</h1>
<h3>Por Nicolas Rocha Lacerda</h3>

1- Para utilizar a aplicação, tenha em sua máquina o SQLServer com as seguintes configurações><br>
user: 'api',<br>
password:'@n1Mseguranza',<br>
database: 'Taskapi'<br>
Após instalação e configuração execute os seguintes comandos no <strong> Package Manager Console.</strong>
Add migration -Context TaskManagerDBContext><br>
Update-database -Context TaskManagerDBContext<br>

2- Estes comandos irão instanciar e criar as tabelas do banco de dados.

3- Após isso é só executar a API.
