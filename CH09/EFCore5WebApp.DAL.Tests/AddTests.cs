using EFCore5WebApp.Core.Entities;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EFCore5WebApp.DAL.Tests {
    [TestFixture]
    public class AddTests {
        private AppDbContext _context;

        [SetUp]
        public void SetUp() {
            _context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=US-PF1ZJ9QY\\PRDEVENV;Database=EfCore5WebApp;Trusted_Connection=True;MultipleActiveResultSets=true; User Id=sa; Password=prayal@123;").Options);
        }

        [Test]
        public void InsertPersonWithAddresses() {
            var record = new Person() { 
                FirstName = "Clarke",
                LastName = "Kent",
                EmailAddress = "clark@daileybugel.com",
                Addresses = new List<Address> { 
                    new Address {
                        AddressLine1 = "1234 Fake Street",
                        AddressLine2 = "Suite 1",
                        City = "Chicago",
                        State = "IL",
                        ZipCode = "60652",
                        Country = "United States"
                    },
                    new Address {
                        AddressLine1 = "555 Waverly Street",
                        AddressLine2 = "APT B2",
                        City = "Mt. Pleasant",
                        State = "MI",
                        ZipCode = "48858",
                        Country = "USA"
                    }
                }
            };
            _context.Persons.Add(record);
            _context.SaveChanges();

            var addedPerson = _context.Persons.Single(x => x.FirstName == "Clarke" && x.LastName == "Kent");
            Assert.Greater(addedPerson.Id, 0);
            Assert.AreEqual(2, addedPerson.Addresses.Count);
            Assert.AreEqual(record.FirstName, addedPerson.FirstName);
            Assert.AreEqual(record.LastName, addedPerson.LastName);
            Assert.AreEqual(record.EmailAddress, addedPerson.EmailAddress);

            for (int i = 0; i < record.Addresses.Count; i++) {
                Assert.AreEqual(record.Addresses[i].AddressLine1, addedPerson.Addresses[i].AddressLine1);
                Assert.AreEqual(record.Addresses[i].AddressLine2, addedPerson.Addresses[i].AddressLine2);
                Assert.AreEqual(record.Addresses[i].City, addedPerson.Addresses[i].City);
                Assert.AreEqual(record.Addresses[i].State, addedPerson.Addresses[i].State);
                Assert.AreEqual(record.Addresses[i].ZipCode, addedPerson.Addresses[i].ZipCode);
                Assert.AreEqual(record.Addresses[i].Country, addedPerson.Addresses[i].Country);
            }
        }

        [TearDown]
        public void TearDown() {
            var person = _context.Persons.Single(x => x.FirstName == "Clarke" && x.LastName == "Kent");
            _context.Persons.Remove(person);
            _context.SaveChanges();
        }

        //public void TestCode() {
        //    using (var context = new AppDbContext()) {
        //        var address = context.Addresses.SingleOrDefault(x => x.Id == 1);
        //        if (address != null) {
        //            address.AddressLine1 = "1234 New Street";
        //            context.SaveChanges();
        //        }
        //    }
        //}
    }
}
