using BDRPracticaXamarin.Models;
using BDRPracticaXamarin.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BDRPracticaXamarin
{
    public class APIConnectionHelper
    {
        private HttpClient client;


        private HttpClient GetInstance()
        {
            HttpClient _client = new HttpClient(new HttpClientHandler());

            _client.BaseAddress = new Uri("https://bdrpracticaapi20220530191625.azurewebsites.net/data/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return _client;
        }

        public async Task<List<Pais>> GetPaises()
        {
            client = GetInstance();

            HttpResponseMessage response = await client.GetAsync("getPaises");

            response.EnsureSuccessStatusCode();

            var paisesResponse = await response.Content.ReadAsStringAsync();

            List<Pais> paises = JsonConvert.DeserializeObject<List<Pais>>(paisesResponse);

            return paises;
        }

        public async Task<List<Cliente>> GetClientes()
        {

            client = GetInstance();

            HttpResponseMessage response = await client.GetAsync("getClientes");

            response.EnsureSuccessStatusCode();

            var clientesResponse = await response.Content.ReadAsStringAsync();

            List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(clientesResponse);

            return clientes;
        }

        public async Task<string> InsertCliente(Cliente cliente)
        {
            
            client = GetInstance();

            string clienteToJson = JsonConvert.SerializeObject(cliente);

            var content = new StringContent(clienteToJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("insertCliente", content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UpdateCliente(Cliente cliente)
        {

            client = GetInstance();

            string clienteToJson = JsonConvert.SerializeObject(cliente);

            var content = new StringContent(clienteToJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("updateCliente", content);

            response.EnsureSuccessStatusCode();

            
            
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteCliente(Int64 idCliente)
        {


            client = GetInstance();

            HttpResponseMessage response = await client.GetAsync($"deleteCliente?IdCliente={idCliente}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
