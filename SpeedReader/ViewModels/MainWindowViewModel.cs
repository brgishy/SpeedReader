
namespace SpeedReader.ViewModels
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.Input;

    public partial class MainWindowViewModel : ViewModelBase
    {
        private string text = "Speed Reader v1.0";
        private int wordsPerMinute = 300;
        private int textSize = 70;

        public string Text
        {
            get => this.text;
            set => this.SetProperty(ref this.text, value);
        }

        public int TextSize
        {
            get => this.textSize;
            set => this.SetProperty(ref this.textSize, value);
        }

        public int WordsPerMinute
        {
            get => this.wordsPerMinute;
            set => this.SetProperty(ref this.wordsPerMinute, value);
        }

        [RelayCommand]
        private async Task Run()
        {
            string clipboardText = TextCopy.ClipboardService.GetText() ?? string.Empty;

            foreach (var word in clipboardText.Split(' ', '\r', '\n', '\t').Where(x => string.IsNullOrWhiteSpace(x) == false))
            {
                this.Text = word;
        
                // Delay between showing words
                await Task.Delay((int)((60.0f / this.wordsPerMinute) * 1000.0f));
            }
        }

        [RelayCommand]
        private void WordsPerMinuteDecrease() => this.WordsPerMinute = Math.Max(10, this.WordsPerMinute - 10);

        [RelayCommand]
        private void WordsPerMinuteIncrease() => this.WordsPerMinute += 10;

        [RelayCommand]
        private void TextSizeDecrease() => this.TextSize = Math.Max(10, this.TextSize - 5);

        [RelayCommand]
        private void TextSizeIncrease() => this.TextSize += 5;
    }
}
