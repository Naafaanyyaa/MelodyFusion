using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;

namespace MelodyFusion.UnitTests.Base
{
    public class BaseUnitTest
    {
        public static IMapper IMapper { get; private set; }

        static BaseUnitTest()
        {
            MapperConfiguration config = new(cfg => cfg.AddProfile<BLL.Mappings.AutoMapper>());
            IMapper = config.CreateMapper();
        }

        public ILoggerFactory GetMockedLogger(ILogger logger = null)
        {
            Mock<ILoggerFactory> loggerFactoryMock = new();
            loggerFactoryMock.Setup(f => f.CreateLogger(It.IsAny<string>())).Returns(logger ?? new Mock<ILogger>().Object);

            return loggerFactoryMock.Object;
        }

        public void AssertEqualCollections<T>(List<T> expectedCollection, List<T> actualCollection)
        {
            Assert.Equal(expectedCollection.Count, actualCollection.Count);

            int i = 0;
            Assert.Collection(actualCollection,
                actualCollection.Select(actualItem => (Action<T>)(item => Assert.Equal(expectedCollection[i++], item))).ToArray()
            );
        }
    }
}
