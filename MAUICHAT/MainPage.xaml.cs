namespace MAUICHAT
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
            {
                CounterBtn.Text = $"Message sent {count} time";
            }
            if (count >= 1 && count! < 100)
            {
                CounterBtn.Text = $"Message sent {count} times";
            }
            if (count >= 100 && count! < 500)
            {
                CounterBtn.Text = $"Stop wasting your time :) ({count})";
            }
            if (count >= 500 && count! < 1000)
            {
                CounterBtn.Text = $"If you dont stop it, i will. :D ({count})";
            }
            if (count == 1000)
            {
                CounterBtn.Text = $":DDDD";
            }
            if (count == 1001)
            {
                SemanticScreenReader.Announce(CounterBtn.Text);
                Application.Current.Quit();
            }
        }
        private void OnChatClicked(object sender, EventArgs e)
        {

        }
        }

}
