using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Clipboard;
using System.Linq;
using System.Diagnostics;

namespace SpeedReader
{
    public partial class MainPage : ContentPage
    {
        private int wordsPerMinute = 300;  // 300 = 5 words per second
        private int textSize = 70;

        public MainPage()
        {
            InitializeComponent();
        }

        // Called when the app first shows
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.UpdateText();

            // HACK [bgish]: Added placeholder row for debug mode so UWP buttons don't hide the UI
            DebugRow.Height = Debugger.IsAttached ? 20 : 0;
        }

        private async void RunClicked(object sender, EventArgs e)
        {
            string clipboardText = await CrossClipboard.Current.GetTextAsync();

            foreach (var word in clipboardText.Split(' ', '\r', '\n', '\t').Where(x => string.IsNullOrWhiteSpace(x) == false))
            {
                this.SpeedText.Text = word;

                // Delay between showing words
                await Task.Delay((int)((60.0f / wordsPerMinute) * 1000.0f));
            }
        }

        private void WordsPerMinuteDecreaseClicked(object sender, EventArgs e)
        {
            this.wordsPerMinute = Math.Max(10, this.wordsPerMinute - 10);
            this.UpdateText();
        }

        private void WordsPerMinuteIncreaseClicked(object sender, EventArgs e)
        {
            this.wordsPerMinute += 10;
            this.UpdateText();
        }

        private void TextDecreaseClicked(object sender, EventArgs e)
        {
            this.textSize = Math.Max(10, this.textSize - 5);
            this.UpdateText();
        }

        private void TextIncreaseClicked(object sender, EventArgs e)
        {
            this.textSize += 5;
            this.UpdateText();
        }

        private void UpdateText()
        {
            this.WordsPerMinute.Text = this.wordsPerMinute.ToString();
            this.TextSize.Text = this.textSize.ToString();
            this.SpeedText.FontSize = this.textSize;
        }
    }
}
