using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LBTT_Calculator;
using System.Net.Http.Json;

namespace Tests
{
    public class APITest
    {
        private HttpClient _client;
        [SetUp]
        public void Setup()
        {
            var app = new WebApplicationFactory<Program>();
            _client = app.CreateClient();
        }

        [Test]
        public async Task ResidentialTest_ValidInput_ReturnsOk()
        {
            // Arrange
            var transactionDetails = new TransactionDetails(300000, 5000, true);
            var content = JsonContent.Create(transactionDetails);

            // Act
            var response = await _client.PostAsync("/residentialtest", content);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var result = await response.Content.ReadAsStringAsync();
            double.TryParse(result, out double taxResult);

            Assert.IsTrue(taxResult >= 0); // Assuming tax cannot be negative
        }
        [Test]
        public async Task ResidentialTest_NullInput_ReturnsBadRequest()
        {
            // Arrange
            var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/residentialtest", content);

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            var result = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("Invalid data.", result);
        }

        [Test]
        public async Task TestRootEndpoint()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var response = await client.GetStringAsync("/");

            Assert.Equals("Hello World!", response);
        }
    }
}
