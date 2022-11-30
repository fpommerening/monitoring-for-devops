using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FP.Monitoring.Trace.Common;
using FP.Monitoring.Trace.Common.Models;
using FP.Monitoring.Trace.UI.Models;

namespace FP.Monitoring.Trace.UI.Business
{
    public class OrderRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public OrderRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IReadOnlyList<ProductViewModel>> GetProducts()
        {
            using var activity = DemoActivitySource.ActivitySource.StartActivity("GetProducts");
            activity?.AddTag("class", nameof(OrderRepository));

            var client = _httpClientFactory.CreateClient("stockservice");
            var response = await client.GetAsync("Product");
            
            var products = JsonSerializer.Deserialize<Product[]>(await response.Content.ReadAsStringAsync(), _jsonSerializerOptions);

            return products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Quantity = x.Quantity
            }).ToList().AsReadOnly();
        }

        public async Task OrderProduct(Guid id, int quantity, string customer, string cardType, string cardNumber)
        {
            using var activity = DemoActivitySource.ActivitySource.StartActivity("OrderProduct");
            activity?.AddTag("class", nameof(OrderRepository));

            var stockClient = _httpClientFactory.CreateClient("stockservice");
            var productsResponse = await stockClient.GetAsync("Product");
            var products = JsonSerializer.Deserialize<Product[]>(await productsResponse.Content.ReadAsStringAsync(), _jsonSerializerOptions);
            var product = products.Single(x => x.Id == id);

            var orderResponse = await stockClient.PutAsync($"Product/{id}", new StringContent(quantity.ToString(), Encoding.UTF8, "application/json"));
            orderResponse.EnsureSuccessStatusCode();

            await Task.Delay(250);
            var payment = new Payment
            {
                Name = customer,
                Amount = product.Price * (decimal) quantity,
                Number = cardNumber,
                Type = cardType
            };

            var paymentClient = _httpClientFactory.CreateClient("paymentservice");
            var paymentAsJson = JsonSerializer.Serialize(payment, _jsonSerializerOptions);

            var paymentResponse = await paymentClient.PutAsync("Payment", new StringContent(paymentAsJson, Encoding.UTF8, "application/json"));
            paymentResponse.EnsureSuccessStatusCode();

        }
    }
    
}
