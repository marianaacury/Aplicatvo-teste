namespace Aplicatvo_teste.View;

public partial class AvaliacoesPage : ContentPage
{
    public AvaliacoesPage()
    {
        InitializeComponent();
    }
    private async void IrMainPage(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}