using API;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace TestIntegration
{
    public class TestIntegration
    {
        protected readonly HttpClient TestClient;

        public TestIntegration()
        {
            var appFactory = new CustomWebApplicationFactory<Startup>();
            TestClient = appFactory.CreateClient();
            TestClient.DefaultRequestHeaders.Add("consentedId", "1");
            TestClient.DefaultRequestHeaders.Add("documentId", "2");
            TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> ValidateResponse(HttpResponseMessage response, string endpoint)
        {
            object value = response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedGet = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(returnedGet);
            var schema = SchemaReader.GetSchema(endpoint);
            return jsonObject.IsValid(schema);
        }

        public async Task<bool> ValidateResponseArray(HttpResponseMessage response, string endpoint)
        {
            object value = response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedGet = await response.Content.ReadAsStringAsync();
            var jsonObject = JArray.Parse(returnedGet);
            var schema = SchemaReader.GetSchema(endpoint);
            return jsonObject.IsValid(schema);
        }

        public async Task<(bool, IList<string>)> ValidateResponse_WithErrorMessages(HttpResponseMessage response, string endpoint)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedGet = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(returnedGet);
            var schema = SchemaReader.GetSchema(endpoint);

            var valid = jsonObject.IsValid(schema, out IList<string> errorList);
            return (valid, errorList);
        }


    }
}
