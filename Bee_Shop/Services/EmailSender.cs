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
            <p>Chào bạn,</p>
            <p>Bạn đã đăng ký tài khoản tại Bee Shop.</p>
            <p>Nhấn vào liên kết sau để xác nhận tài khoản:</p>
            <p><a href='{confirmUrl}'>Kích hoạt tài khoản</a></p>
            <p>Cảm ơn bạn!</p>";

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
        

