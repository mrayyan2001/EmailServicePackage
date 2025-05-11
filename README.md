# EmailService

A lightweight .NET 8 library for sending emails via SMTP, designed for seamless integration with ASP.NET Core applications.

## Features

- Simple and intuitive API for sending emails
- Supports SMTP configuration via `appsettings.json`
- Built-in dependency injection support
- Clean separation of concerns using `IEmailService` interface
- Fully configurable via `EmailSettings`

## Installation

Install the package via NuGet Package Manager:

```bash
dotnet add package MH.EmailService
```

## Configuration

Add the following section to your `appsettings.json`:

```json
"EmailSettings": {
  "from": "your-email@example.com",
  "appPassword": "your-app-password",
  "host": "smtp.example.com",
  "port": 587
}
```

## Usage

### 1. Register the EmailService

In your `Program.cs` or `Startup.cs`, register the service:

```csharp
builder.Services.AddEmailService(builder.Configuration);
```

### 2. Inject and Use IEmailService

In your classes or controllers, inject `IEmailService` and use it to send emails:

```csharp
public class NotificationService
{
    private readonly IEmailService _emailService;

    public NotificationService(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task NotifyUserAsync()
    {
        var email = new SendEmailDto
        {
            to = "recipient@example.com",
            subject = "Welcome!",
            body = "Thank you for signing up."
        };

        await _emailService.SendEmailAsync(email);
    }
}
```

## API Reference

### `SendEmailDto`

| Property  | Type   | Description             |
| --------- | ------ | ----------------------- |
| `to`      | string | Recipient email address |
| `subject` | string | Email subject           |
| `body`    | string | Email body content      |

### `EmailSettings`

| Property      | Type   | Description                  |
| ------------- | ------ | ---------------------------- |
| `from`        | string | Sender email address         |
| `appPassword` | string | SMTP app password or token   |
| `host`        | string | SMTP server host             |
| `port`        | int    | SMTP server port (e.g., 587) |

## Authors

- Mohammad
- Heba

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Repository

For more information, visit the [GitHub repository](https://github.com/mrayyan2001/EmailServicePackage).
