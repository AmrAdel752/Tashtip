# Tashtip (تشطيب)

**Tashtip** is a full-stack apartment-finishing platform: a public marketing site where clients browse finished units in **2D photos, 3D models, and VR 360° tours**, request a quote, get a cost estimate, book a site visit — and a real back office for the team to run the whole pipeline from request to delivery.

Originally built as a graduation project, this repo has been carried forward into a product-shaped codebase: real authentication with Admin/Customer roles, two full dashboards, a request-status workflow with an audit trail, and a wider feature set aimed at actually being demoed to and sold to clients.

## Features

**Public site**
- Services, portfolio gallery (filterable), pricing, team, FAQ, contact
- Real customer reviews (moderated before they go public)
- Cost estimator (service tier × area × city → an instant price range)
- WhatsApp quick-contact button

**Unit viewer** — three ways to see a unit before booking:
- **2D** — photo gallery with lightbox
- **3D** — interactive `<model-viewer>` GLTF model, rotate/zoom/AR
- **VR 360°** — drag-to-look-around equirectangular panorama (Photo Sphere Viewer)

Every unit ships with a demo model/panorama out of the box; admins attach real media per unit from the back office with no code changes.

**Customer account**
- Register → auto-provisioned as a Customer
- **My Requests** — every booking request with a live status timeline
- **My Appointments** — book/track a site-visit with an engineer
- In-app notifications (header bell) when a request's status changes
- Leave a review once a request is completed

**Admin back office** (role- and claim-gated, `/Admin/*`)
- Dashboard — live stat cards + Chart.js charts (status breakdown, requests over the last 14 days)
- Requests — filter by status, inline status change (writes to the audit trail + notifies the customer), print-to-PDF
- Gallery — manage units, attach real 2D photos / 3D models / 360° panoramas
- Reviews — approve/reject queue
- Appointments — track and update site-visit bookings
- Users & roles — grant/revoke Admin access

## Tech stack

- **ASP.NET Core 7 MVC** + Razor Pages (Identity UI) on .NET 7
- **EF Core 7** / SQL Server
- **ASP.NET Core Identity** (cookie auth, role + claim-based authorization)
- Bootstrap 5, Chart.js, DataTables, GLightbox, SweetAlert2, `<model-viewer>`, Photo Sphere Viewer
- **AspNetCore.Reporting** for PDF request print-outs (RDLC)

## Architecture

Four-project "clean architecture" split (`Document.txt` has the original intent):

| Project | Role |
|---|---|
| `TASHTIP.Web` | Presentation - controllers, views, Identity area |
| `TASHTIP.EF` | Entities, view models, Identity/roles/permissions policy |
| `TASHTIP.InfraDB` | EF Core `DbContext`s + migrations |
| `TASHTIP.RepoUOWCore` | Business logic services (requests, gallery, reviews, appointments, notifications, dashboard stats) |
| `TASHTIP.Report` | RDLC report dataset |

## Local setup

1. **Clone & open** `TASHTIP.sln` (Visual Studio 2022 or `dotnet` CLI, .NET 7 SDK).
2. **Database**: point `TASHTIP/appsettings.json` → `ConnectionStrings:TASHTIPConnection` at your SQL Server instance, then apply migrations:
   ```bash
   dotnet tool restore
   dotnet tool run dotnet-ef database update --project TASHTIP.InfraDB2/TASHTIP.InfraDB.csproj --startup-project TASHTIP/TASHTIP.Web.csproj --context GeneralDBContext
   ```
   (A local `dotnet-tools.json` pins `dotnet-ef 7.0.5` — the globally installed newer tool version can't load this project's net7.0 design-time assemblies.)
3. **Run**:
   ```bash
   dotnet run --project TASHTIP/TASHTIP.Web.csproj
   ```
   On first run the app seeds the `Admin`/`Customer` roles (with their permission claims) and one admin account:
   - **Username:** `admin`
   - **Password:** `Tashtip@Admin1`

   ⚠️ Change this password immediately (`/Identity/Account/Manage`) on any environment beyond your own machine.
4. Register a second account through `/Identity/Account/Register` to see the Customer side.

## Roadmap

- Replace demo 3D/VR media with real per-unit captures
- Payment/deposit collection on approved quotes
- Engineer-facing schedule view for appointments
- Email notifications alongside in-app ones
- Move remaining direct `DbContext` calls in `HomeController` into `TASHTIP.RepoUOWCore` services

## Deployment

See [DEPLOYMENT.md](DEPLOYMENT.md) for MonsterASP.NET-specific steps.
