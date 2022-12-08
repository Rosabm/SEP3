using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace HttpClients.Implementations;

public class SockHttpClient:ISockService
{
    private readonly HttpClient client;
    private ISockService _sockServiceImplementation;

    public SockHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<SocksCard> Create(CreateSocksCardDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("Socks", dto);
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        SocksCard product = JsonSerializer.Deserialize<SocksCard>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return product;
    }

    public async Task<SocksCard> GetById(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"https://localhost:7999/Socks/{id}");
        string result = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        SocksCard product = JsonSerializer.Deserialize<SocksCard>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return product;
    }

    public async Task<ICollection<SocksCard>> GetAllSockCards()
    {
        HttpResponseMessage response = await client.GetAsync("https://localhost:7999/Socks");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<SocksCard> socks = JsonSerializer.Deserialize<ICollection<SocksCard>>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
      
        return socks;
    }

    public async Task RemoveProductCardSockById(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7999/Socks/{id}");
        string content = await response.Content.ReadAsStringAsync();

      
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}