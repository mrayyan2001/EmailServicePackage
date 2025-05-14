# 📧 Send Email Using Gmail SMTP in ASP.NET Core 8 — Complete Tutorial

In this guide, you’ll learn how to send emails from an ASP.NET Core 8 app using Gmail’s SMTP server.
We’ll create a reusable `EmailService` with dependency injection and Gmail App Password authentication.

---

## ✅ Prerequisites

### 1. Create a Google Account

If you don’t have a Gmail account, [create one here](https://accounts.google.com/signup).

### 2. Enable 2-Step Verification (2FA)

Google requires 2FA to use **App Passwords**.
Enable it from [here](https://support.google.com/accounts/answer/185839?hl=en).

### 3. Create an App Password

- Go to [Google App Passwords](https://support.google.com/mail/answer/185833?hl=en).
- Select **Mail** as the app and **Other (Custom name)**, e.g., "ASP.NET Core App".
- Copy the generated 16-character app password (you'll need this soon).

---

## 🚀 Step 1: Create the Email Service Interface

Create an interface to define your email sending logic.

📄 File: `Interfaces/IEmailService.cs`

```csharp
namespace YourProject.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
```

---

## 🚀 Step 2: Implement the Email Service

Now, implement the actual logic using Gmail's SMTP server.

📄 File: `Services/EmailService.cs`

```csharp
using System.Net;
using System.Net.Mail;
using YourProject.Interfaces;

namespace YourProject.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {

```

---

### Define from, appPassword, host, and port inside the method.

```csharp
            var from = "yourgmail@gmail.com"; // Replace with your Gmail
            var appPassword = "your_app_password"; // Replace with your App Password
            var host = "smtp.gmail.com";
            int port = 587; // Use 587 for TLS
```

ℹ️ **Note**:

- Port `465` requires an SSL stream, but `SmtpClient` in .NET doesn’t handle implicit SSL (port `465`) properly. It expects to start with plain communication and then upgrade to TLS (using port `587`).
- Use port `25` for unencrypted communication.
- Use port `587` for TLS (Recommended).
- Use port `465` for SSL.

---

### Create SmtpClient object and set the properties for sending email.

```csharp
            using var client = new SmtpClient(host, port)
            {
                EnableSsl = true, // Enables SSL encryption.
                Credentials = new NetworkCredential(from, appPassword)
            };
```

---

### Send email using SmtpClient object.

```csharp
            var mailMessage = new MailMessage(from, to, subject, body);
            await client.SendMailAsync(mailMessage);
        }
    }
}
```

> ℹ️ **Note**: Do NOT hardcode sensitive data in real apps. Use **appsettings.json** or **secrets.json** instead.

---

## 🚀 Step 3: Register Email Service in DI (Program.cs)

Tell .NET Core to inject `EmailService` wherever `IEmailService` is needed.

📄 File: `Program.cs`

```csharp
using YourProject.Interfaces;
using YourProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Register Email Service
builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();

app.MapControllers();

app.Run();
```

> ✅ **Transient** lifetime creates a new instance every time it’s requested — perfect for lightweight, stateless services like this.

---

## 🚀 Step 4: Use Email Service in a Controller

Let’s send an email from an API endpoint!

📄 File: `Controllers/EmailController.cs`

```csharp
using Microsoft.AspNetCore.Mvc;
using YourProject.Interfaces;

namespace YourProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
```

---

```csharp

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(string to, string subject, string body)
        {
            await _emailService.SendEmailAsync(to, subject, body);
            return Ok("Email Sent Successfully!");
        }
    }
}
```

Now, run your project and test the API using **Postman** or **Swagger** by sending a POST request like this:

```
POST /email/send
?to=recipient@example.com
&subject=Hello
&body=This is a test email
```

---

## ⚠️ Important Notes

- 🔐 **Never expose** your app password in public code.
- ✅ Use **appsettings.json** for configurations.
- 📧 Gmail SMTP settings:

  - Host: `smtp.gmail.com`
  - Port: `587` (TLS) or `465` (SSL)

---

## ✅ Optional Bonus: Move Config to appsettings.json

### In `appsettings.json`

```json
"EmailSettings": {
  "From": "yourgmail@gmail.com",
  "AppPassword": "your_app_password",
  "Host": "smtp.gmail.com",
  "Port": 587
}
```

Then in `EmailService`, read from config instead of hardcoding 👇
(Ask me if you want me to show you this upgraded version — it’s best practice!)

---
