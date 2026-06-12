# Railway deployment (root directory: HackerRank1)

## Required environment variable

| Variable | Description |
|----------|-------------|
| `Supabase__ConnectionString` | Full Npgsql connection string to Supabase/PostgreSQL |

Example (replace credentials):

```
Host=aws-1-us-east-2.pooler.supabase.com;Port=5432;Database=postgres;Username='postgres.PROJECT_REF';Password='YOUR_PASSWORD';SSL Mode=Require;Trust Server Certificate=true
```

Alternative: `DATABASE_URL` (postgresql://...) is also supported.

## Railway settings

- **Branch:** `Wuipy` (NOT `main`)
- **Root Directory:** `HackerRank1`
- **.NET version:** 8.0 (see `global.json` and `nixpacks.toml`)
- **Health check:** `/api/frauds`

Optional variable if build still uses .NET 6 SDK:

```
NIXPACKS_DOTNET_SDK_VERSION=8.0
```

If build fails with duplicate Npgsql or missing JwtBearer, Railway is deploying an **old commit**. Redeploy from the latest `Wuipy` push.

## Frontend (Netlify)

Set `VITE_API_URL` to your Railway public URL, e.g. `https://your-app.up.railway.app`
