# Claude Code Proxy

A reverse proxy server built with ASP.NET Core and YARP (Yet Another Reverse Proxy) that forwards requests to the Anthropic API with request/response logging capabilities.

## Features

- **Reverse Proxy**: Forwards all incoming requests to `https://api.anthropic.com/`
- **Request/Response Logging**: Logs HTTP method, request content, and response content to console for debugging
- **Built with YARP**: Leverages Microsoft's high-performance reverse proxy library
- **ASP.NET Core 8.0**: Modern web framework with built-in performance optimizations

## Prerequisites

- .NET 8.0 SDK or later
- Visual Studio 2022, Rider, or VS Code (optional)

## Getting Started

### Build and Run

```bash
cd ccp
dotnet build
dotnet run
```

The proxy will start on the default ASP.NET Core ports (typically `http://localhost:5000` and `https://localhost:5001`).

### Configuration

The proxy configuration is defined in `appsettings.json`:

```json
{
  "ReverseProxy": {
    "Routes": {
      "route1": {
        "ClusterId": "cluster1",
        "Match": {
          "Path": "{**catch-all}"
        }
      }
    },
    "Clusters": {
      "cluster1": {
        "Destinations": {
          "destination1": {
            "Address": "https://api.anthropic.com/"
          }
        }
      }
    }
  }
}
```

## Usage

Once running, send requests to the proxy server and they will be forwarded to the Anthropic API. All requests and responses will be logged to the console for monitoring and debugging purposes.

Example:
```bash
ANTHROPIC_BASE_URL="http://localhost:2311" claude
```
Make sure the address matches the output of the proxy. 

## Project Structure

- `Program.cs` - Main application entry point with YARP configuration
- `ccp.csproj` - Project file with dependencies
- `appsettings.json` - Application configuration including proxy routes

## Dependencies

- **Yarp.ReverseProxy** (2.3.0) - Microsoft's reverse proxy library
- **Microsoft.AspNetCore.Diagnostics.Middleware** (9.9.0) - Diagnostic middleware
- **Microsoft.Extensions.Compliance.Redaction** (9.9.0) - Data redaction capabilities

## Development

The application includes request and response transform middleware that logs:
- HTTP method and request details
- Request content (if present)
- Response details and content

This logging can be helpful for debugging API interactions and monitoring traffic flow.

# License

See [LICENSE.md]
