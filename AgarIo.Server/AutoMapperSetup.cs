namespace AgarIo.Server
{
    using AgarIo.Contract;
    using AgarIo.Server.Logic;
    using AgarIo.Server.Logic.Blobs;
    using AgarIo.Server.Logic.Physics;

    using AutoMapper;

    internal class AutoMapperSetup
    {
        public void Run()
        {
            Mapper.CreateMap<Player, PlayerDto>();
            Mapper.CreateMap<Blob, BlobDto>()
                .ForMember(d => d.Type, o => o.Ignore())
                .ForMember(d => d.Name, o => o.Ignore())
                .Include<PlayerBlob, BlobDto>()
                .Include<VirusBlob, BlobDto>()
                .Include<FoodBlob, BlobDto>();
            Mapper.CreateMap<PlayerBlob, BlobDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Owner.Name))
                .ForMember(d => d.Type, o => o.UseValue(BlobType.Player));
            Mapper.CreateMap<VirusBlob, BlobDto>()
                .ForMember(d => d.Type, o => o.UseValue(BlobType.Virus));
            Mapper.CreateMap<FoodBlob, BlobDto>()
                .ForMember(d => d.Type, o => o.UseValue(BlobType.Food));
            Mapper.CreateMap<Vector, VectorDto>();
            Mapper.CreateMap<WorldSettings, WorldSettingsDto>();
        }
    }
}