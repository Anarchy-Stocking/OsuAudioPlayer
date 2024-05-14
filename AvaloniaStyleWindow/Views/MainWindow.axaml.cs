using Avalonia.Controls;
using ReactiveUI;

namespace AvaloniaStyleWindow.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string? _SelectedItem;

        public string? SelectedItem
        {
            get => _SelectedItem;
            set => this._SelectedItem = value;
        }
        private void HandleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = e.AddedItems[0].ToString();
            // showSelectionCase.Text = SelectedItem;
        }
    }
}