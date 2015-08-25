namespace AgarIo.ClientExample
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading;

    using AgarIo.Contract;
    using AgarIo.Contract.AdminCommands;
    using AgarIo.Contract.PlayerCommands;
    using AgarIo.SystemExtension;

    public class Program
    {
        public static void Main(string[] argv)
        {
            //RunAdmin();
            RunPlayer(argv[0]);

            Console.ReadKey(true);
        }

        private static void RunAdmin()
        {
            var tcpClient = new TcpClient();

            tcpClient.Connect("localhost", 8000);

            using (var writer = new StreamWriter(tcpClient.GetStream()))
            {
                writer.AutoFlush = true;
                using (var reader = new StreamReader(tcpClient.GetStream()))
                {
                    HandleAdminConnection(reader, writer);
                }
            }
        }

        private static void RunPlayer(string playerName)
        {
            var tcpClient = new TcpClient();

            tcpClient.Connect("localhost", 8000);

            using (var writer = new StreamWriter(tcpClient.GetStream()))
            {
                writer.AutoFlush = true;
                using (var reader = new StreamReader(tcpClient.GetStream()))
                {
                    HandlePlayerConnection(playerName, reader, writer);
                }
            }
        }

        private static void HandleAdminConnection(TextReader reader, TextWriter writer)
        {
            var loginDto = new LoginDto { Login = "kfazi-admin", Password = "lol", IsAdmin = true };
            var loginJson = loginDto.ToJson();

            Console.WriteLine("Sending login");
            writer.WriteLine(loginJson);

            Console.WriteLine("Awaiting response");
            var loginResponseJson = reader.ReadLine();

            Console.WriteLine(loginResponseJson);

            var worldDto = new DefineWorldAdminCommandDto { Size = 100 };
            var worldJson = worldDto.ToJson();
            writer.WriteLine(worldJson);
            var worldResponseJson = reader.ReadLine();
            Console.WriteLine(worldResponseJson);

            var snapshotDto = new GetSnapshotAdminCommandDto();
            var snapshotJson = snapshotDto.ToJson();
            writer.WriteLine(snapshotJson);
            var snapshotResponseJson = reader.ReadLine();
            Console.WriteLine(snapshotResponseJson);
            var snapshot = reader.ReadLine();
            Console.WriteLine(snapshot);
        }

        private static void HandlePlayerConnection(string playerName, TextReader reader, TextWriter writer)
        {
            var loginDto = new LoginDto { Login = playerName, Password = "lol", IsAdmin = false };
            var loginJson = loginDto.ToJson();

            Console.WriteLine("Sending login");
            writer.WriteLine(loginJson);

            Console.WriteLine("Awaiting response");
            var loginResponseJson = reader.ReadLine();

            Console.WriteLine(loginResponseJson);

            var joinDto = new JoinPlayerCommandDto();
            var joinJson = joinDto.ToJson();
            writer.WriteLine(joinJson);
            var joinResponseJson = reader.ReadLine();
            Console.WriteLine(joinResponseJson);

            Thread.Sleep(100);

            var random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                var getViewDto = new GetViewPlayerCommandDto();
                var getViewJson = getViewDto.ToJson();
                writer.WriteLine(getViewJson);
                var getViewResponseJson = reader.ReadLine();
                var getViewResponseDto = getViewResponseJson.FromJson<GetViewResponseDto>();

                var myBlob = getViewResponseDto.Blobs.FirstOrDefault(x => x.Name == playerName);
                if (myBlob == null)
                {
                    Console.WriteLine("DEAD!!!");
                    return;
                }

                double destinationX = random.Next(-255, 256);
                double destinationY = random.Next(-255, 256);

                Console.WriteLine($"Got {getViewResponseDto.Blobs.Count(x => x.Type == BlobType.Food)} foods");

                var food =
                    getViewResponseDto.Blobs.OrderBy(
                        x => Math.Sqrt(Math.Pow(x.Position.X - myBlob.Position.X, 2) + Math.Pow(x.Position.Y - myBlob.Position.Y, 2)))
                        .FirstOrDefault(x => x.Type == BlobType.Food);
                if (food != null)
                {
                    destinationX = food.Position.X - myBlob.Position.X;
                    destinationY = food.Position.Y - myBlob.Position.Y;
                }

                if (random.Next(100) > 97)
                {
                    //var splitJson = new SplitPlayerCommandDto().ToJson();
                    //writer.WriteLine(splitJson);
                    //var splitResponse = reader.ReadLine();
                    //Console.WriteLine(splitResponse);
                }

                var moveDto = new MovePlayerCommandDto { Dx = destinationX * 1000, Dy = destinationY * 1000 };
                var moveJson = moveDto.ToJson();
                writer.WriteLine(moveJson);
                var moveResponseJson = reader.ReadLine();
                Console.WriteLine(moveResponseJson);

                Thread.Sleep(food != null ? 100 : 1500);
            }
        }
    }
}
