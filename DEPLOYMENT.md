# Deploying Tashtip to MonsterASP.NET

MonsterASP.NET is a Windows/IIS host built for ASP.NET Core, so this deploys the same way any ASP.NET Core 7 MVC app would: a site + a SQL Server database from their panel, then either **Web Deploy** from Visual Studio or **FTP** of a self-contained publish output.

## 1. Create the site and database

1. In the MonsterASP.NET control panel, create a new **ASP.NET Core** hosting plan/site.
2. In the same panel, create a **SQL Server** database. Note the connection string it gives you (server, database name, SQL login + password) — it will look like:
   ```
   Server=SQLxxxx.domain.com;Database=db_xxxx;User Id=xxxx;Password=xxxx;TrustServerCertificate=True;
   ```
3. In the site's settings, confirm which **.NET runtime versions** are enabled for the app pool. This app targets **.NET 7** — MonsterASP.NET's panel lets you pick the runtime per site, so make sure .NET 7 (or an LTS you've migrated to) is available before publishing. .NET 7 itself is out of support upstream; it was kept here deliberately because the PDF-report library (`AspNetCore.Reporting` 2.1.0) isn't verified on newer target frameworks — see the note in the README's Roadmap if you want to revisit that.

## 2. Point the app at the production database

Don't commit production secrets. Use one of:

- **Environment variable** on the site (most hosts expose this in the panel): `ConnectionStrings__TASHTIPConnection` = the connection string from step 1. ASP.NET Core's configuration system picks this up automatically over `appsettings.json`.
- Or an `appsettings.Production.json` **on the server only** (not in git) with the real connection string — `ASPNETCORE_ENVIRONMENT=Production` on the site will load it.

## 3. Apply migrations to the production database

From your machine, pointed at the production connection string (temporarily, e.g. via an environment variable or a throwaway `appsettings.Production.json`):

```bash
dotnet tool restore
dotnet tool run dotnet-ef database update ^
  --project TASHTIP.InfraDB2/TASHTIP.InfraDB.csproj ^
  --startup-project TASHTIP/TASHTIP.Web.csproj ^
  --context GeneralDBContext ^
  --connection "Server=SQLxxxx...;Database=db_xxxx;User Id=xxxx;Password=xxxx;TrustServerCertificate=True;"
```

This creates every table (including `AspNetUsers`/roles from Identity, once you also run it — or just start the app once against the production DB so `Program.cs`'s startup seeding creates the Admin/Customer roles and the seed admin account for you).

## 4. Publish

**Option A — Web Deploy (Visual Studio):**
1. Right-click `TASHTIP.Web` → **Publish** → **New profile** → **Web Deploy**.
2. MonsterASP.NET's panel has a "Web Deploy" / "Get Publish Profile" download — import that `.PublishSettings` file into Visual Studio's publish wizard.
3. Publish. Visual Studio builds, packages, and pushes the site directly.

**Option B — FTP:**
1. `dotnet publish TASHTIP/TASHTIP.Web.csproj -c Release -o ./publish`
2. Upload the contents of `./publish` to the site's web root via the FTP credentials from the panel.

## 5. First run checklist

- [ ] Site loads at the assigned domain, `Home/Home` renders (hero + gallery)
- [ ] `/Identity/Account/Login` works; log in with the seeded admin, **change the password immediately** via `/Identity/Account/Manage`
- [ ] `/Admin/Dashboard` loads with real (or zeroed) stats
- [ ] Submit a test request through the public site, confirm it shows in `/Admin/Requests`
- [ ] Confirm uploaded images land in and serve from `wwwroot/ImageFinshProject/Image/` (make sure that folder is writable by the app pool identity)

## Custom domain / HTTPS

MonsterASP.NET issues/manages TLS certs per site from the panel — point your domain's DNS at the host they give you, then bind the domain and enable HTTPS from their site settings before going live with real customer data.
