# Dotnet Core Clean Architecture

This repository is used for demonstrating how to implement Clean Architecture approach on Dotnet Core Web API development.

The architecture of this application is heavily inspired by [Jason Taylor](https://github.com/jasontaylordev)'s [idea](https://github.com/jasontaylordev/CleanArchitecture/) about pragmatic approach to the **ASP.NET Core Clean Architecture**. We add some twists to only support API Development.

![image](https://i.ibb.co/zx4dVMX/download.png)

It divides the application into 4 modules: `Application`, `Domain`, `Infrastructure`, and `Presenter` as described in the structure below:

```
+ Application
    + Exceptions
    + Extensions
    + Infrastructures
    + Interfaces
    + Misc
    + Models
    + UseCases

+ Domain
    + Entities
    + Infrastructures

+ Infrastructure
    + Authorization
    + ErrorHandler
    + FileManager
    + Notification
    + Persistentences

+ Presenter
    + Controllers
```

## Usage

Create a database for this example, and then edit `appsettings.json` and add approriate data on `WebApiDatabase`

### Install Packages

    ~$ dotnet restore

### Run Migrations

    ~$ dotnet ef database update

### Build and Run Application

    ~$ dotnet build && dotnet run

And you can access the application through `http://127.0.0.1:5000`


## API

### Get all users

    ~$ curl --location -g --request GET 'http://127.0.0.1:5000/users?paging[page]=1&paging[limit]=5&sort[column]=id&sort[type]=asc'

### Get a user by ID

    ~$ curl --location --request GET 'http://127.0.0.1:5000/users/1'

### Create a user

 ```
 ~$ curl --location --request POST 'http://127.0.0.1:5000/users/' \
--header 'Content-Type: application/json' \
--data-raw '{
    "Data": {
        "name": "Hans",
        "email": "hans@gmail.com",
        "phone": "08569542321",
        "age": 20,
        "file_byte": "[base64 encoded]",
        "user_name": "hans"
    }
}'
 ```


### Update a user

```
~$ curl --location --request PATCH 'http://127.0.0.1:5000/users/3' \
--header 'Content-Type: application/json' \
--data-raw '{
    "Data": {
        "name": "Hans Maatita",
        "email": "hans@gmail.com",
        "phone": "08596699969",
        "age": 21,
        "file_byte": "[base64 encoded]",
        "user_name": "hansome"
    }
}'
```

### Delete a user

```
~$ curl --location --request POST 'http://127.0.0.1:5000/users/delete' \
--header 'Content-Type: application/json' \
--data-raw '{
    "ids": [4]
}'
```