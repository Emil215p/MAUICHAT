namespace MAUICHAT;

public partial class ChatPage : ContentPage
{
    public ChatPage()
    {
        InitializeComponent();
    }

    private void OnSendMessage(object sender, EventArgs e)
    { 
    
    }

    private void OnReturnMenu(object sender, EventArgs e)
    {
        App.Current.MainPage = new NavigationPage(new MainPage());
    }
}