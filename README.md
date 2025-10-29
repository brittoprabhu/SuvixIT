# Suvix IT Site

This bundle includes a ready-to-host static website:

- `index.html` — Homepage linking to products and pricing
- `stoxedge.html` — Product page for StoxEdge (stock/day-trade insights)
- `flowcrm.html` — Product page for FlowCRM (CRM for teams)

## Hosting
Upload all files to any static host (GitHub Pages, Netlify, Vercel, S3/CloudFront, etc.).
Tailwind is loaded via CDN.

## Contact Form
The contact form currently shows a JS alert. Replace it with your backend or a service like Formspree.
Example:
<form method="POST" action="https://formspree.io/f/yourid">...</form>

## Customization
- Update pricing in `index.html` as needed.
- Replace Terms/Privacy links.
- Add your domain, favicon, and analytics.

## Backend API
An ASP.NET Core Web API is available in the `SuvixIT.Api` project. It stores newsletter subscriptions, demo requests, and email configuration using PostgreSQL.

### Prerequisites
- .NET 7 SDK
- PostgreSQL database (update `ConnectionStrings:DefaultConnection` in `SuvixIT.Api/appsettings.json`).

### Database setup
Apply Entity Framework Core migrations (create one if this is a new database) or run `dotnet ef database update` after generating migrations.

### Running the API
```
dotnet restore SuvixIT.Api/SuvixIT.Api.csproj
dotnet run --project SuvixIT.Api/SuvixIT.Api.csproj
```
Swagger UI will be available at `https://localhost:7189/swagger` in development.

### Key endpoints
- `POST /api/subscriptions` — Persist a new subscription.
- `POST /api/demorequests` — Create a demo request and send an email using the stored configuration.
- `GET/PUT /api/admin/email-configuration` — Manage SMTP settings for outbound notifications.
