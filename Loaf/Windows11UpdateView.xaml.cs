using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Loaf
{
    public sealed partial class Windows11UpdateView
    {
        private bool _disposed = false;

        public Windows11UpdateView()
        {
            InitializeComponent();
            Loaded += Windows11UpdateView_Loaded;
            Unloaded += Windows11UpdateView_Unloaded;
            // KeyDown += Windows11UpdateView_KeyDown;
            PointerReleased += Windows11UpdateView_PointerReleased;

        }

        private void Windows11UpdateView_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            Focus(FocusState.Keyboard);
        }

        // private void Windows11UpdateView_KeyDown(object sender, KeyRoutedEventArgs e)
        // {
        // }

        private void Windows11UpdateView_Unloaded(object sender, RoutedEventArgs e)
        {
            _disposed = true;
        }

        private async void Windows11UpdateView_Loaded(object sender, RoutedEventArgs e)
        {
            int index = 0;
            while (_disposed == false)
            {
                UpdatingElement.Text = string.Format(ResourceExtensions.GetLocalized("UpdatingText"), (index++) % 101);
                await Task.Delay(TimeSpan.FromSeconds(new Random().Next(1,16)));
            }
        }
    }
}
