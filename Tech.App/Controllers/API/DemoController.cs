using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Tech.QScript;
using Tz.BackApp.Filters;
using Tz.BackApp.Models;
using Tz.Net;

namespace Tz.BackApp.Controllers.API
{
    public class Param
    {
        public string ClientID;
        public string Intend;
        public List<Inparam> InputParam;
        public List<Outparam> outparam;
    }

  //  [BasicAuthentication]
    [RoutePrefix("api/Demo")]
    public class DemoController : ApiController
    {
        // GET: api/Demo
        [HttpPost]
        [Route("GetResult")]
        public HttpResponseMessage Get(Param c)
        {
            //string ss = "d:k=Expr(13 + 5);";
            //EvaluationParam ev = new EvaluationParam("connection", "Server=dell6;Initial Catalog=talentozdev;Uid=root;Pwd=admin312");
            //QScriptStatement sq = new QScriptStatement(ss, ev);
            //var res = sq.Evaluation();

            DataScriptManager dataScript = new DataScriptManager(c.Intend,c.ClientID);
            foreach (Inparam p in c.InputParam) {
                dataScript.AddParam(p.Name,p.Value);
            }
            object res;
            try
            {
                res = dataScript.GetResult("result");
            }
            catch (Exception ex) {
                var r = Request.CreateResponse(HttpStatusCode.InternalServerError);
                r.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                return r;
            }
            string s =Newtonsoft.Json.JsonConvert.SerializeObject(res);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(s, Encoding.UTF8, "application/json");
            return response;             
        }
        // GET: api/Demo/5
        public string Get(int id)
        {
            return "value";
        }
        // POST: api/Demo
        public void Post([FromBody]string value)
        {
        }
        // PUT: api/Demo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Demo/5
        public void Delete(int id)
        {
        }
    }

    
}
