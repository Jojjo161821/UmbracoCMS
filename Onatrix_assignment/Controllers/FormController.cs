using Microsoft.AspNetCore.Mvc;
using Onatrix_assignment.Services;
using Onatrix_assignment.ViewModels;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Onatrix_assignment.Controllers
{
    public class FormController : SurfaceController
    {
        public FormController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, FormSubmissionService formService) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _formService = formService;
        }

        private readonly FormSubmissionService _formService;

        public IActionResult HandleCallbackForm(CallbackFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var result = _formService.SaveCallbackRequest(model);
            if (!result)
            {
                TempData["FormError"] = "An error occurred while processing your request. Please try again later.";
                return RedirectToCurrentUmbracoPage();
            }

            TempData["FormSuccess"] = "Your request has been submitted successfully. We’ll get back to you shortly.";
            return RedirectToCurrentUmbracoPage();
        }

        public IActionResult HandleQuestionForm(QuestionFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            var result = _formService.SaveQuestionRequest(model);
            if (!result)
            {
                TempData["FormError"] = "An error occurred while processing your request. Please try again later.";
                return RedirectToCurrentUmbracoPage();
            }

            TempData["FormSuccess"] = "Your question has been submitted successfully. We’ll get back to you shortly.";
            return RedirectToCurrentUmbracoPage();
        }
    }
}
