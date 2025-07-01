# Dockerized ASP.NET Core Web API with MongoDB

This project is a fully Dockerized ASP.NET Core Web API connected to MongoDB. It provides a simple RESTful service for managing user or employee data, ideal as a boilerplate for microservices or modern .NET backend projects.

---

## 📦 Tech Stack

- **ASP.NET Core 8.0**
- **MongoDB**
- **Docker & Docker Compose**

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

---
## 🛠️ Project Structure
```
├── Dockerfile # Builds the ASP.NET Core app
├── docker-compose.yml # Runs API + MongoDB together
├── .dockerignore
├── src/
│ └── CrudOperationwithmongodbApi/ # Your ASP.NET Core Web API source
│ ├── Controllers/
│ ├── Models/
│ └── Program.cs
```

---

## ⚙️ Build and Run with Docker

1. **Clone the repo:**

```bash
git clone https://github.com/<your-username>/Dockerized_ASP.NET_Core_Web_API_with_MongoDB_and_GitHub_Actions.git
cd Dockerized_ASP.NET_Core_Web_API_with_MongoDB_and_GitHub_Actions
```
2. **Build and run the containers:**
```
docker-compose up --build
```
This will:

Build your ASP.NET Core API image

Pull and run the latest MongoDB image

Expose API on http://localhost:8080

## 🌐 Access the API
Once running, go to:
```
http://localhost:8080/swagger
```
You’ll see the Swagger UI for interacting with the API.

## 🧪 Environment Variables
These are configured in docker-compose.yml:
```
- MongoConfig__ConnectionString=mongodb://mongo:27017
- MongoConfig__DatabaseName=EmployeeDb
- MongoConfig__CollectionName=UserDetails
- ASPNETCORE_ENVIRONMENT=Development
- ASPNETCORE_URLS=http://+:80
```
## 📦 MongoDB Data Persistence
MongoDB stores data in a Docker volume called mongodbdata, defined in the docker-compose.yml:
```
volumes:
  mongodbdata:
```
This ensures your data survives container restarts.

## 🧹 Clean Up
To stop and remove containers and volumes:
```
docker-compose down -v
```
