using System;
using System.Net;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace E_PropertyCMS.Api.App_Start
{
	public class ValidateRequest : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext _Context)
        {
            if (_Context != null && _Context.Request != null && !_Context.Request.RequestUri.Scheme.Equals(Uri.UriSchemeHttps))
            {
                var controllerFilters = _Context.ControllerContext.ControllerDescriptor.GetFilters();
                var actionFilters = _Context.ActionDescriptor.GetFilters();

                if ((controllerFilters != null && controllerFilters.Select
                (t => t.GetType() == typeof(ValidateRequest)).Count() > 0) ||
                    (actionFilters != null && actionFilters.Select(t =>
                    t.GetType() == typeof(ValidateRequest)).Count() > 0))
                { 
                    _Context.Response = _Context.Request.CreateResponse(HttpStatusCode.Forbidden,
                            new HttpResponseMessage { ReasonPhrase = "Needs HTTPS,SSL certificate" },
                            new MediaTypeHeaderValue("text/json"));
                }
            }
            else
            {
                base.OnAuthorization(_Context);
            }
        }

    }
}

