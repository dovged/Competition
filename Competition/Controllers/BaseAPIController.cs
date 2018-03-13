using Competition.Context;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Competition.Controllers
{
    public class BaseAPIController : ApiController
    {
        protected readonly DbContext CompetitionDB = new DbContext();

        /** PABAIGOJE IŠTRINTI*/
        protected HttpResponseMessage ToJson(dynamic obj)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            return response;
        }

        /** Grąžina HttpResponseMessage;
            kodas - 200;
            turinys - @param obj, json formatu;*/
        protected HttpResponseMessage ToJsonOK(dynamic obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        /** Grąžina HttpResponseMessage;
            kodas - 404;
            turinys - @param obj, json formatu;*/
        protected HttpResponseMessage ToJsonNotFound(dynamic obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            response.StatusCode = HttpStatusCode.NotFound;
            return response;
        }

        /** Grąžina HttpResponseMessage;
            kodas - 201;
            turinys - @param obj, json formatu;*/
        protected HttpResponseMessage ToJsonCreated(dynamic obj)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }
    }
}