using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace EFCore5WebApp.DAL.Tests {
    [TestFixture]
    public class AggregationTest {
        private AppDbContext _context;
        [SetUp]
        public void SetUp() {
            _context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=US-PF1ZJ9QY\\PRDEVENV;Database=EfCore5WebApp;" +
                "Trusted_Connection=True; MultipleActiveResultSets=true; " +
                "User Id=sa; Password=prayal@123;").Options);
        }

        [Test]
        public void CountPersons() {
            int personCount = _context.Persons.Count();
            Assert.AreEqual(2, personCount);
        }

        [Test]
        public void CountPersonsWithFilter() {
            int personCount = _context.Persons.Count(x => x.FirstName == "John" && x.LastName == "Smith");
            Assert.AreEqual(1, personCount);
        }

        [Test]
        public void MinPersonAge() {
            var minPersonAge = _context.Persons.Min(x => x.Age);
            Assert.AreEqual(20, minPersonAge);
        }

        [Test]
        public void MaxPersonAge() {
            var maxPersonAge = _context.Persons.Max(x => x.Age);
            Assert.Greater(maxPersonAge, 20);
        }

        [Test]
        public void AveragePersonAge() {
            var average = _context.Persons.Average(x => x.Age);
            Assert.AreEqual(25, average);
        }

        [Test]
        public void SumPersonAge() {
            var sumAge = _context.Persons.Sum(x => x.Age);
            Assert.AreEqual(50, sumAge);
        }

        [Test]
        public void GroupAddressesByState() {
            var expectedILAddressCount = _context.Addresses.Where(x => x.State == "IL").Count();
            var expectedCAAddressCount = _context.Addresses.Where(x => x.State == "CA").Count();

            var groupedAddress = (from a in _context.Addresses.ToList()
                                  group a by a.State into g
                                  select new { State = g.Key, Address = g.Select(x => x) }).ToList();

            Assert.AreEqual(expectedILAddressCount, 
                groupedAddress.Single(x => x.State == "IL").Address.Count());
            Assert.AreEqual(expectedCAAddressCount, 
                groupedAddress.Single(x => x.State == "CA").Address.Count());
        }

        [Test]
        public void GroupAddressesByStateCount() {
            var expectedILAddressCount = _context.Addresses.Where(s => s.State == "IL").Count();
            var expectedCAAddressCount = _context.Addresses.Where(s => s.State == "CA").Count();

            var groupedAddress = (from a in _context.Addresses
                                  group a by a.State into g
                                  select new { State = g.Key, Count = g.Count()}).ToList();

            Assert.AreEqual(expectedILAddressCount, groupedAddress.Single(s => s.State == "IL").Count);
            Assert.AreEqual(expectedCAAddressCount, groupedAddress.Single(s => s.State == "CA").Count);
        }

        [Test]
        public void MinAgePerState() {
            var expectedILMinAge = 30;
            var expectedCAMinAge = 20;

            var groupedAddresses = from a in _context.Addresses
                                   select new { State = a.State, Age = a.Person.Age } into stateAge
                                   group stateAge by stateAge.State into g
                                   select new { State = g.Key, MinAge = g.Min(a => a.Age) };

            Assert.AreEqual(expectedILMinAge, groupedAddresses.Single(s => s.State == "IL").MinAge);
            Assert.AreEqual(expectedCAMinAge, groupedAddresses.Single(s => s.State == "CA").MinAge);
        }

        [Test]
        public void MaxAgePerState() {
            var expectedILMaxAge = 30;
            var expectedCAMaxAge = 20;
            var groupedAddresses = from a in _context.Addresses
                                   select new { State = a.State, Age = a.Person.Age } into stateAge
                                   group stateAge by stateAge.State into g
                                   select new { State = g.Key, MaxAge = g.Max(a => a.Age) };

            Assert.AreEqual(expectedILMaxAge, groupedAddresses.Single(s => s.State == "IL").MaxAge);
            Assert.AreEqual(expectedCAMaxAge, groupedAddresses.Single(s => s.State == "CA").MaxAge);
        }

        [Test]
        public void AverageAgePerState() {
            var expectedILAge = 30;
            var expectedCAAge = 20;

            var groupedAddresses = from a in _context.Addresses
                                   select new { State = a.State, Age = a.Person.Age } into stateAge
                                   group stateAge by stateAge.State into g
                                   select new { State = g.Key, AverageAge = g.Average(a => a.Age) };

            Assert.AreEqual(expectedILAge, groupedAddresses.Single(s => s.State == "IL").AverageAge);
            Assert.AreEqual(expectedCAAge, groupedAddresses.Single(s => s.State == "CA").AverageAge);
        }

        [Test]
        public void SumAgePerState() {
            var expectedILAge = 60;
            var expectedCAAge = 20;

            var groupedAddresses = from a in _context.Addresses
                                   select new { State = a.State, Age = a.Person.Age } into stateAge
                                   group stateAge by stateAge.State into g
                                   select new { State = g.Key, SumAge = g.Sum(a => a.Age) };

            Assert.AreEqual(expectedILAge, groupedAddresses.Single(s => s.State == "IL").SumAge);
            Assert.AreEqual(expectedCAAge, groupedAddresses.Single(s => s.State == "CA").SumAge);
        }
    }
}