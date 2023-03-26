using Entities.DbModels;
using FluentAssertions;

namespace EntitiesTests.UnitTests.DbModelTests
{
    [TestClass]
    public class DbPlayerTests
    {
        [TestMethod]
        public void PlaceholderPlayerTests()
        {
            var player = new DbPlayer()
            {
                id = 102,
                name = "Future Tests",
                value = 76.3,
                seasonStartYear = 2022,
                position = "RW",
            };

            player.id.Should().Be(102);
        }
    }
}
