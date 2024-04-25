# Read and set up before use!
[Microsoft Guide](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code) - for basic setup/more info

## Accessing the APIs (Users, use without quotes, port might be different)
Adding - https://localhost:7078/User/Add/"username"/"password"/"accesslevel"

Verifying - https://localhost:7078/User/Login/"username"/"password"

Deleting - https://localhost:7078/User/Remove/"username"

### Notes
Users are the only APIs currently set up. <br>
When new users are added, "Users.db" is renewed; deleting any users previously inside file. <br>