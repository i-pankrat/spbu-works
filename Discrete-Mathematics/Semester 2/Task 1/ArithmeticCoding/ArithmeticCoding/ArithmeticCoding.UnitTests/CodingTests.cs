using NUnit.Framework;

namespace Coding.UnitTests
{
    public class CodingTests
    {
        [Test]
        public void ConstructorTest()
        {
            var element = new ArithmeticCoding();
            Assert.NotNull(element);
        }

        [Test]
        public void EncodeAndDecodeShortMessageTest()
        {
            var element = new ArithmeticCoding();
            string source = "Matmeh is the best place!";
            string encodedMessage = element.Encode(source);
            string decodedMessage = element.Decode(encodedMessage, element.Frequency);
            Assert.IsTrue(source == decodedMessage);
        }

        [Test]
        public void EncodeAndDecodeLongMessageTest()
        {
            var element = new ArithmeticCoding();
            string source = "Ellen very seldom felt that she could afford pity and sympathy for other people, but the boys looked frightened and cold, " +
                            "and her desire to help them was stronger than her reserve. She noticed them staring beyond her to a dish of candy in the room. " +
                            "When she invited them to have a piece, they refused with a shy and elaborate politeness that made her want to take them in her arms. " +
                            "She suggested that they each take a piece of candy home and went into the room for the dish. They followed her.";

            string encodedMessage = element.Encode(source);
            string decodedMessage = element.Decode(encodedMessage, element.Frequency);
            Assert.IsTrue(source == decodedMessage);
        }
    }
}