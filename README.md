# ⚡ SayaxTask

This project is a full-stack web application created for demonstration purposes. It features a backend developed with Clean Architecture principles and a modern Angular-based frontend interface.

---

## 📚 Table of Contents

- 📁 [Project Structure](#project-structure)  
- 🚀 [Setup](#setup)
  - ⚙️ [Prerequisites](#prerequisites)
  - ▶️ [Running Locally](#running-locally)
- 💻 [Tech Stack](#tech-stack)
- ✅ [Features](#features)
- 🧱 [Architecture](#architecture)
- 🐳 [Running Database with Docker](#running-database-with-docker)
  - 🏃 [Running SQL Server](#running-sql-server)
  - ⚙️ [SQL Server Configuration](#sql-server-configuration)
  - 🔧 [CodeFirst with Dotnet CLI](#codefirst-with-dotnet-cli)
- 🐋 [Docker Compose](#docker-compose)
- 👨‍💻 [Developer](#developer)
- 📄 [License](#license)

---

## 📁 Project Structure

```
SayaxTask/
├── Client/              # Angular frontend (voltmeter-ui)
│   └── voltmeter-ui/
├── Server/              # .NET backend (voltmeter)
│ └── src/
├── .dockerignore
├── .gitignore
├── docker-compose.yml
├── LICENSE.txt
├── README.md
```

---

## 🚀 Setup

### ⚙️ Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
- [Node.js & NPM](https://nodejs.org/)  
- [Angular CLI](https://angular.io/cli)  
- [Docker](https://www.docker.com/) (for containerized usage)

### ▶️ Running Locally

#### Backend (.NET 9)

```bash
git clone https://github.com/tasbilek/SayaxTask.git
cd server
dotnet restore
dotnet run
```

#### Frontend (Angular 20)

```bash
cd client/voltmeter-ui
npm install
ng serve
```

App will be running at:  
`http://localhost:4200`

---

## 💻 Tech Stack

### Backend

- .NET 9  
- Clean Architecture  
- CQRS (Command Query Responsibility Segregation)  
- MediatR  
- Entity Framework Core
- SQL Server (MSSQL)

### Frontend

- Angular 20  
- RxJS  
- Bootstrap
- CSS  
- **ngx-toastr** — For displaying non-blocking toast notifications

---

## ✅ Features

- 🧠 **Domain-Driven & Clean Architecture**  
  Backend structured around Clean Architecture and DDD principles for better separation of concerns and testability.

- 🪝 **CQRS & MediatR-based Command/Query Handling**  
  All business actions are handled via the CQRS pattern using MediatR handlers.

- 📦 **Dockerized Deployment**  
  Docker Compose support for running the fullstack app locally in containers.

- 💻 **Modern Angular Frontend**  
  Intuitive and responsive user interface built with Angular 20.

---

## 🧱 Architecture

The backend is designed following Clean Architecture principles and consists of:

- **Domain** – Business rules and core entities  
- **Application** – CQRS-based use cases and MediatR handlers  
- **Infrastructure** – Database and external service integrations  
- **Web API** – Entry point for HTTP requests  

This structure enhances testability, maintainability, and separation of concerns.

---

## 🐳 Running Database with Docker

This project uses SQL Server inside separate Docker containers. You need to manually or with using compose to start database container using Docker commands.

### 🏃 Running SQL Server

To run SQL Server in Docker (if not already running locally), you can find it this address:
   `https://hub.docker.com/r/microsoft/azure-sql-edge`

Ensure that your application connection strings are correctly configured to connect to these database instances.

#### ⚙️ SQL Server Configuration

Ensure that the SQL Server container or your local SQL Server instance is running. You can configure the connection strings in the `appsettings.json` file for **SQL Server**.

If you are using a **SQL Server container**, the connection string should be configured as follows:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=host.docker.internal,1433;Database=YourDatabase;User Id=sa;Password=yourPassword123!"
}
```

- `host.docker.internal` allows the container to communicate with your local machine.
- `1433` is the default port for SQL Server.
- Replace `YourDatabase` with your actual database name, and use the appropriate `sa` password.

On Mac (and also on some Linux environments), host.docker.internal might not work as expected to refer to the host machine. In such cases, you can use your local machine's IP address instead.

To find your local machine's IP address on Mac, you can use the following command in the terminal:

```bash
ifconfig | grep inet
```

Look for the IP address associated with your network interface (typically en0 for Wi-Fi or en1 for Ethernet). Use this IP address in place of `host.docker.internal` in your connection string.

For example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=192.168.x.x,1433;Database=YourDatabase;User Id=sa;Password=yourPassword123!"
}
```
Where `192.168.x.x` is the IP address of your local machine.

This approach should work in environments where `host.docker.internal` is not resolving correctly.

If you're running SQL Server locally, you can adjust the connection string to point to your local instance.

#### 🔧 CodeFirst with Dotnet CLI

1. Create a migration to create the database schema from your models:

```bash
dotnet ef migrations add InitialCreate
```
2. Apply the migration to the database:

```bash
dotnet ef database update
```

---

## 🐋 Docker Compose

This project includes a `docker-compose.yml` file to easily run both frontend and backend services together.

### ▶️ How to Run

```bash
docker-compose up --build
```

Services included:

- `voltmeter-api`: .NET backend (on port 5001)  
- `voltmeter-ui`: Angular frontend (on port 4200)  
- `sql-server`: (optional) MSSQL container (port 1433)

### 🛑 How to Stop

```bash
docker-compose down
```

> Ensure Docker and Docker Compose are installed on your system.

---

## 👨‍💻 Developer

**Muhammed Orhan Taşbilek**  
GitHub: [@tasbilek](https://github.com/tasbilek)
LinkedIn: [@tasbilek](https://www.linkedin.com/in/tasbilek)
Email: muhammedorhantasbilek@gmail.com

---

## 📄 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

