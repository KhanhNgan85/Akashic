using Akashic.Utilities.Extensions.Collection;
using Akashic.Utilities.Test.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Akashic.Utilities.Test.Extensions.Collections
{
    public class IEnumerableExtensionsTest
    {
        private readonly IEnumerable<Fruit> _list = new List<Fruit>
        {
            new Fruit() { Id = 11, Name = "Watermelon", Group = 3 },
            new Fruit() { Id = 12, Name = "Mango", Group = 3 },
            new Fruit() { Id = 13, Name = "Strawberry", Group = 1 },
            new Fruit() { Id = 14, Name = "Orange", Group = 2 },
            new Fruit() { Id = 15, Name = "Apple", Group = 2 },
            new Fruit() { Id = 16, Name = "Lemon", Group = 1 },
            new Fruit() { Id = 17, Name = "Orange", Group = 3 }
        };

        //

        [Test]
        public void PagingTest()
        {
            var pageSize = 2;
            var page1 = _list.Page(1, pageSize);
            var page2 = _list.Page(2, pageSize);

            Assert.IsNotNull(page1);
            Assert.IsNotNull(page1);
            Assert.AreEqual(pageSize, page1.Count());
            Assert.AreEqual(pageSize, page2.Count());
            Assert.IsNotEmpty(page1.ElementAt(1).Name);
            Assert.IsNotEmpty(page2.ElementAt(1).Name);
            Assert.AreEqual("Mango", page1.ElementAt(1).Name);
            Assert.AreEqual("Strawberry", page2.ElementAt(0).Name);
        }

        [Test]
        [Obsolete]
        public void DistinctWithTest()
        {
            var uniqueCollection = _list.DistinctWith(x => x.Name).ToList();
            Assert.IsNotNull(uniqueCollection);
            Assert.AreEqual(6, uniqueCollection.Count());
        }

        [Test]
        public void CopyToDataTableTest()
        {
            var _table = new DataTable();
            _table.Columns.Add("Id", typeof(int));
            _table.Columns.Add("Name", typeof(string));
            _table.Columns.Add("Group", typeof(int));
            _table.Rows.Add(11, "Watermelon", 3);
            _table.Rows.Add(12, "Mango", 3);
            _table.Rows.Add(13, "Strawberry", 1);
            _table.Rows.Add(14, "Orange", 2);
            _table.Rows.Add(15, "Apple", 2);
            _table.Rows.Add(16, "Lemon", 1);
            _table.Rows.Add(17, "Orange", 3);

            var expected = _table;
            var actual = _list.ToDataTable();

            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);
            Assert.AreNotSame(expected, actual);
            Assert.IsTrue(TestHelpers.IsTableSame(expected, actual));
        }

        #region Test Suite : IndexOf

        [Test]
        public void IndexOfTest()
        {
            var item = _list.IndexOf(new Fruit() { Id = 14, Name = "Orange", Group = 2 });

            Assert.IsNotNull(item);
            Assert.AreEqual(3, item);
        }

        [Test]
        public void IndexByPredicateTest()
        {
            var item = _list.IndexOf(x => x.Name == "Orange");

            Assert.IsNotNull(item);
            Assert.AreEqual(3, item);
        }

        [Test]
        public void IndexOfNullTest()
        {
            var item = _list.IndexOf(x => x.Name == "Blueberry");

            Assert.IsNotNull(item);
            Assert.AreEqual(-1, item);
        }

        #endregion

        #region Test Suite : JoinToString

        [Test]
        public void JoinToStringTest_UserDefineObject()
        {
            var expected = "Watermelon,Mango,Strawberry,Orange,Apple,Lemon,Orange";
            var actual = _list.JoinToString(); //Lưu ý : phải override ToString()

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void JoinToStringTest_Int()
        {
            var list = new List<int>{11, 12, 13, 14};
            var expected = "11,12,13,14";
            var actual = list.JoinToString();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void JoinToStringTest_Null()
        {
            var list = new List<string>();
            var expected = "";
#pragma warning disable CS8604 // Possible null reference argument.
            var actual = list.JoinToString() ?? string.Empty;
#pragma warning restore CS8604 // Possible null reference argument.

            Assert.AreEqual(expected, actual);
        }

        #endregion

        [Test]
        public void ExistsTest()
        {
            Assert.IsTrue(_list.Exists(x => x.Name == "Orange"));
            Assert.IsFalse(_list.Exists(x => x.Name == "Grape"));
        }

        [Test]
        public void GetRandomTest()
        {
            var items = _list.GetRandom(2);

            Assert.IsNotNull(items);
            Assert.AreEqual(2, items.Count());
            Assert.IsTrue(_list.Exists(x => x.Name == items.ElementAt(0).Name));
            Assert.IsTrue(_list.Exists(x => x.Name == items.ElementAt(1).Name));
        }

        [Test]
        public void IsValidTest()
        {
            var validList = _list;
            Assert.IsTrue(validList.IsValid());

            var invalidList = new List<string>();
            Assert.IsFalse(invalidList.IsValid());
        }
    }
}
