namespace Aplicatvo_teste.View;

public partial class PagamentoPage : ContentPage
{
    public PagamentoPage()
    {
        InitializeComponent();
    }
    private async void IrMainPage(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}