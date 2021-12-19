using CommunityToolkit.WinUI.Helpers;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Loaf
{
    public sealed partial class MainView 
    {
        private static bool useWin10Style = false;
        private static bool firstLaunch = true;
        private bool UseWin10Style
        {
            get => useWin10Style;
            set
            {
                useWin10Style = value;
                StyleTextBlock.Text = ResourceExtensions.GetLocalized(value ? "Win10StyleText" : "Win11StyleText");
            }
        }

        public MainView()
        {
            InitializeComponent();
            VersionElement.Text = GetVersion();
            StyleToggleButton.Loaded += OnStyleToggleButtonLoaded;
        }


        private static string GetVersion()
        {
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;

            return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision} Reloaded";
        }

        private void OnStyleToggleButtonLoaded(object sender, RoutedEventArgs e)
        {
            StyleToggleButton.IsChecked = UseWin10Style;
        }

        private void OnToggleStyle(object sender, RoutedEventArgs e)
        {
            UseWin10Style = StyleToggleButton.IsChecked == true;
        }

        private void OnLoaf(object sender, RoutedEventArgs e)
        {
            if (firstLaunch)
            {
                LoafTeachingTip.IsOpen = true;
            }
            else
            {
                OnStartLoaf(null, null);
            }
        }

        private void OnStartLoaf(TeachingTip sender, object args)
        {
            MainWindow.Instance.Loaf(UseWin10Style ? Microsoft.UI.ColorHelper.FromArgb(192, 00, 120, 215) : Colors.Black);
            firstLaunch = false;
        }

        private async void OnRate(object sender, RoutedEventArgs e)
        {
            await SystemInformation.LaunchStoreForReviewAsync();
        }
    }
}
