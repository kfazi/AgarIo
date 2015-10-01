namespace AgarIo.AdminPanel.ViewModels
{
    using Caliburn.Micro;

    public class MainViewModel : Screen
    {
        public ConnectionViewModel ConnectionViewModel { get; set; }
        public SettingsViewModel SettingsViewModel { get; set; }

        public MainViewModel(ConnectionViewModel connectionViewModel, SettingsViewModel settingsViewModel)
        {
            SettingsViewModel = settingsViewModel;
            ConnectionViewModel = connectionViewModel;
        }
    }
}