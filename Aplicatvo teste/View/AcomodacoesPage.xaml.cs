using Aplicatvo_teste.Models;

namespace Aplicatvo_teste.View;

public partial class AcomodacoesPage : ContentPage
{
    List<Quarto> quartos = new();

    public AcomodacoesPage()
    {
        InitializeComponent();

        quartos.Add(new Quarto
        {

            Nome = "Villa Aconchego",
            Descricao = "Sinta o aconchego de se hospedar em nosso bangalô com vista para o vale, ofurô com hidromassagem e lareira." +
                            " Aproveite nossa estrutura de resort com piscinas, trilhas e atividades de aventura. ",
            Preco = 989,
            Icone = "🌿",
            Imagem = "quarto1.jpg"
        });

        quartos.Add(new Quarto
        {


            Nome = "Villa Aconchego Família",
            Descricao = "Hospede-se em família no nosso bangalô com vista para o vale, ofurô com hidromassagem e lareira. " +
                        "Aproveite nossa estrutura de resort com piscinas, trilhas e atividades de aventura para toda a família.",
            Preco = 1028,
            Icone = "💧",
            Imagem = "quarto2.jpg"

        });

        quartos.Add(new Quarto
        {

            Nome = "Villa Romance",
            Descricao = "Amplo bangalô de 60m², com dois ambientes cuidadosamente projetados para proporcionar uma experiência romântica, confortável e inesquecível a dois." +
                        "Dispõe de cama king size, banheira interna de hidromassagem com hidroterapia e cromoterapia, além de uma charmosa varanda com ofurô privativo, perfeita para momentos de relaxamento.",
            Preco = 1850,
            Icone = "🏡",
            Imagem = "quarto3.jpg"

        });

        ListaQuartos.ItemsSource = quartos;
    }

    private async void IrMainPage(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }

}

