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
namespace Tz.BackApp.Controllers.API
{
    public class Car
    {
        public string ClientKey;
        public string ScriptID;
        public List<Inparam> inparam;
        public List<Outparam> outparam;
    }

    [BasicAuthentication]
    [RoutePrefix("api/Demo")]
    public class DemoController : ApiController
    {
        // GET: api/Demo
        [HttpGet]
        [Route("GetResult")]
        public HttpResponseMessage Get(Car c)
        {
            string ss = "d:k=Expr(13 + 5);";
            EvaluationParam ev = new EvaluationParam("connection", "Server=dell6;Initial Catalog=talentozdev;Uid=root;Pwd=admin312");
            QScriptStatement sq = new QScriptStatement(ss, ev);
            var res = sq.Evaluation();
            string s =Newtonsoft.Json.JsonConvert.SerializeObject(res.get(c.outparam[0].key));
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
