using Azure;
using Azure.Communication.Email;
using Onatrix_assignment.Interface;
using System.Net.Mail;

namespace Onatrix_assignment.Services;

public class EmailService(EmailClient emailClient, IConfiguration configuration) : IEmailService
{
    private readonly EmailClient _emailClient = emailClient;
    private readonly IConfiguration _configuration = configuration;

    public async Task SendVerificationEmailAsync(string email)
    {
        var subject = "Thanks! We've received your request";
        var emailMessage = new EmailMessage(
        senderAddress: _configuration["ACS:SenderAddress"],
        content: new EmailContent(subject)
        {
            PlainText = @"Thank you! We received your request and will get back to you shortly.",
            Html = @"
            <html>
              <body style='font-family: Arial, sans-serif; background-color: #f4f4f7; padding: 40px;'>
                <table style='max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; box-shadow: 0 2px 8px rgba(0,0,0,0.05);'>
                  <tr>
                    <td style='padding: 40px; text-align: center;'>
                      <h1 style='color: #4F5955;'>Thank you!</h1>
                      <p style='font-size: 16px; color: #535656;'>
                        We’ve received your request and will get back to you shortly.
                      </p>
                      <div style='margin-top: 30px;'>
                        <a href='https://umbracocms-johanna-johansson.azurewebsites.net' 
                           style='display: inline-block; padding: 12px 24px; background-color: #4F5955; color: #F2EDDC; text-decoration: none; border-radius: 4px; font-weight: bold;'>
                          Visit our website
                        </a>
                      </div>
                      <hr style='margin: 40px 0; border: none; border-top: 1px solid #eeeeee;' />
                      <p style='font-size: 12px; color: #999999;'>
                        © 2025 Onatrix. All rights reserved.
                      </p>
                    </td>
                  </tr>
                </table>
              </body>
            </html>"
        },
        recipients: new EmailRecipients(new List<EmailAddress>
        {
            new EmailAddress($"{email}")
        }));

        var emailSendOperation = await _emailClient.SendAsync(WaitUntil.Completed, emailMessage);
    }
}

