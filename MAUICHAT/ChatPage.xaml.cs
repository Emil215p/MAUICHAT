using System.Text.Json;
using System.Text;
using System.Net.Http;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Timers;
using System.Dynamic;
namespace MAUICHAT;

public partial class ChatPage : ContentPage
{
    public class MyViewModel : INotifyPropertyChanged
    {
        private string? _editorContent;
        public string? EditorContent
        {
            get { return _editorContent; }
            set
            {
                if (_editorContent != value)
                {
                    _editorContent = value;
                    OnPropertyChanged(nameof(EditorContent));
                }
            }
        }
        private string? _nameContent;
        public string? NameContent
        {
            get { return _nameContent; }
            set
            {
                if (_nameContent != value)
                {
                    _nameContent = value;
                    OnPropertyChanged(nameof(NameContent));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
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
    private static void OnTimedMessage(object source, ElapsedEventArgs e)
    {
        OnGetMessage();
    }
    public ChatPage()
    {
        InitializeComponent();
        System.Timers.Timer timer = new(1000);
        timer.Elapsed += new ElapsedEventHandler(OnTimedMessage);
        timer.Interval = 1000;
        timer.Enabled = true;

        while (true)
        {
            Thread.Sleep(1000);
        }
    }
    private async void OnSendMessage(object sender, EventArgs e)
    {
        string user = namebox.Text;
        string? imageload = null;
        string? editorcontent = editor.Text;
        var client = new HttpClient();
        var data = new { username = user, message = editorcontent, image = imageload }; // Data to send
        var json = JsonConvert.SerializeObject(data); // Make the data Jason
        var content = new StringContent(json, Encoding.UTF8, "application/json"); // More Jason stuff
        await client.PostAsync("http://emko01.skp-dp.sde.dk/CSharpAPI_Test/index.php", content); // Post the data, declare API url
        editor.Text = ""; // Clear the editor
    }
    public class Messages
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string? Username { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("image")]
        public string? Image { get; set; }
    }

    public static async void OnGetMessage()
    {
            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync("http://emko01.skp-dp.sde.dk/CSharpAPI_Test/index.php"); // get the data, declare API url
            string content = await response.Content.ReadAsStringAsync(); // Get the content of the response
        _ = JsonConvert.DeserializeObject<Messages[]>(content);
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
    public Messages[]? Message1 { get; private set; }
}