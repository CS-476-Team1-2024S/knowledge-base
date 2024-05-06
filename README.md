# Read before use!
[Microsoft Guide](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio-code) - for basic setup/more info (if local testing)

Check [here](https://github.com/CS-476-Team1-2024S/website_ui/blob/LB-addingUserAPI/src/hooks/Login.js) for and example on how to implement this on front-end (react) 

For {host}:</br>
!Must be on washington VPN unless you set 'localhost' on API AND on front-end!</br>
Using Washington VPN? Use: 140.146.23.39</br>
!If you encounter issues fetching, go to https://140.146.23.39:5001/Directory/Scan and view webpage, then try again!

## Users
Add - https://{host}:5001/User/Add
```
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
{
    "directoryInfo":
    {
        "path": ""
    }
}
```

Move - https://{host}:5001/Directory/Move
```
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
{
    "fileInfo":
    {
        "path": ""
    }
}
```

Move - https://{host}:5001/File/Move
```
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
{
    "fileInfo":
    {
        "path": ""
    }
}
```

Write - https://{host}:5001/File/Write
```
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
{
    "fileInfo":
    {
        "path": ""
    }
}
```

## Search
Query - https://{host}:5001/Search/Query
```
{
    "searchInfo":
    {
        "query": ""
    }
}
```