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

**For using SMTP**

```json
"SmtpSettings": {
  "From": "your-email@example.com",
  "AppPassword": "your-app-password",
  "Host": "smtp.example.com",
  "Port": 587
}
```

**For using SendGrid**

```json
"SendGridSettings": {
    "ApiKey": "your-sendgrid-api-key",
    "From": "your-email",
    "FromName": "your-name",
  }
```

## Usage

### 1. Register the EmailService

In your `Program.cs` or `Startup.cs`, register the service:

```csharp
builder.Services.AddSmtpService(builder.Configuration); // For SMTP
// or
builder.Services.AddSendGridEmailService(builder.Configuration); // For SendGrid
```

### 2. Inject and Use IEmailService

In your classes or controllers, inject `ISmtpService` or `ISendGridService` and use it to send emails:

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

    public async Task SendNotification(string to, string subject, string body)
    {
        var emailDto = new SendEmailDto
        {
            To = to,
            Subject = subject,
            Body = body
        };

        // For SMTP
        await _smtpService.SendEmailAsync(emailDto);

        // For SendGrid
        await _sendGridService.SendEmailAsync(emailDto);
    }
}
```

## API Reference

### `SendEmailDto`

| Property  | Type   | Description             |
| --------- | ------ | ----------------------- |
| `To`      | string | Recipient email address |
| `Subject` | string | Email subject           |
| `Body`    | string | Email body content      |

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

## Authors

- Mohammad
- Heba

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Repository

For more information, visit the [GitHub repository](https://github.com/mrayyan2001/EmailServicePackage).
