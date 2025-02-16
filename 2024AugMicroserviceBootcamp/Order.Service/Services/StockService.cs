using System.Net.Http.Json;

namespace Oder.API.Services
{
    public record CheckStockResponse(bool Status);
    public class StockService(HttpClient httpClient)
    {
        public async Task<bool> CheckStockAsync(int productId, int quantity)
        {
            var response = await httpClient.GetAsync($"api/stocks/{productId}/{quantity}");

            if (response.IsSuccessStatusCode)
            {
                //log
                return false;
            }

            var content = await response.Content.ReadFromJsonAsync<CheckStockResponse>();
            return content!.Status;
        }
    }
}