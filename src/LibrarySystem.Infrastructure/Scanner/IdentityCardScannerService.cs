using LibrarySystem.Application.Interfaces;
using LibrarySystem.Application.Users.Commands;
using LibrarySystem.Core.Entities;
using LibrarySystem.Infrastructure.UserParser;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LibrarySystem.Infrastructure.Scanner
{
    public class IdentityCardScannerService : IIdentityCardScannerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserParser _userParser;

        public IdentityCardScannerService(
            IHttpClientFactory httpClientFactory,
            IUserParser userParser)
        {
            _httpClientFactory = httpClientFactory;
            _userParser = userParser;
        }

        public async Task<User> ScanAsync(byte[] identityCardImage)
        {
            var client = _httpClientFactory.CreateClient(HttpClientName.MicroBlinkClient);
            var base64Image = Convert.ToBase64String(identityCardImage);
            var mrzIdRequest = new MrzIdRequest(base64Image);
            HttpResponseMessage httpResponse = await client.PostAsJsonAsync(string.Empty, mrzIdRequest);
            httpResponse.EnsureSuccessStatusCode();
            var response = await httpResponse.Content.ReadFromJsonAsync<MrzIdResponse>();
            var user = _userParser.Parse(response);
            return user;
        }
    }
}
