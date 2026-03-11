## Overview

This solution is a **live stock dashboard** with:

- **Backend**: ASP.NET Core 8 API + SignalR
- **Frontend**: React + TypeScript + AG Grid (AG Never implemented before mostly google developed by help)
- **Fault injection**: 10% server‑side failure rate
- **Realtime updates**: SignalR hub broadcasting stock prices
- **Docker**: Backend Dockerfile

## Backend (ASP.NET Core 8)

- Projects:
  - `LiveStock` – Web API + SignalR host.
  - `LiveStockBL` – business logic (DTOs, hubs, services, middleware).
- Stock API:
  - `IStockService` / `StockService` keep an in‑memory `List<Stock>`.
  - `StockController` exposes endpoints to read stock data.
  - `IStockService` is registered as a singleton so API and background service share the same data.
- 10% failure middleware:
  - `FailureMiddleWare` randomly returns HTTP 500 on ~10% of requests, otherwise calls the next middleware.
- Exception middleware:
  - `ExceptionHandlingMiddleware` logs unhandled exceptions and returns a JSON error with HTTP 500.
- SignalR + scheduler:
  - `StockHub` pushes updates to clients on `ReceiveStocks`.
  - `StockUpdateScheduler` (`BackgroundService`) calls `UpdatePrices()` every 2 seconds and broadcasts via `IHubContext<StockHub>`.
  - Registered with `AddSignalR`, `AddHostedService<StockUpdateScheduler>` and `app.MapHub<StockHub>("/stockhub")`.
- CORS:
  - Named policy `FrontEndCors` allows `http://localhost:3000` with credentials, applied via `app.UseCors("FrontEndCors")`.

## Frontend (React + TypeScript)

- Structure:
  - `src/Api/Signalr.ts` – singleton `HubConnection` to `/stockhub` with automatic reconnect.
  - `src/Api/CallApi.ts` – Axios wrapper for REST API (e.g. `getStocks`).
  - `src/Components/StockGrid.tsx` – AG Grid displaying stocks and wiring SignalR updates.
  - `src/Types/Stock.ts` – TypeScript interface matching backend DTO.
- AG Grid:
  - Uses `AgGridReact` with columns `id`, `symbol`, `price`.
  - Registers community module using `ModuleRegistry.registerModules([AllCommunityModule])`.
- SignalR client:
  - On first render, loads initial stocks via REST and starts the connection only if it is `Disconnected`.
  - Subscribes to `ReceiveStocks` and replaces local state with the latest list.

## Running Locally

- **Backend**
  - `cd LiveStock`
  - `dotnet run`
  - API + SignalR available at the configured Kestrel port (hub at `/stockhub`).
- **Frontend**
  - `cd InventoryApp-frontend`
  - `npm install`
  - `npm start`
  - Runs on `http://localhost:3000`, pointing to the backend.

## Docker / docker-compose

- Backend Dockerfile: `LiveStock/Dockerfile` (ASP.NET 8 base image + SDK build stage).
- Build and run backend only with the Docker CLI:

```bash
cd <repo-root>
docker build -t livestock-backend -f LiveStock/Dockerfile .
docker run -p 8080:8080 livestock-backend
```

- Or run from Visual Studio:
  - Select the **Docker** launch profile for the `LiveStock` project.
  - Press **F5**  Visual Studio builds the image, creates a container, and maps container port `8080` to a random host port (shown in the Containers window).
  - Use that host port (for example `http://localhost:32792`) from the frontend for API and SignalR calls.

- Containerize **backend + frontend together** using `docker-compose.yml`:

```bash
cd <repo-root>
docker compose up --build```

  - Backend runs as service `backend` on `http://localhost:8080`.
  - Frontend runs as service `frontend` on `http://localhost:3000` and talks to the backend using `REACT_APP_API_URL` (set to `http://localhost:8080` in `docker-compose.yml`).

## Key Architectural Decisions

- **Separation of concerns**: API host, business logic, and (future) data access are split into projects for easier testing and evolution.
- **In‑memory state + background service**: Keeps the exercise simple while still showing a realistic pattern for live updates with SignalR.
- **Failure via middleware**: 10% failure is injected in one place, so controllers/services stay clean and behavior is easy to tune or disable.
- **React + AG Grid**: Uses a mature grid for performance and UX, letting the code focus on real‑time behavior instead of table plumbing.
- **Docker‑ready backend**: Backend can be containerized and plugged into a future `docker-compose` setup with minimal extra work.

