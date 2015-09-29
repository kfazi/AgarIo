namespace AgarIo.AdminPanel.ViewModels
{
    using Caliburn.Micro;

    public class MainViewModel : Screen
    {
        public ArenaViewModel ArenaViewModel { get; set; }
        public ConnectionViewModel ConnectionViewModel { get; set; }
        public SettingsViewModel SettingsViewModel { get; set; }

        public MainViewModel(ConnectionViewModel connectionViewModel, ArenaViewModel arenaViewModel, SettingsViewModel settingsViewModel)
        {
            ArenaViewModel = arenaViewModel;
            SettingsViewModel = settingsViewModel;
            ConnectionViewModel = connectionViewModel;
        }
    }
}