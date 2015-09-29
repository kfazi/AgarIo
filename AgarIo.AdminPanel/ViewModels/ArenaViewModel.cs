namespace AgarIo.AdminPanel.ViewModels
{
    using System;
    using System.Linq;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using AgarIo.AdminPanel.Events;
    using AgarIo.Contract;

    using Caliburn.Micro;

    public class ArenaViewModel : Screen, IHandle<SnapshotEvent>
    {
        private const int BitmapSize = 1000;

        private WriteableBitmap _bitmap;

        private float _scale;

        public ArenaViewModel(IEventAggregator eventAggregator)
        {
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

        public void Handle(SnapshotEvent snapshotEvent)
        {
            var snapshot = snapshotEvent.Snapshot;

            if (snapshot.WorldSize != Size)
            {
                Size = snapshot.WorldSize;
                _scale = 2.0f * Size / BitmapSize;
            }

            Bitmap.Clear(Color.FromArgb(0, 255, 255, 255));
            foreach (var blob in snapshot.Blobs.OrderBy(x => x.Type))
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
        }

        private VectorDto PositionToBitmap(VectorDto position)
        {
            return new VectorDto { X = (position.X + Size) / _scale, Y = (position.Y + Size) / _scale };
        }
    }
}