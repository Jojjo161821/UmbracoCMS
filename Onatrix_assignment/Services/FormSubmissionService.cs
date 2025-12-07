using Onatrix_assignment.Interface;
using Onatrix_assignment.ViewModels;
using System.Diagnostics;
using Umbraco.Cms.Core.Services;

namespace Onatrix_assignment.Services;

public class FormSubmissionService(IContentService contentService, IEmailService emailService)
{
    private readonly IContentService _contentService = contentService;
    private readonly IEmailService _emailService = emailService;

    public bool SaveCallbackRequest(CallbackFormViewModel model)
    {
        try
        {
            var container = _contentService.GetRootContent().FirstOrDefault(x => x.ContentType.Alias == "formSubmissions");
            if (container == null)
                return false;

            var requestName = $"{DateTime.Now:yyyy-MM-dd HH:mm} - {model.Name}";
            var request = _contentService.Create(requestName, container, "callbackRequest");

            request.SetValue("callbackRequestName", model.Name);
            request.SetValue("callbackRequestEmail", model.Email);
            request.SetValue("callbackRequestPhone", model.Phone);
            request.SetValue("callbackRequestOption", model.SelectedOption);

            var saveResult = _contentService.Save(request);

            if (saveResult.Success)
            {
                _emailService.SendVerificationEmailAsync(model.Email);
            } 

            return saveResult.Success;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public bool SaveQuestionRequest(QuestionFormViewModel model)
    {
        try
        {
            var container = _contentService.GetRootContent().FirstOrDefault(x => x.ContentType.Alias == "formSubmissions");
            if (container == null)
                return false;

            var requestName = $"{DateTime.Now:yyyy-MM-dd HH:mm} - {model.Name}";
            var request = _contentService.Create(requestName, container, "questionRequest");

            request.SetValue("questionFormName", model.Name);
            request.SetValue("questionFormEmail", model.Email);
            request.SetValue("questionFormQuestion", model.Question);

            var saveResult = _contentService.Save(request);

            if (saveResult.Success)
            {
                _emailService.SendVerificationEmailAsync(model.Email);
            }

            return saveResult.Success;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }
}
