# Environment Variables for Render.com Deployment

When deploying your ASP.NET Core Web API to Render.com, you should configure the following environment variables in the Render dashboard. These variables will override settings in your appsettings.Production.json file.

## Required Environment Variables

| Variable Name | Example Value | Description |
|---------------|--------------|-------------|
| `ASPNETCORE_ENVIRONMENT` | `Production` | Sets the .NET application environment to Production |
| `CORS__ALLOWEDORIGINS__0` | `https://your-frontend-domain.com` | The primary domain for your frontend application |
| `CORS__ALLOWEDORIGINS__1` | `https://admin.your-frontend-domain.com` | Any additional domains that need to access your API |

## Database Connection (if your API uses a database)

| Variable Name | Example Value | Description |
|---------------|--------------|-------------|
| `ConnectionStrings__DefaultConnection` | `Data Source=your-db-server;Initial Catalog=InternsDb;User ID=dbuser;Password=your-secure-password` | Database connection string |
| `DB_USER` | `dbuser` | Database username (alternative to including in connection string) |
| `DB_PASSWORD` | `your-secure-password` | Database password (alternative to including in connection string) |

## API Configuration

| Variable Name | Example Value | Description |
|---------------|--------------|-------------|
| `ApiSettings__BaseUrl` | `https://your-api-name.onrender.com` | The base URL of your deployed API (will be provided by Render) |
| `ApiSettings__UseHttps` | `true` | Enable HTTPS on your API (recommended) |

## Security Settings (Optional but Recommended)

| Variable Name | Example Value | Description |
|---------------|--------------|-------------|
| `SecurityHeaders__ContentSecurityPolicy` | `default-src 'self'; script-src 'self'; style-src 'self';` | Content Security Policy header |
| `JWT_SECRET` | `your-strong-secret-key` | If your API uses JWT authentication |

## System Configuration (Set by Render)

These variables are automatically set by Render.com, but are available to your application:

| Variable Name | Description |
|---------------|-------------|
| `PORT` | The port your application should listen on (already used in render.yaml) |
| `RENDER` | Set to `true` in Render environments |
| `RENDER_SERVICE_ID` | Unique identifier for your service |
| `RENDER_EXTERNAL_URL` | The URL of your deployed service |

## How to Set Environment Variables in Render

1. Go to your Render dashboard
2. Select your web service
3. Go to the "Environment" tab
4. Add each key-value pair in the "Environment Variables" section
5. Click "Save Changes" to apply the new variables

Note: For sensitive values like passwords and API keys, use Render's "Secret Files" feature or environment variables rather than putting them directly in your code or configuration files.

