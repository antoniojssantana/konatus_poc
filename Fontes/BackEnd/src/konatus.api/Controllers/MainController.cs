using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using konatus.business.Notifications;

namespace konatus.api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        private readonly HttpContext _context;

        protected MainController(INotifier notifier, IHttpContextAccessor contextAccessor)
        {
            _notifier = notifier;
            _context = contextAccessor.HttpContext;
        }

        protected ActionResult CustomResponse(object result = null)
        {
            _context.Response.Headers.Add("Front-OriginFront-Origin", "Konatus");
            if (this.IsValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    errors = _notifier.GetNotifications().Select(n => n.Message)
                });
            }
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifierErrorInvalidModel(modelState);
            return CustomResponse();
        }

        protected void NotifierErrorInvalidModel(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                ErrorNotifier(errorMessage);
            }
        }

        protected void ErrorNotifier(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected bool IsValid()
        {
            return !_notifier.HasNotification();
        }
    }
}