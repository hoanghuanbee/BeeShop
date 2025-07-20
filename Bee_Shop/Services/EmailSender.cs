using Bee_Shop.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
namespace Bee_Shop.Services
{
    public class EmailSender
    {
        private readonly EmailSettings _settings;
        private readonly IConfiguration _config;
        public EmailSender(IOptions<EmailSettings> options, IConfiguration config)
        {
            _settings = options.Value;
            _config = config;
        }


        public async Task SendConfirmationEmail(string appUrl, string toEmail, string token)
        {
            Console.WriteLine("📨 [SEND START] Gửi email tới: " + toEmail);

            try
            {
                var confirmUrl = $"{appUrl}/Account/Confirm?token={token}";

                var body = $@"
                    <table style='max-width: 600px; margin: auto; font-family: Arial, sans-serif; border: 1px solid #e0e0e0;'>
                        <tr style='background-color: #fffbee;'>
                            <td style='padding: 20px; text-align: center;'>
                                <img src='https://yourdomain.com/images/beeshop-logo.png' alt='Bee Shop' style='max-width: 150px; height: auto;' />
                            </td>
                        </tr>
                        <tr>
                            <td style='padding: 30px; background-color: #fffaf0; color: #333;'>
                                <h2 style='color: #f0ad00;'>🐝 Xác nhận tài khoản của bạn</h2>
                                <p>Chào bạn,</p>
                                <p>Cảm ơn bạn đã đăng ký tài khoản tại <strong style='color:#f0ad00;'>Bee Shop</strong>.</p>
                                <p>Vui lòng nhấn vào nút dưới đây để xác nhận tài khoản của bạn:</p>
                                <p style='text-align: center; margin: 30px 0;'>
                                    <a href='{confirmUrl}' style='background-color: #ffc107; color: black; padding: 12px 25px; text-decoration: none; border-radius: 5px; font-weight: bold;'>🐝 Kích hoạt tài khoản</a>
                                </p>
                                <p>Nếu bạn không yêu cầu đăng ký, vui lòng bỏ qua email này.</p>
                                <p>Thân mến,<br/>Đội ngũ <strong>Bee Shop</strong> 🐝</p>
                            </td>
                        </tr>
                        <tr style='background-color: #f7f7f7;'>
                            <td style='padding: 15px; text-align: center; font-size: 12px; color: #888;'>
                                © 2025 Bee Shop. All rights reserved.
                            </td>
                        </tr>
                    </table>";

                var mail = new MailMessage
                {
                    From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                    Subject = "Xác nhận tài khoản Bee Shop",
                    Body = body,
                    IsBodyHtml = true
                };
                mail.To.Add(toEmail);

                using var smtp = new SmtpClient(_settings.SmtpServer, _settings.Port)
                {
                    Credentials = new NetworkCredential(_settings.SenderEmail, _settings.SenderPassword),
                    EnableSsl = _settings.UseSsl
                };

                await smtp.SendMailAsync(mail);
                Console.WriteLine("✅ [SEND DONE] Gửi email thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ [SEND ERROR] " + ex.Message);
                throw;
            }
        }
    }
}
        

