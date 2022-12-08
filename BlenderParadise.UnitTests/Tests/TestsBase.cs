using BlenderParadise.Data.Models;
using BlenderParadise.Tests.Common;
using BlenderParadise.UnitTests.Mocks;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderParadise.UnitTests.Tests
{
    public class TestsBase
    {
        protected Mock<UserManager<ApplicationUser>> userManager;
        private readonly BlenderParadiseTestDb testDb = new();

        [OneTimeSetUp]
        public void OnTimeSetUp()
        {
            this.userManager = UserManagerMock.MockUserManager<ApplicationUser>(new List<ApplicationUser>()
            {
                testDb.User,
                testDb.User2,
                testDb.UserWithNoUploads
            });
        }
    }
}
