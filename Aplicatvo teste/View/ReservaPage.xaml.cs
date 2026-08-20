using Aplicatvo_teste.Models;
using Aplicatvo_teste.Services;
using Microsoft.Extensions.Hosting;
namespace Aplicatvo_teste.View;

public partial class ReservaPage : ContentPage
{
    private readonly ClimaService _climaService;

    List<Quarto> quartos = new();
    List<Passeio> passeios = new();
    public ReservaPage()
    {
        InitializeComponent();

        _climaService = new ClimaService();

        quartos.Add(new Quarto
        {
            Nome = "Villa Aconchego",
            Descricao = "Bangalô com vista para o vale, ofurô com hidromassagem e lareira.",
            Preco = 989,
            Icone = "🌿",
            Imagem = "quarto1.jpg"
        });

        quartos.Add(new Quarto
        {
            Nome = "Villa Aconchego Família",
            Descricao = "Bangalô com vista para o vale, ideal para hospedagem em família.",
            Preco = 1028,
            Icone = "💧",
            Imagem = "quarto2.jpg"
        });

        quartos.Add(new Quarto
        {
            Nome = "Villa Romance",
            Descricao = "Bangalô romântico de 60m² com banheira de hidromassagem e ofurô privativo.",
            Preco = 1850,
            Icone = "🏡",
            Imagem = "quarto3.jpg"
        });

        passeios.Add(new Passeio
        {
            Nome = "Picnic na Natureza",
            Descricao = "Escolha seu espaço na natureza e montamos seu picnic, com cesta de petiscos e vinho.",
            Horario = "Reserve na recepção",
            Preco = 525,
            Icone = "🥾"
        });

        passeios.Add(new Passeio
        {
            Nome = "Picnic na Varanda",
            Descricao = "Um picnic na varanda de seu bangalô, ao lado de seu ofurô, com vista do horizonte.",
            Horario = "Reserve na recepção",
            Preco = 525,
            Icone = "🪂"
        });

        passeios.Add(new Passeio
        {
            Nome = "Day Use",
            Descricao = "Nosso day use entrega mais valor e satisfação por cada real pago.",
            Horario = "Reserve agora",
            Preco = 200,
            Icone = "🛶"
        });

        passeios.Add(new Passeio
        {
            Nome = "Rapel na Cachoeira",
            Descricao = "Descida de rapel na cachoeira com instrutores certificados. Adrenalina pura!",
            Horario = "09:30 e 15:00",
            Preco = 200,
            Icone = "⛰️"
        });

        passeios.Add(new Passeio
        {
            Nome = "Fogueira & Estrelas",
            Descricao = "Noite especial ao redor da fogueira com contação de histórias e observação do céu.",
            Horario = "20:00",
            Preco = 60,
            Icone = "🔥"
        });

        ListaQuartosReserva.ItemsSource = quartos;
        ListaPasseiosReserva.ItemsSource = passeios;
    }

    private void ListaQuartosReserva_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (var quarto in quartos)
            quarto.Selecionado = false;

        if (ListaQuartosReserva.SelectedItem is Quarto selecionado)
            selecionado.Selecionado = true;
    }

    private void ListaPasseiosReserva_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        foreach (var passeio in passeios)
            passeio.Selecionado = false;

        foreach (var item in ListaPasseiosReserva.SelectedItems)
        {
            if (item is Passeio passeioSelecionado)
                passeioSelecionado.Selecionado = true;
        }
    }

    private async void OnConsultarClimaClicked(object? sender, EventArgs e)
    {
        lblClima.Text = "Consultando API...";

        string clima = await _climaService.ObterClimaAsync();

        lblClima.Text = clima;
    }

    private async void VerificarDisponibilidade(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txt_nome.Text))
        {
            await DisplayAlertAsync(
                "Atenção", "Por favor, preencha seu nome.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(txt_hospedes.Text))
        {
            await DisplayAlertAsync("Atenção", "Informe a quantidade de hóspedes.", "OK");
            return;
        }


        if ((DateTime)dtp_checkout.Date <= (DateTime)dtp_checkin.Date)
        {
            await DisplayAlertAsync("Atenção", "A data de check-out deve ser posterior ao check-in.", "OK");
            return;
        }

        var quartosSelecionados = quartos
    .Where(q => q.Selecionado == true)
    .ToList();

        if (!quartosSelecionados.Any())
        {
            await DisplayAlertAsync(
                "Atenção",
                "Selecione ao menos uma acomodação.",
                "OK");

            return;
        }

        var passeiosSelecionados = passeios.Where(p => p.Selecionado).ToList();

        int diarias = ((DateTime)dtp_checkout.Date - (DateTime)dtp_checkin.Date).Days;

        double totalAcomodacao = quartosSelecionados.Sum(q => q.Preco) * diarias;
        double totalPasseios = passeiosSelecionados.Sum(p => p.Preco);
        double totalGeral = totalAcomodacao + totalPasseios;

        string listaAcomodacoes = string.Join("\n", quartosSelecionados.Select(q => $"   • {q.Nome} (R$ {q.Preco:F2}/diária)"));
        string listaPasseios = passeiosSelecionados.Count > 0
            ? string.Join("\n", passeiosSelecionados.Select(p => $"   • {p.Nome} (R$ {p.Preco:F2})"))
            : "   Nenhum passeio selecionado";

        string mensagem = $"Olá, {txt_nome.Text}!\n\n" +
                          $"✅ Verificamos disponibilidade para:\n" +
                          $"📅 Check-in: {(DateTime)dtp_checkin.Date:dd/MM/yyyy}\n" +
                          $"📅 Check-out: {(DateTime)dtp_checkout.Date:dd/MM/yyyy}\n" +
                          $"🌙 {diarias} diária(s)\n" +
                          $"👥 {txt_hospedes.Text} hóspede(s)\n\n" +
                          $"🏡 Acomodações:\n{listaAcomodacoes}\n\n" +
                          $"🌿 Passeios:\n{listaPasseios}\n\n" +
                          $"💰 Total estimado: R$ {totalGeral:F2}\n\n" +
                          $"Nossa equipe entrará em contato para confirmar sua reserva. Obrigado!";

        await DisplayAlert("Solicitação Enviada!", mensagem, "Perfeito!");
    }

    private async void IrMainPage(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}