using Assignment3;

// Assignment 3
// Group 1
// April 3rd 2024

namespace Assignment3.Tests
{
    public class SLLTests
    {
        private ILinkedListADT users;

        [SetUp]
        public void Setup()
        {
            users = new SLL();
            users.AddLast(new User(1, "Joe Blow", "jblow@gmail.com", "password"));
            users.AddLast(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"));
            users.AddLast(new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555"));
            users.AddLast(new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999"));
        }

        [TearDown]
        public void TearDown()
        {
            this.users.Clear();
        }

        // Test is to check if the List is empty
        [Test]
        public void IsEmpty()
        {
            Assert.IsFalse(this.users.IsEmpty(), "Confirmed that the list is not empty");
        }

        // Test is to check if prepend works
        [Test]
        public void AddFirst()
        {
            users.AddFirst(new User(5, "Janet", "janny@yahoo.com", "janisgreat"));
            //Checking if it got prepended by using the same User value at index 0 because it got added to the beginning
            Assert.AreEqual(new User(5, "Janet", "janny@yahoo.com", "janisgreat"), users.GetValue(0));
        }

        // Test is to check if append works
        [Test]
        public void AddLast()
        {
            //Checking if it got appended by using the same User value at index 4 because it got added to the end
            users.AddLast(new User(6, "Henry", "hurrayhen@shaw.ca", "ohhenry"));
            Assert.AreEqual(new User(6, "Henry", "hurrayhen@shaw.ca", "ohhenry"), users.GetValue(4));
        }


        // Test to check if Insert works as spcified value
        [Test]
        public void Add()
        {
            users.Add(new User(7, "Rupert", "rupee@outlook.com", "linktothepast"), 1);
            //Checking the index 1 changed to the specified User Value
            Assert.AreEqual(new User(7, "Rupert", "rupee@outlook.com", "linktothepast"), users.GetValue(1));
        }

        // Test to check if Replace works at specified value
        [Test]
        public void Replace()
        {
            users.Replace(new User(8, "Jolene", "jolene@dolly.com", "flaminglocks"), 1);
            //Checking if index 1 changed to specified value
            Assert.AreEqual(new User(8, "Jolene", "jolene@dolly.com", "flaminglocks"), users.GetValue(1));
        }

        // Test to check if the beginning changed
        [Test]
        public void RemoveFirst()
        {
            users.RemoveFirst();
            //Checking if index 0 changed due to 0 being the first and confirming it got changed
            Assert.AreEqual(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"), users.GetValue(0));
        }

        // Test to check if the end changed
        [Test]
        public void RemoveLast()
        {
            users.RemoveLast();
            //Checking if index 3 exists and should give exception
            Assert.Throws<NullReferenceException>(delegate { users.GetValue(3); });
        }

        // Test to check if the middle got removed
        [Test]
        public void Remove()
        {
            users.Remove(1);
            //Checking if index 1, the middle, got removed
            Assert.AreEqual(new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555"), users.GetValue(1));
        }

        // Test to check if the Find method works
        [Test]
        public void Contains()
        {
            //Checking if the specified User value exists
            Assert.IsTrue(users.Contains(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef")));
        }
        [Test]
        public void Count()
        {
            // Arrange: The expected count is 4 since we added 4 users in the Setup method
            int expectedCount = 4;

            // Act: Call the Count method
            int actualCount = users.Count();

            // Assert: Check if the count matches the expected count
            Assert.AreEqual(expectedCount, actualCount);
        }

        // Test to check if sorting method works
        [Test]
        public void SortByName()
        {
            // Arrange: Original order of users
            string[] expectedOrder = { "Colonel Sanders", "Joe Blow", "Joe Schmoe", "Ronald McDonald" };

            // Act: Sort the users by name
            ((SLL)users).SortByName();

            // Assert: Check if the list is sorted correctly
            for (int i = 0; i < expectedOrder.Length; i++)
            {
                Assert.AreEqual(expectedOrder[i], ((SLL)users).GetValue(i).Name);
            }
        }

        // Test to check if the NodesToArray work
        [Test]
        public void ArrayNodes()
        {
            User[] Temp = new User[] { new User(1, "Joe Blow", "jblow@gmail.com", "password"), new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"), new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555"), new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999") };
            User[] main = users.NodesToArray();
            //Checking if array is the same
            Assert.AreEqual(Temp, main);
        }
    }
}