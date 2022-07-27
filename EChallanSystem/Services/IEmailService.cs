using EChallanSystem.DTO;

namespace EChallanSystem.Services
{
    public interface IEmailService
    {
        void SendMail(EmailDTO request);
    }
}
