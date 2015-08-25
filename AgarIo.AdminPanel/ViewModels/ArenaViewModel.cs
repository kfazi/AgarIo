namespace AgarIo.AdminPanel.ViewModels
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;

    using AgarIo.AdminPanel.Events;
    using AgarIo.Contract;
    using AgarIo.Contract.AdminCommands;

    using Caliburn.Micro;

    public class UpdateableFieldViewModel<T> : PropertyChangedBase
    {
        private T _value;

        private T _originalValue;

        public T Value
        {
            get { return _value; }

            set
            {
                if (Equals(value, _value)) return;
                _value = value;
                IsValid = Validation(value);
                NotifyOfPropertyChange(() => Value);
                NotifyOfPropertyChange(() => IsModified);
            }
        }

        public T OriginalValue
        {
            get { return _value; }

            set
            {
                if (Equals(value, _originalValue)) return;

                if (Equals(_value, _originalValue))
                {
                    _value = value;
                    NotifyOfPropertyChange(() => Value);
                }

                _originalValue = value;
                NotifyOfPropertyChange(() => OriginalValue);
            }
        }

        public bool IsValid
        {
            get { return _isValid; }

            private set
            {
                if (value == _isValid) return;
                _isValid = value;
                NotifyOfPropertyChange(() => IsValid);
            }
        }

        public bool IsModified => !Equals(_value, _originalValue);

        public Func<T, bool> Validation = value => true;

        private bool _isValid;
    }

    public class SettingsViewModel
    {
        public SettingsViewModel()
        {
            MinPlayerBlobMass = new UpdateableFieldViewModel<float>();
            MaxPlayerBlobMass = new UpdateableFieldViewModel<float>();
            VisibilityFactor = new UpdateableFieldViewModel<float>();
            MassGainFactor = new UpdateableFieldViewModel<float>();
            RecombineWaitTimeMassFactor = new UpdateableFieldViewModel<float>();
            MassLossPerTick = new UpdateableFieldViewModel<float>();
            MoveDecayPerTick = new UpdateableFieldViewModel<float>();
            PlayerEatingRangeFactor = new UpdateableFieldViewModel<float>();
            VirusEatingRangeFactor = new UpdateableFieldViewModel<float>();
            SplitSpeedFactor = new UpdateableFieldViewModel<float>();
            VirusMassMultiplier = new UpdateableFieldViewModel<float>();
            StandardEatingMassMultiplier = new UpdateableFieldViewModel<float>();
            MaxPlayerBlobCount = new UpdateableFieldViewModel<int>();
            FoodSpawnAmount = new UpdateableFieldViewModel<int>();
            MaxFoodCount = new UpdateableFieldViewModel<int>();
            FoodMass = new UpdateableFieldViewModel<int>();
            VirusMinAmount = new UpdateableFieldViewModel<int>();
            VirusMaxAmount = new UpdateableFieldViewModel<int>();
            VirusMinMass = new UpdateableFieldViewModel<int>();
            VirusFeedAmount = new UpdateableFieldViewModel<int>();
            EjectMass = new UpdateableFieldViewModel<int>();
            EjectMassLoss = new UpdateableFieldViewModel<int>();
            EjectSpeed = new UpdateableFieldViewModel<int>();
            MinMassEject = new UpdateableFieldViewModel<int>();
            MinMassSplit = new UpdateableFieldViewModel<int>();
            InitialFoodAmount = new UpdateableFieldViewModel<int>();
            RecombineWaitTicks = new UpdateableFieldViewModel<uint>();
            FoodSpawnTicksInterval = new UpdateableFieldViewModel<uint>();
            VirusSpawnTicksInterval = new UpdateableFieldViewModel<uint>();
        }

        public UpdateableFieldViewModel<float> MinPlayerBlobMass { get; }

        public UpdateableFieldViewModel<float> MaxPlayerBlobMass { get; }

        public UpdateableFieldViewModel<float> VisibilityFactor { get; }

        public UpdateableFieldViewModel<float> MassGainFactor { get; }

        public UpdateableFieldViewModel<float> RecombineWaitTimeMassFactor { get; }

        public UpdateableFieldViewModel<float> MassLossPerTick { get; }

        public UpdateableFieldViewModel<float> MoveDecayPerTick { get; }

        public UpdateableFieldViewModel<float> PlayerEatingRangeFactor { get; }

        public UpdateableFieldViewModel<float> VirusEatingRangeFactor { get; }

        public UpdateableFieldViewModel<float> SplitSpeedFactor { get; }

        public UpdateableFieldViewModel<float> VirusMassMultiplier { get; }

        public UpdateableFieldViewModel<float> StandardEatingMassMultiplier { get; }

        public UpdateableFieldViewModel<int> MaxPlayerBlobCount { get; }

        public UpdateableFieldViewModel<int> FoodSpawnAmount { get; }

        public UpdateableFieldViewModel<int> MaxFoodCount { get; }

        public UpdateableFieldViewModel<int> FoodMass { get; }

        public UpdateableFieldViewModel<int> VirusMinAmount { get; }

        public UpdateableFieldViewModel<int> VirusMaxAmount { get; }

        public UpdateableFieldViewModel<int> VirusMinMass { get; }

        public UpdateableFieldViewModel<int> VirusFeedAmount { get; }

        public UpdateableFieldViewModel<int> EjectMass { get; }

        public UpdateableFieldViewModel<int> EjectMassLoss { get; }

        public UpdateableFieldViewModel<int> EjectSpeed { get; }

        public UpdateableFieldViewModel<int> MinMassEject { get; }

        public UpdateableFieldViewModel<int> MinMassSplit { get; }

        public UpdateableFieldViewModel<int> InitialFoodAmount { get; }

        public UpdateableFieldViewModel<uint> RecombineWaitTicks { get; }

        public UpdateableFieldViewModel<uint> FoodSpawnTicksInterval { get; }

        public UpdateableFieldViewModel<uint> VirusSpawnTicksInterval { get; }

        public void UpdateFromServer(WorldSettingsDto worldSettingsDto)
        {
            MinPlayerBlobMass.OriginalValue = worldSettingsDto.MinPlayerBlobMass;
            MaxPlayerBlobMass.OriginalValue = worldSettingsDto.MaxPlayerBlobMass;
            VisibilityFactor.OriginalValue = worldSettingsDto.VisibilityFactor;
            MassGainFactor.OriginalValue = worldSettingsDto.MassGainFactor;
            RecombineWaitTimeMassFactor.OriginalValue = worldSettingsDto.RecombineWaitTimeMassFactor;
            MassLossPerTick.OriginalValue = worldSettingsDto.MassLossPerTick;
            MoveDecayPerTick.OriginalValue = worldSettingsDto.MoveDecayPerTick;
            PlayerEatingRangeFactor.OriginalValue = worldSettingsDto.PlayerEatingRangeFactor;
            VirusEatingRangeFactor.OriginalValue = worldSettingsDto.VirusEatingRangeFactor;
            SplitSpeedFactor.OriginalValue = worldSettingsDto.SplitSpeedFactor;
            VirusMassMultiplier.OriginalValue = worldSettingsDto.VirusMassMultiplier;
            StandardEatingMassMultiplier.OriginalValue = worldSettingsDto.StandardEatingMassMultiplier;
            MaxPlayerBlobCount.OriginalValue = worldSettingsDto.MaxPlayerBlobCount;
            FoodSpawnAmount.OriginalValue = worldSettingsDto.FoodSpawnAmount;
            MaxFoodCount.OriginalValue = worldSettingsDto.MaxFoodCount;
            FoodMass.OriginalValue = worldSettingsDto.FoodMass;
            VirusMinAmount.OriginalValue = worldSettingsDto.VirusMinAmount;
            VirusMaxAmount.OriginalValue = worldSettingsDto.VirusMaxAmount;
            VirusMinMass.OriginalValue = worldSettingsDto.VirusMinMass;
            VirusFeedAmount.OriginalValue = worldSettingsDto.VirusFeedAmount;
            EjectMass.OriginalValue = worldSettingsDto.EjectMass;
            EjectMassLoss.OriginalValue = worldSettingsDto.EjectMassLoss;
            EjectSpeed.OriginalValue = worldSettingsDto.EjectSpeed;
            MinMassEject.OriginalValue = worldSettingsDto.MinMassEject;
            MinMassSplit.OriginalValue = worldSettingsDto.MinMassSplit;
            InitialFoodAmount.OriginalValue = worldSettingsDto.InitialFoodAmount;
            RecombineWaitTicks.OriginalValue = worldSettingsDto.RecombineWaitTicks;
            FoodSpawnTicksInterval.OriginalValue = worldSettingsDto.FoodSpawnTicksInterval;
            VirusSpawnTicksInterval.OriginalValue = worldSettingsDto.VirusSpawnTicksInterval;
        }

        public WorldSettingsDto GetWorldSettingsDto()
        {
            return new WorldSettingsDto
            {
                MinPlayerBlobMass = MinPlayerBlobMass.Value,
                MaxPlayerBlobMass = MaxPlayerBlobMass.Value,
                VisibilityFactor = VisibilityFactor.Value,
                MassGainFactor = MassGainFactor.Value,
                RecombineWaitTimeMassFactor = RecombineWaitTimeMassFactor.Value,
                MassLossPerTick = MassLossPerTick.Value,
                MoveDecayPerTick = MoveDecayPerTick.Value,
                PlayerEatingRangeFactor = PlayerEatingRangeFactor.Value,
                VirusEatingRangeFactor = VirusEatingRangeFactor.Value,
                SplitSpeedFactor = SplitSpeedFactor.Value,
                VirusMassMultiplier = VirusMassMultiplier.Value,
                StandardEatingMassMultiplier = StandardEatingMassMultiplier.Value,
                MaxPlayerBlobCount = MaxPlayerBlobCount.Value,
                FoodSpawnAmount = FoodSpawnAmount.Value,
                MaxFoodCount = MaxFoodCount.Value,
                FoodMass = FoodMass.Value,
                VirusMinAmount = VirusMinAmount.Value,
                VirusMaxAmount = VirusMaxAmount.Value,
                VirusMinMass = VirusMinMass.Value,
                VirusFeedAmount = VirusFeedAmount.Value,
                EjectMass = EjectMass.Value,
                EjectMassLoss = EjectMassLoss.Value,
                EjectSpeed = EjectSpeed.Value,
                MinMassEject = MinMassEject.Value,
                MinMassSplit = MinMassSplit.Value,
                InitialFoodAmount = InitialFoodAmount.Value,
                RecombineWaitTicks = RecombineWaitTicks.Value,
                FoodSpawnTicksInterval = FoodSpawnTicksInterval.Value,
                VirusSpawnTicksInterval = VirusSpawnTicksInterval.Value
            };
        }
    }

    public class ArenaViewModel : Screen, IHandle<LoggedInEvent>, IHandle<DisconnectedEvent>
    {
        private const int BitmapSize = 1000;

        private readonly IConnection _connection;

        private readonly DispatcherTimer _dispatcherTimer;

        private WriteableBitmap _bitmap;

        private float _scale;

        public ArenaViewModel(IEventAggregator eventAggregator, IConnection connection)
        {
            _connection = connection;

            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += OnTick;
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(100);

            var bitmap = BitmapFactory.New(BitmapSize, BitmapSize);
            Bitmap = BitmapFactory.ConvertToPbgra32Format(bitmap);

            eventAggregator.Subscribe(this);
        }

        public WriteableBitmap Bitmap
        {
            get { return _bitmap; }

            set
            {
                if (Equals(value, _bitmap)) return;
                _bitmap = value;
                NotifyOfPropertyChange(() => Bitmap);
            }
        }

        public int Size { get; set; }

        public void Handle(LoggedInEvent message)
        {
            _dispatcherTimer.Start();
        }

        public void Handle(DisconnectedEvent message)
        {
            _dispatcherTimer.Stop();
        }

        private async void OnTick(object sender, EventArgs e)
        {
            _dispatcherTimer.Stop();

            var getSnapshotCommandDto = new GetSnapshotAdminCommandDto();
            var response =
                await _connection.DispatchCommandAsync<GetSnapshotAdminCommandResponseDto>(getSnapshotCommandDto, CancellationToken.None);

            if (response.WorldSize != Size)
            {
                Size = response.WorldSize;
                _scale = 2.0f * Size / BitmapSize;
            }

            Bitmap.Clear(Color.FromArgb(0, 255, 255, 255));
            foreach (var blob in response.Blobs.OrderBy(x => x.Type))
            {
                Color blobColor;
                switch (blob.Type)
                {
                    case BlobType.Player:
                        blobColor = Color.FromArgb(200, 0, 0, 255);
                        break;
                    case BlobType.Virus:
                        blobColor = Color.FromArgb(200, 255, 0, 0);
                        break;
                    case BlobType.Food:
                        blobColor = Color.FromArgb(200, 0, 255, 0);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var radius = (int)Math.Max(blob.Radius / _scale, 1);

                var position = PositionToBitmap(blob.Position);
                Bitmap.FillEllipseCentered((int)position.X, (int)position.Y, radius, radius, blobColor);
            }

            _dispatcherTimer.Start();
        }

        private VectorDto PositionToBitmap(VectorDto position)
        {
            return new VectorDto { X = (position.X + Size) / _scale, Y = (position.Y + Size) / _scale };
        }
    }
}