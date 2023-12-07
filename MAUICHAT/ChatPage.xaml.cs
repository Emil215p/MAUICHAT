namespace MAUICHAT;

public partial class ChatPage : ContentPage
{
    public async Task<FileResult?> PickAndShow(PickOptions options)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(options);
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    var image = ImageSource.FromStream(() => stream);
                }
            }
            if (result != null)
            {
                return result;
            }
        }
        catch (Exception)
        {
            // The user cancelled or something went wrong
            await DisplayAlert("Alert", "Action cancelled", "OK");

        }

        return null;
    }

    public ChatPage()
    {
        InitializeComponent();
    }

    private void OnSendMessage(object sender, EventArgs e)
    {

    }
    private async void OnAttachMedia(object sender, EventArgs e)
    {
        await PickAndShow(PickOptions.Default);
    }

    private void OnReturnMenu(object sender, EventArgs e)
    {
        if (Application.Current != null)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
}