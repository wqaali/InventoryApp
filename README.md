# InventoryApp

InventoryApp is a sample Technical Assignment **inventory tracking system** consisting of:

- **Backend (`LiveStock`)**: ASP.NET Core 8.0 API plus SignalR hub for live stock updates.
- **Business layer (`LiveStockBL`)**: Domain models and services for stock operations.
- **Frontend (`InventoryApp-frontend`)**: React (Create React App + TypeScript) SPA that displays and updates stock in real time.

The solution is designed to be container-friendly, with separate Dockerfiles for the backend and frontend.

---

## Project structure

- `LiveStock/` – ASP.NET Core 8 API, SignalR hub, background scheduler.
- `LiveStockBL/` – Business logic and data models.
- `InventoryApp-frontend/` – React + TypeScript UI (bootstrapped with Create React App).

---

## Prerequisites

To build and run using containers you need:

- **Docker** (Docker Desktop on Windows)
- **Git** (to clone the repository)

For local, non-container development you additionally need:

- **.NET SDK 8.0+**
- **Node.js 18+** and **npm**

---

## Running with Docker Compose

All container configuration (ports, images, environment variables, etc.) is defined in the `Dockerfile`(s) and the `docker-compose.yml` file, so you usually **only need a single command**.

From the folder that contains your `docker-compose.yml`:

```bash
docker compose up -d --build
```

This will:

- Build the backend and frontend images using their Dockerfiles.
- Start all services defined in `docker-compose.yml` in the background.

To stop everything:

```bash
docker compose down
```

After `docker compose up -d --build` completes, open the frontend URL defined in your `docker-compose.yml` (commonly `http://localhost:3000`) and make sure the API base URL in the frontend matches the backend service URL from the compose file.

---

## Local development (without Docker)

- **Backend**: Open `InventoryApp.sln` in Visual Studio or run from CLI:

  ```bash
  dotnet run --project LiveStock/LiveStock.csproj
  ```

- **Frontend**:

  ```bash
  cd InventoryApp-frontend
  npm install
  npm start
  ```

Then open `http://localhost:3000` in your browser. Ensure the frontend API base URL points to the backend (for example `http://localhost:8080`).

# InventoryApp
InventoryApp
