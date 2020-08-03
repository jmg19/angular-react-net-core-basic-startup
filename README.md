# angular-net-core-basic-startup
Angular + .Net Core Base Project with some TDD, DDD and CQRS approaches

Make Demo Run:
1) Clean and Build the Visual Studio Solution
2) Select BaseStartupProject.Infrastructure as startup project
3) Open Package Manager Console, select BaseStartupProject.Infrastructure as default Project.
4) Run Command "Update-Database". Local Database should be created.
5) In visual studio on DataBase Connections add a new Sql Server Connection with server name "(LocalDb)\MSSQLLocalDB" and database "BaseStartupDemo"
6) Open a new query and run "INSERT INTO [dbo].[Configurations] ([Name], [Value]) VALUES (N'tokenEncriptKey', N'[SOME_ENCRIPTION_KEY]')"
7) Select BaseStartupProject.API as startup project
8) Run BaseStartupProject.API
9) open "AngularApp" folder
10) make sure you have Node.js and Angular Cli installed in your machine
11) run "npm i"
12) run "ng serve"
13) Open in a browser the Angular App in "localhost:4200"
14) If all this don't work just insult me =D


I hope you like this project and that it will be useful to start your next dream project.
If you don't like it just insult me =D

Peace and love to all.
