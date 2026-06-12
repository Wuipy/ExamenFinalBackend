# Railway deployment (root directory: HackerRank1)

## Settings

- **Branch:** `Wuipy`
- **Root Directory:** `HackerRank1`
- **Build:** `Dockerfile` (official .NET 8 image, no Nixpacks)
- **Health check:** `/api/frauds`

## Required environment variable

```
Supabase__ConnectionString=Host=...;Port=5432;Database=postgres;Username='...';Password='...';SSL Mode=Require;Trust Server Certificate=true
```

Alternative: `DATABASE_URL` (postgresql://...) is also supported.

## Frontend (Netlify)

```
VITE_API_URL=https://your-app.up.railway.app
```
