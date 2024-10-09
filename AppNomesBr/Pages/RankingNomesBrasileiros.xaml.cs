using AppNomesBr.Domain.Interfaces.Services;
using Newtonsoft.Json; // Para usar o Newtonsoft.Json
using System.Collections.Generic; // Para List<>
using System.Net.Http;
using System.Threading.Tasks; // Para Task
using Microsoft.Maui.Controls; // Importando o namespace correto para ContentPage

namespace AppNomesBr.Pages
{
    public partial class RankingNomesBrasileiros : ContentPage
    {
        private readonly INomesBrService service;

        public RankingNomesBrasileiros(INomesBrService service)
        {
            InitializeComponent();
            this.service = service;
        }

        protected override async void OnAppearing()
        {
            await CarregarNomes();
            base.OnAppearing();
        }

        private async Task CarregarNomes()
        {
            var result = await service.ListaTop20Nacional();
            this.GrdNomesBr.ItemsSource = result;
        }

        // O método OnPesquisarClicked agora está corrigido
        private async void OnPesquisarClicked(object sender, EventArgs e)
        {
            string cidade = entryCity.Text;
            string sexo = pickerSex.SelectedItem?.ToString().ToLower();

            if (!string.IsNullOrEmpty(cidade) && !string.IsNullOrEmpty(sexo))
            {
                // Construa a URL da API com os parâmetros
                string apiUrl = $"https://servicodados.ibge.gov.br/api/v2/censo/nomes/{cidade}?sexo={sexo}";

                try
                {
                    // Faça a chamada à API
                    using HttpClient client = new HttpClient();
                    var response = await client.GetStringAsync(apiUrl);

                    // Atualize a listagem com os dados retornados
                    var nomes = JsonConvert.DeserializeObject<List<Nomes>>(response);
                    GrdNomesBr.ItemsSource = nomes; // Atualiza o CollectionView com os dados recebidos
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", "Não foi possível carregar os dados: " + ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Atenção", "Por favor, preencha todos os campos.", "OK");
            }
        }
    }

    // Defina a classe Nomes de acordo com a estrutura esperada da API
    public class Nomes
    {
        public string Nome { get; set; }
        public int Frequencia { get; set; }
        public int Ranking { get; set; }
    }
}
