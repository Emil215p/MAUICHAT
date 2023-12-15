using System.Text.Json;
using System.Text;
using System.Net.Http;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Timers;
using System.Dynamic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace MAUICHAT;

public partial class ChatPage : ContentPage
{

    readonly ChatPageViewModel ViewModel = new();

    public ChatPage()
    {
        InitializeComponent();

        BindingContext = ViewModel;
    }

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
    private async void OnSendMessage(object sender, EventArgs e)
    {
        await scrollView.ScrollToAsync(0, 0, false);

        string user = namebox.Text;
        string? imageload = null;
        string? editorcontent = editor.Text;
        await ViewModel.messageViewModel.SetMessage(user, editorcontent, imageload);
        await DisplayAlert("Alert", "Message Sent ", "OK");
        editor.Text = string.Empty; // Clear the editor
    }

    private async void OnAttachMedia(object sender, EventArgs e)
    {
        await PickAndShow(PickOptions.Default); // Pick the file
    }
    private void OnReturnMenu(object sender, EventArgs e)
    {
        if (Application.Current != null)
        {
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
    }
    void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        Console.WriteLine($"ScrollX: {e.ScrollX}, ScrollY: {e.ScrollY}");
    }
}

