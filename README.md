# EmailService

A lightweight .NET 8 library for sending emails via **SMTP** or **SendGrid**, designed for seamless integration with ASP.NET Core applications.

## Features

- Supports **SMTP** and **SendGrid** email providers
- Simple and intuitive API for sending emails
- Supports configuration via `appsettings.json`
- Built-in dependency injection support
- Clean separation of concerns using `ISmtpService` and `ISendGridService`
- Shared DTO structure (`SendEmailDto`) for both providers
- Optional recipient name support (`ToName`)

## Installation

Install the package via NuGet Package Manager:

```bash
dotnet add package MH.EmailService
```

## Configuration

Add one or both of the following sections to your `appsettings.json`:

**SMTP:**

```json
"SmtpSettings": {
  "From": "your-email@example.com",
  "AppPassword": "your-app-password",
  "Host": "smtp.example.com",
  "Port": 587
}
```

**SendGrid:**

```json
"SendGridSettings": {
  "ApiKey": "your-sendgrid-api-key",
  "From": "your-email@example.com",
  "FromName": "Your Name"
}
```

## Usage

### 1. Register the Email Services

In `Program.cs`:

```csharp
builder.Services.AddSmtpService(builder.Configuration);      // For SMTP
builder.Services.AddSendGridService(builder.Configuration);  // For SendGrid
```

### 2. Inject and Use Email Services

```csharp
public class NotificationService
{
    private readonly ISmtpService _smtpService;
    private readonly ISendGridService _sendGridService;

    public NotificationService(ISmtpService smtpService, ISendGridService sendGridService)
    {
        _smtpService = smtpService;
        _sendGridService = sendGridService;
    }

    public async Task SendNotificationAsync(string to, string subject, string body, string? toName = null)
    {
        var email = new SendEmailDto
        {
            To = to,
            ToName = toName,
            Subject = subject,
            Body = body
        };

        // Use SMTP
        await _smtpService.SendEmailAsync(email);

        // Or use SendGrid
        await _sendGridService.SendEmailAsync(email);
    }
}
```

## API Reference

### `SendEmailDto`

| Property  | Type   | Description                     |
| --------- | ------ | ------------------------------- |
| `To`      | string | Recipient email address         |
| `ToName`  | string | (Optional) Recipient name       |
| `Subject` | string | Email subject                   |
| `Body`    | string | Email body content (plain/html) |

### `SmtpSettings`

| Property      | Type   | Description                  |
| ------------- | ------ | ---------------------------- |
| `From`        | string | Sender email address         |
| `AppPassword` | string | SMTP app password or token   |
| `Host`        | string | SMTP server host             |
| `Port`        | int    | SMTP server port (e.g., 587) |

### `SendGridSettings`

| Property   | Type   | Description          |
| ---------- | ------ | -------------------- |
| `ApiKey`   | string | SendGrid API key     |
| `From`     | string | Sender email address |
| `FromName` | string | Sender name          |

## Documentation

- 📄 [SMTP Setup Guide](docs/SMTP.md)
- 📄 [SendGrid Setup Guide](docs/SendGrid.md)

## Authors

- Mohammad
- Heba

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Repository

[GitHub Repository](https://github.com/mrayyan2001/EmailServicePackage)
