namespace AgarIo.Server.Tests
{
    using AutoMapper;

    using NUnit.Framework;

    [TestFixture]
    public class AutoMapperSetupTests
    {
        [Test]
        public void ShouldBeValid()
        {
            var autoMapperSetup = new AutoMapperSetup();

            autoMapperSetup.Run();

            Mapper.AssertConfigurationIsValid();
        }
    }
}
