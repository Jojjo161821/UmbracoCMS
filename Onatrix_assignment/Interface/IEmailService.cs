namespace Onatrix_assignment.Interface
{
    public interface IEmailService
    {
        Task SendVerificationEmailAsync(string email);
    }
}