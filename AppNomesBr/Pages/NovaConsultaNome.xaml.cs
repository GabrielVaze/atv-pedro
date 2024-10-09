using AppNomesBr.Domain.Interfaces.Repositories;
using AppNomesBr.Domain.Interfaces.Services;
using Newtonsoft.Json; // Adicione esta linha para usar o Newtonsoft.Json
using System.Net.Http;

namespace AppNomesBr.Pages
{
    public partial class NovaConsultaNome : ContentPage
    {
        private readonly INomesBrService service;
        private readonly INomesBrRepository repository;

        public NovaConsultaNome(INomesBrService service, INomesBrRepository repository)
        {
            InitializeComponent();
            this.service = service;
            this.repository = repository;
            BtnPesquisar.Clicked += BtnPesquisar_Clicked;
            BtnDeleteAll.Clicked += BtnDeleteAll_Clicked;
        }

        private async void BtnDeleteAll_Clicked(object? sender, EventArgs e)
        {
            var registros = await repository.GetAll();

            foreach (var registro in registros)
                await repository.Delete(registro.Id);

            await CarregarNomes();
        }

        protected override async void OnAppearing()
        {
            await CarregarNomes();
            base.OnAppearing();
        }

        private async void BtnPesquisar_Clicked(object? sender, EventArgs e)
        {
            // Lógica para pesquisar e atualizar o ranking com base no nome e sexo
            string nome = TxtNome.Text.ToUpper();
            string sexo = pickerSexo.SelectedItem?.ToString(); // Acesse o Picker corretamente

            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(sexo))
            {
                // Chame seu método de serviço para inserir um novo registro considerando o sexo
                await service.InserirNovoRegistroNoRanking(nome, sexo);
                await CarregarNomes();
            }
            else
            {
                await DisplayAlert("Atenção", "Por favor, preencha todos os campos.", "OK");
            }
        }

        private async Task CarregarNomes()
        {
            try
            {
                var result = await service.ListaMeuRanking();
                this.GrdNomesBr.ItemsSource = result; // Atualizado para definir diretamente a lista de nomes
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao carregar nomes: {ex.Message}", "OK");
            }
        }

        // Método para buscar ranking baseado no sexo selecionado
        private async void OnUpdateClicked(object sender, EventArgs e)
        {
            string sexo = pickerSexo.SelectedItem?.ToString();

            // Verifique se o sexo foi selecionado
            if (!string.IsNullOrEmpty(sexo))
            {
                try
                {
                    var result = await service.ListaMeuRankingPorSexo(sexo);
                    this.GrdNomesBr.ItemsSource = result; // Atualizado para definir diretamente a lista de nomes
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Erro", $"Erro ao carregar nomes por sexo: {ex.Message}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Atenção", "Por favor, selecione um sexo.", "OK");
            }
        }

        // Defina a classe Nomes de acordo com a estrutura esperada da API
        public class Nomes
        {
            public string Nome { get; set; }
            public int Frequencia { get; set; }
            public int Ranking { get; set; }
            public string Sexo { get; set; } // Adicione a propriedade Sexo
        }
    }
}
