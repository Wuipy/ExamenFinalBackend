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

- **Branch:** `Wuipy`
- **Root Directory:** `HackerRank1`
- **Health check:** `/api/frauds`

## Frontend (Netlify)

Set `VITE_API_URL` to your Railway public URL, e.g. `https://your-app.up.railway.app`
