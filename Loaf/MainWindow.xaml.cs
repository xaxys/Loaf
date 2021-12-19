using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.UI;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Loaf
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private bool _isLoafing;

        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            Root.Loaded += OnLoaded;
            Root.KeyDown += Root_KeyDown;
            Activated += MainWindow_Activated;
        }

        private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            var control = Frame.Content as Page;
            if (control != null && _isLoafing)
            {
                control.Focus(FocusState.Keyboard);
            }
        }

        public static MainWindow Instance { get; private set; }

        private void Root_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (_isLoafing && e.Key == Windows.System.VirtualKey.Escape)
            {
                Unload();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainView));
            Frame.Navigate(typeof(Windows11UpdateView));
            Frame.GoBack();
            _appWindow = GetAppWindowForCurrentWindow();
            _appWindow.Title = "WinUI" + ResourceExtensions.GetLocalized("Loaf") + " Reloaded By xa";
        }

        private AppWindow _appWindow;

        public void Loaf(Color background)
        {
            Frame.Background = new SolidColorBrush(background);
            _appWindow.Show();
            Load();
        }

        public void Load()
        {
            Frame.GoForward();
            _appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
            var parent = VisualTreeHelper.GetParent(Root);
            while (parent != null)
            {
                if (parent is FrameworkElement element)
                {
                    element.IsHitTestVisible = false;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }

            while (ShowCursor(false) >= 0) ; //隐藏光标
            // 阻止系统睡眠，阻止屏幕关闭。
            SystemSleep.PreventForCurrentThread();
            _isLoafing = true;
        }

        public void Unload()
        {
            Frame.GoBack();
            _appWindow.SetPresenter(AppWindowPresenterKind.Default);
            var parent = VisualTreeHelper.GetParent(Root);
            while (parent != null)
            {
                if (parent is FrameworkElement element)
                {
                    element.IsHitTestVisible = true;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }
            while (ShowCursor(true) < 0) ; //显示光标
            // 恢复此线程曾经阻止的系统休眠和屏幕关闭。
            SystemSleep.RestoreForCurrentThread();
            _isLoafing = false;
        }

        [DllImport("user32", EntryPoint = "ShowCursor")]
        private extern static int ShowCursor(bool show);

        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        private AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            WindowId myWndId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }
    }
}
