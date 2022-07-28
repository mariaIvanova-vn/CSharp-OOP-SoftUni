namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        [TestCase(new int[0])]
        [TestCase(new[] { 1 })]
        [TestCase(new[] { 2, 20, 32, 5 })]
        public void constructorShouldPassValidData(int[] elements)
        {
            Database database = new Database(elements);

            Assert.AreEqual(elements.Length, database.Count);
        }
        [TestCase(new int[0], new[] { 1, 2, 3 }, 3)]
        public void addWithValidData(int[] constructorElements, int[] elementsToAdd, int expectedElementsCount)
        {
            Database database = new Database(constructorElements);

            for (int i = 0; i < elementsToAdd.Length; i++)
            {
                database.Add(elementsToAdd[i]);
            }

            Assert.AreEqual(expectedElementsCount, database.Count);
        }
        [TestCase(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void addThrowException(int[] elements)
        {
            Database database = new Database(elements);

            Assert.Throws<InvalidOperationException>(() => database.Add(1));
        }
        [TestCase(new int[0], new[] { 1, 2, 3 }, 2, 1)]
        public void validRemove(int[] constructorElements, int[] elementsToAdd, int removeCount, int expectedElementsCount)
        {
            Database database = new Database(constructorElements);

            foreach (var item in elementsToAdd)
            {
                database.Add(item);
            }
            for (int i = 0; i < removeCount; i++)
            {
                database.Remove();
            }
            Assert.AreEqual(expectedElementsCount, database.Count);
        }
        [TestCase(new[] { 1, 2, 3 }, 3)]
        public void inValidRemove(int[] constructorElements, int removeCount)
        {
            Database database = new Database(constructorElements);


            for (int i = 0; i < removeCount; i++)
            {
                database.Remove();
            }
            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }
        [TestCase(new int[] {1, 2, 3 }, new int[] {4,5}, 3, new int[] {1,2})]
        public void FetchWithValidData(int[] constructorElements, int[] elementsToAdd, int removeCount, int[] expectedArray)
        {
            Database database = new Database(constructorElements);
            foreach (var item in elementsToAdd)
            {
                database.Add(item);
            }
            for (int i = 0; i < removeCount; i++)
            {
                database.Remove();
            }
            int[] result=database.Fetch();
            Assert.AreEqual(expectedArray, result);
        }
    }
}
