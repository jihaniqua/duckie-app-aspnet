using Duckie.Controllers;
using Duckie.Data;
using Duckie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuckieUnitTest
{
    [TestClass]
    public class ChildProfilesTests
    {
        // setup mock db
        private ApplicationDbContext _context;

        // declare controller object globally
        ChildProfilesController controller;

        // list of child profiles
        List<ChildProfile> childProfiles = new List<ChildProfile>();

        [TestInitialize]
        public void TestInitialize()
        {
            // sets up new in memory db
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            // mock db data
            var childProfile = new ChildProfile { ChildProfileId = 123, Name = "Isabella", Birthdate = DateTime.Now };
            _context.ChildProfile.Add(childProfile);

            childProfile = new ChildProfile { ChildProfileId = 456, Name = "Lennon", Birthdate = DateTime.Now };
            _context.ChildProfile.Add(childProfile);

            childProfile = new ChildProfile { ChildProfileId = 789, Name = "Grayson", Birthdate= DateTime.Now };
            _context.ChildProfile.Add(childProfile);

            _context.SaveChanges();

            // create the controller using mock db
            controller = new ChildProfilesController(_context);
        }

        // GET Edit - No Id after Edit => /ChildProfiles/Edit
        [TestMethod]
        public void GetEditNullIdReturnsError()
        {
            // act
            var result = (ViewResult)controller.Edit(null).Result;

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        // GET Edit - With Id but doesn't exist in the ChildProfile table
        [TestMethod]
        public void GetEditNullChildProfileReturnsError()
        {
            // act
            var result = (ViewResult)controller.Edit(12).Result;

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        // GET Edit - Id is valid
        [TestMethod]
        public void EditValidIdReturnsView()
        {
            // arrange
            var validId = 910;
            var validProfile = new ChildProfile { ChildProfileId = validId, Name = "Valid Profile Name", Birthdate = DateTime.Now };
            _context.ChildProfile.Add(validProfile); 
            _context.SaveChanges();

            // act
            var result = (ViewResult)controller.Edit(validId).Result;

            // assert
            Assert.AreEqual(validProfile, result.Model);
        }

        // POST Edit - Id is not the same as ChildProfileId
        [TestMethod]
        public void EditInvalidIdReturnsError()
        {
            // arrange
            var invalidId = 727;
            var invalidProfile = new ChildProfile { ChildProfileId = 360, Name = "Invalid Profile Name", Birthdate = DateTime.Now };
            controller.ModelState.AddModelError("ChildProfileId", "Invalid Profile Id");

            // act
            var result = (ViewResult)controller.Edit(invalidId, invalidProfile).Result;

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        // POST Edit - Id doesn't exist
        [TestMethod]
        public void EditNoProfileReturnsError()
        {
            // arrange
            var noId = 1217;
            var noProfile = new ChildProfile { ChildProfileId = noId, Name = "No Profile Name", Birthdate = DateTime.Now };

            // act
            var result = (ViewResult)controller.Edit(noId, noProfile).Result;

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        // POST Edit  - Model of data is valid and changes are saved
        [TestMethod]
        public void EditValidModelReturnsView()
        {
            // arrange
            var validId = 9;
            var validProfile = new ChildProfile { ChildProfileId = validId, Name = "Valid Profile Name", Birthdate= DateTime.Now };
            _context.ChildProfile.Add(validProfile);
            _context.SaveChanges();

            // act
            var result = (RedirectToActionResult)controller.Edit(validId, validProfile).Result;

            // assert
            Assert.AreEqual("Index", result.ActionName);
            }
    }
}