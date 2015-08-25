namespace AgarIo.AdminPanel.ViewModels
{
    using Caliburn.Micro;

    public class MainViewModel : Screen
    {
        public ArenaViewModel ArenaViewModel { get; set; }
        public ConnectionViewModel ConnectionViewModel { get; set; }

        public MainViewModel(ConnectionViewModel connectionViewModel, ArenaViewModel arenaViewModel)
        {
            ArenaViewModel = arenaViewModel;
            ConnectionViewModel = connectionViewModel;
        }
    }
}