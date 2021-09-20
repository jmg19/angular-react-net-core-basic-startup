import React from "react";

export class Home extends React.Component{
    render(){
        return(
            <div className="w3-container w3-margin-top">
                <h1>Hello, world!</h1>
                <p>Welcome to this base project prototype</p>
                <p>This project is built with:</p>
                <ul>
                <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
                <li><a href='https://reactjs.org/'>React</a> and <a href='https://www.typescriptlang.org/docs/handbook/react.html'>TypeScript</a> for client-side code</li>
                <li><a href='https://www.w3schools.com/w3css/default.asp'>w3css</a> for layout and styling</li>
                </ul>
                                
                <p>This is a simple app that creates new users and list them all if you are logged in. You can start by creating a new User</p>                
            </div>
            );
    }
}