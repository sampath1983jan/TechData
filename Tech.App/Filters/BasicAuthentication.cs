using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Tz.BackApp.Filters
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                var authHeader = actionContext.Request.Headers.Authorization;

                if (authHeader != null)
                {
                    var authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                    var decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                    var usernamePasswordArray = decodedAuthenticationToken.Split(':');
                    var userName = usernamePasswordArray[0];
                    var password = usernamePasswordArray[1];

                    //SMRHRT.Services.Collections.ErrorCollection.ErrorInfo objErrInfo = new SMRHRT.Services.Collections.ErrorCollection.ErrorInfo();
                    //TalentOZNet.APIModel.APIAuthentication objAPI = new TalentOZNet.APIModel.APIAuthentication(userName, password);
                    //objErrInfo = objAPI.Validate();
                    // Replace this with your own system of security / means of validating credentials
                    //var isValid = userName == "andy" && password == "password";
                    
                    //if (userName == "admin" && password == "312") {
                    //    var principal = new GenericPrincipal(new GenericIdentity(userName), null);
                    //    Thread.CurrentPrincipal = principal;
                    //    return;
                    //}    
                    
                    return;
                    //if (objErrInfo.TypeOfError == SMRHRT.Services.Collections.ErrorCollection.ErrorInfo.ErrorType.NO_ERROR)
                    //{
                    //    var principal = new GenericPrincipal(new GenericIdentity(objErrInfo.ErrorMessage), null);
                    //    Thread.CurrentPrincipal = principal;
                    //    //actionContext.Response =
                    //    //   actionContext.Request.CreateResponse(HttpStatusCode.OK,
                    //    //      "User " + userName + " successfully authenticated");

                    //    return;
                    //}
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            HandleUnathorized(actionContext);
        }
        private static void HandleUnathorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='Data' location = 'http://localhost:");
        }
    }
}