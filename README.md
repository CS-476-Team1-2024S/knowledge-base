# Read and set up before use!
[Microsoft Guide](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code) - for basic setup/more info

## Accessing the APIs (Users, use without quotes)
Adding - https://localhost:5001/User/Add/"username"/"password"/"accesslevel"

Verifying - https://localhost:5001/User/Login/"username"/"password"

Deleting - https://localhost:5001/User/Remove/"username"

### Notes
Users are the only APIs currently set up. <br>
When new users are added, "Users.db" is renewed; deleting any users previously inside file. <br>
