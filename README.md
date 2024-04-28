# Read before use!
[Microsoft Guide](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code) - for basic setup/more info (if local testing)

Check [here](https://github.com/CS-476-Team1-2024S/website_ui/blob/LB-addingUserAPI/src/hooks/Login.js) for and example on how to implement this on front-end (react) 

For {host}:</br>
Local testing? Use: localhost</br>
Using Washington VPN? (local)? Use: 140.146.23.39</br>
Publishing to webserver? Use: 140.146.23.39

## Users
Add - https://{host}:5001/User/Add
```
(Request body)
{
    "userInfo":
    {
        "username": "",
        "password": "",
        "accessLevel": ""
    }
}
```

Login - https://{host}:5001/User/Login
```
(Request body)
{
    "userInfo":
    {
        "username": "",
        "password": ""
    }
}
```

Remove - https://{host}:5001/User/Remove
```
(Request body)
{
    "userInfo":
    {
        "username": ""
    }
}
```


## Directories
Create - https://{host}:5001/Directory/Create
```
(Request body)
{
    "directoryInfo":
    {
        "path": ""
    }
}
```

Move - https://{host}:5001/Directory/Move
```
(Request body)
{
    "directoryInfo":
    {
        "source": "",
        "destination": ""
    }
}
```

Delete - https://{host}:5001/Directory/Delete
```
(Request body)
{
    "directoryInfo":
    {
        "path": ""
    }
}
```

Scan - https://{host}:5001/Directory/Scan
```
Returns directories and files in 'root'
```

## Files
Create - https://{host}:5001/File/Create
```
(Request body)
{
    "fileInfo":
    {
        "path": ""
    }
}
```

Move - https://{host}:5001/File/Move
```
(Request body)
{
    "fileInfo":
    {
        "source": "",
        "destination": ""
    }
}
```

Delete - https://{host}:5001/File/Delete
```
(Request body)
{
    "fileInfo":
    {
        "path": ""
    }
}
```

Write - https://{host}:5001/File/Write
```
(Request body)
{
    "fileInfo":
    {
        "path": "",
        "content": "",
        "append": "true || false"
    }
}
```

Read - https://{host}:5001/File/Read
```
(Request body)
{
    "fileInfo":
    {
        "path": ""
    }
}
```
