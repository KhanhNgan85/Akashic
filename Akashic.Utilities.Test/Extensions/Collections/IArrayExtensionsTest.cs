using Akashic.Utilities.Extensions.Collection;
using Akashic.Utilities.Test.Model;
using NUnit.Framework;

namespace Akashic.Utilities.Test.Extensions.Collections
{
    public class IArrayExtensionsTest
    {
        private readonly Fruit[] _array = new Fruit[]
        {
            new Fruit() { Id = 11, Name = "Watermelon", Group = 3 },
            new Fruit() { Id = 12, Name = "Mango", Group = 3 },
            new Fruit() { Id = 13, Name = "Strawberry", Group = 1 },
            new Fruit() { Id = 14, Name = "Orange", Group = 2 },
            new Fruit() { Id = 15, Name = "Apple", Group = 2 },
            new Fruit() { Id = 16, Name = "Lemon", Group = 1 },
            new Fruit() { Id = 17, Name = "Orange", Group = 3 }
        };

        [Test]
        public void RemoveAtTest()
        {
            var array = _array.RemoveAt(2);

            Assert.IsNotNull(array);
            Assert.AreNotSame(_array, array);
            Assert.AreEqual(_array.Length - 1, array.Length);
            Assert.IsFalse(array.Exists(x => x.Name == "Strawberry"));
        }

        #region Test Suite : JoinToString

        [Test]
        public void JoinToStringTest_UserDefineObject()
        {
            var expected = "Watermelon,Mango,Strawberry,Orange,Apple,Lemon,Orange";
            var actual = _array.JoinToString() ?? string.Empty;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void JoinToStringTest_Null()
        {
            string[]? array = null;
            var expected = "";
#pragma warning disable CS8604 // Possible null reference argument.
            var actual = array.JoinToString() ?? string.Empty;
#pragma warning restore CS8604 // Possible null reference argument.

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}