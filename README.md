# 🌐 All In One Dotnet Backend

Welcome to **All In One Dotnet Backend**, a scalable backend solution built using .NET 8 and following the Clean
Architecture principles. This repository is designed to ensure scalability, maintainability, and ease of deployment
across various environments.

## 📋 Table of Contents

- [About the Project](#about-the-project)
- [Architecture Overview](#architecture-overview)
- [Technologies Used](#technologies-used)
- [Running the Application](#running-the-application)
    - [Running Locally](#running-locally)
    - [Running with Docker Compose](#running-with-docker-compose)
    - [Running with Dockerfile](#running-with-dockerfile)
- [Deploying the Application](#deploying-the-application)
- [Contributing](#contributing)
- [License](#license)

## 📖 About the Project

**All In One Dotnet Backend** is a clean and modular backend solution tailored for scalability and performance. The
architecture is designed to be extendable, following best practices for separation of concerns and ensuring a high level
of maintainability.

## 🏗️ Architecture Overview

The solution follows the **Clean Architecture** principles, which ensures that the core of the application remains
independent of external frameworks, databases, and user interfaces. The architecture is structured as follows:

- **Core**: Contains business logic and domain models.
- **Application**: Manages the application's behavior and use cases.
- **Infrastructure**: Handles data persistence, external services, and other infrastructure concerns.
- **Web API**: The entry point for external interactions, exposing endpoints and handling HTTP requests.

## 🚀 Technologies Used

- **.NET 8** - The latest version of .NET for building high-performance applications.
- **Entity Framework Core** - For object-relational mapping (ORM) and data access.
- **SQL Server** - Primary database for persistence.
- **Redis** - For caching and improving application performance.
- **Docker** - Containerization of the application for easy deployment.
- **Swagger** - API documentation and testing tool.

## 🛠️ Running the Application

### Running Locally

Follow these steps to run the application locally.

1. **Run SQL Server Container**:
    ```shell
    docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1234@Abcd" -p 20022:1433 --name sql22 -d mcr.microsoft.com/mssql/server:2022-latest
    ```

2. **Run Redis Container**:
    ```shell
    docker run -p 16379:6379 -d redis
    ```

3. **Configure Connection Strings**:
    - Ensure that the connection strings in `appsettings.json` (located in `src/API/AIO.Web.API/appsettings.json`) are
      correctly configured.

### Running with Docker Compose (Under Development)

You can also run the application using Docker Compose. Simply run:

```shell
docker-compose up
```

This option is still under development and may require further configuration.

### Running with Dockerfile

To run the application using a Dockerfile, follow these steps:

Build the Docker Image:

```shell
docker build . -f "src\API\AIO.Web.API\Dockerfile" --force-rm -t aio-dotnet-webapi
```

Verify the Docker Image:

```shell
docker images
```

Run the Docker Container:

```shell
docker run -it --rm -p 3000:8080 --name aio-dotnet-webapi-container aio-dotnet-webapi
```

Access Swagger UI:

Open your browser and navigate to: http://localhost:3000/swagger

## 🚢 Deploying the Application

Deploying the application involves building the Docker image and running it on the target environment.

Build the Docker Image:

```shell
docker build . -f "src\API\AIO.Web.API\Dockerfile" -t aio-dotnet-webapi
```

Save the Docker Image:

```shell
docker save -o "./aio-dotnet-webapi-latest" aio-dotnet-webapi:latest
```

Upload the Image via SFTP.

Load the Docker Image:

```shell
docker load -i './aio-dotnet-webapi-latest'
```

Drop and Prepare the Database (If Required).

Stop and Remove Previous Containers:

```shell
docker stop aio-dotnet-webapi-container
docker rm aio-dotnet-webapi-container
```

Run the New Container:

```shell
docker run -d -v './src/API/AIO.Web.API/Dockerfile/appsettings.json:/app/appsettings.json' -p 20080:8080 --name aio-dotnet-webapi-container aio-dotnet-webapi
```

Configure appsettings.json:

```shell
copy appsettings.Development.json appsettings.json
nano appsettings.json
```

# 🤝 Contributing

Contributions are welcome!

If you find any issues or want to add new features, feel free to fork the repository and submit a pull request.

# 📜 License

This project is licensed under the Apache License Version 2.0 License.

See the [LICENSE](LICENSE) file for details.

### Highlights:

- **Structured Content**: The README is neatly organized into sections, making it easy to navigate.
- **Clear Instructions**: Each step for running and deploying the application is clearly outlined, ensuring ease of use.
- **Aesthetic Markdown**: Emojis and formatting enhance readability and visual appeal.
- **Comprehensive Overview**: Includes all necessary sections such as architecture, technologies, and deployment
  instructions.

This README serves as a user-friendly guide to set up, run, and deploy the All In One Dotnet Backend, while also
offering a clear overview of the project's structure and purpose.

### Todo:

list of features to implement:

- [ ] add identification
- [ ] add JWT support
- [ ] add 3rd party auth (Social) support
- [ ] add PBAC authorization
- [ ] add Logging on ELK or sentry
- [ ] add captcha support
- [ ] add filters and search
- [ ] add pagination
- [ ] add generators
- [ ] add unit test
- [ ] add file management
- [ ] add personalization
- [ ] add settings
- [ ] add cache
- [ ] add support for other database
- [ ] add multi-factor auth support (authenticator)
- [ ] add RabbitMQ support
- [ ] add Polly with refit for API calls
- [ ] add multi logging drivers
- [ ] add queue and scheduler
- [ ] add notification center
- [ ] add push notification
- [ ] add realtime
- [ ] add encryption
- [ ] add payment and wallet
- [ ] add process manager
- [ ] add rule engine
 