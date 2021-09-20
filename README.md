# angular-react-net-core-basic-startup
Angular/React + .Net Core Base Project with some TDD, DDD and CQRS approaches

Make Demo Run:
1) Clean and Build the Visual Studio Solution
2) Select BaseStartupProject.Infrastructure as startup project
3) Open Package Manager Console, select BaseStartupProject.Infrastructure as default Project.
4) Run Command "Update-Database". Local Database should be created.
5) In visual studio on DataBase Connections add a new Sql Server Connection with server name "(LocalDb)\MSSQLLocalDB" and database "BaseStartupDemo"
6) Open a new query and run "INSERT INTO [dbo].[Configurations] ([Name], [Value]) VALUES (N'tokenEncriptKey', N'[SOME_ENCRIPTION_KEY]')"
7) Select BaseStartupProject.API as startup project
8) Run BaseStartupProject.API

If you want to run the Angular APP:
1) open "angular-app" folder
2) make sure you have Node.js and Angular Cli installed in your machine
3) run "npm i"
4) run "ng serve"
5) Open in a browser the Angular App in "localhost:4200"

If you want to run the React APP:
1) open "react-app" folder
2) make sure you have Node.js
3) run "npm i"
4) react-scripts start
5) Open in a browser the React App in "localhost:3000"


I hope you like this project and that it will be useful to start your next dream project.