using Microsoft.VisualStudio.TestTools.UnitTesting;
using _2.zad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.zad.Tests
{
    [TestClass]
    public class TodoRepositoryTests
    {
        [TestMethod]
        public void MarkAsCompleted_Test()
        {
            TodoItem item = new TodoItem("one");

            Assert.IsFalse(item.IsCompleted);

            item.MarkAsCompleted();
            Assert.IsTrue(item.IsCompleted);

            Assert.IsFalse(item.MarkAsCompleted());
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void Add_test()
        {
            TodoRepository testRepository = new TodoRepository();
            TodoItem testItem = new TodoItem("one");
            testRepository.Add(testItem);
            testRepository.Add(testItem);
        }

        [TestMethod]
        public void Get_test()
        {
            TodoRepository testRepository = new TodoRepository();
            TodoItem item0 = new TodoItem("one");
            TodoItem item1 = new TodoItem("two");
            TodoItem item2 = new TodoItem("three");
            TodoItem item3;

            testRepository.Add(item0);
            testRepository.Add(item1);
            testRepository.Add(item2);

            item3 = testRepository.Get(item2.Id);

            Assert.IsTrue(item2.Equals(item3));
            Assert.IsFalse(!(item2.Equals(item3)));
        }

        [TestMethod]
        public void Remove_test()
        {
            TodoRepository testRepository = new TodoRepository();
            TodoItem item0 = new TodoItem("one");

            testRepository.Add(item0);
            Assert.IsTrue(testRepository.Remove(item0.Id));
            Assert.IsFalse(testRepository.Remove(item0.Id));
        }


        [TestMethod]
        public void Update_test()
        {
            TodoRepository testRepository = new TodoRepository();
            TodoItem testItem0 = new TodoItem("one");
            testRepository.Add(testItem0);

            testItem0.DateCompleted = DateTime.MaxValue;
            testItem0.DateCompleted = DateTime.MinValue;

            Assert.AreEqual(testItem0, testRepository.Update(testItem0));

            Assert.AreEqual(testRepository.Get(testItem0.Id).DateCompleted, testRepository.Update(testItem0).DateCompleted);
        }


        [TestMethod]
        public void GetAll_test()
        {
            TodoRepository testRepository = new TodoRepository();
            TodoItem testItem0 = new TodoItem("one");
            TodoItem testItem1 = new TodoItem("two");
            testItem0.DateCreated = DateTime.MinValue;
            testItem1.DateCreated = DateTime.MaxValue;

            testRepository.Add(testItem0);
            testRepository.Add(testItem1);

            List<TodoItem> L = new List<TodoItem>();
            L.Add(testItem0);
            L.Add(testItem1);

            Assert.IsTrue(L.OrderByDescending(x => x.DateCreated).ToList().SequenceEqual(testRepository.GetAll()));

        }

        [TestMethod]
        public void GetActive_test()
        {
            TodoRepository testRepository = new TodoRepository();
            TodoItem testItem0 = new TodoItem("one");
            TodoItem testItem1 = new TodoItem("two");
            TodoItem testItem2 = new TodoItem("three");
            TodoItem testItem3 = new TodoItem("four");
            testItem0.MarkAsCompleted();
            testItem1.MarkAsCompleted();
            testItem3.MarkAsCompleted();
            testRepository.Add(testItem0);
            testRepository.Add(testItem1);
            testRepository.Add(testItem2);
            testRepository.Add(testItem3);

            List<TodoItem> L = testRepository.GetActive();

            Assert.IsTrue(L.SequenceEqual(testRepository.GetActive()));
        }

        [TestMethod]
        public void GetCompleted_test()
        {
            TodoRepository testRepository = new TodoRepository();
            TodoItem testItem0 = new TodoItem("one");
            TodoItem testItem1 = new TodoItem("two");
            TodoItem testItem2 = new TodoItem("three");
            TodoItem testItem3 = new TodoItem("four");
            testItem0.MarkAsCompleted();
            testItem1.MarkAsCompleted();
            testItem3.MarkAsCompleted();
            testRepository.Add(testItem0);
            testRepository.Add(testItem1);
            testRepository.Add(testItem2);
            testRepository.Add(testItem3);

            List<TodoItem> L = testRepository.GetCompleted();

            Assert.IsTrue(L.SequenceEqual(testRepository.GetCompleted()));
        }

        [TestMethod]
        public void GetFiltered_test()
        {
            TodoRepository testRepository = new TodoRepository();
            TodoItem testItem0 = new TodoItem("one");
            TodoItem testItem1 = new TodoItem("two");
            TodoItem testItem2 = new TodoItem("three");
            TodoItem testItem3 = new TodoItem("four");
            testItem0.MarkAsCompleted();
            testItem1.MarkAsCompleted();
            testItem3.MarkAsCompleted();
            testRepository.Add(testItem0);
            testRepository.Add(testItem1);
            testRepository.Add(testItem2);
            testRepository.Add(testItem3);

            List<TodoItem> L = testRepository.GetCompleted();

            Assert.IsTrue(L.SequenceEqual(testRepository.GetFiltered(x => x.IsCompleted)));
        }


    }
}