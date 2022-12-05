using BlenderParadise.Data.Models;
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

        [OneTimeSetUp]
        public void OnTimeSetUp()
        {
            this.userManager = UserManagerMock.MockUserManager<ApplicationUser>(new List<ApplicationUser>()
            {
                new ApplicationUser(),
                new ApplicationUser()
            });
        }
    }
}
