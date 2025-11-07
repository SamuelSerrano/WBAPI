# WBAPI

API Creada por Samuel Serrano para la prueba Técnica.

## Ramas
- `main`
- `dev-WBAPI`
- `qa-WBAPI`

## Ejecutar local
1. `dotnet restore`
2. `dotnet run --project WBAPI.Api`
3. API: `http://localhost:5276`

## Flujo de pruebas (Postman)
Importar `postman_collection_WBAPI.json`
1. POST `/api/auth/login` (user: admin / pass: password) -> token
2. Usar token en Authorization Bearer para CRUD en `/api/products`

## Notas
- Cambiar clave JWT en `appsettings.json` por variable segura.
