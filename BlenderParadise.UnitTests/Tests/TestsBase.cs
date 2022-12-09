using BlenderParadise.Data.Models;
using BlenderParadise.Services.Contracts;
using BlenderParadise.Services;
using BlenderParadise.Tests.Common;
using BlenderParadise.UnitTests.Mocks;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlenderParadise.Repositories.Contracts;

namespace BlenderParadise.UnitTests.Tests
{
    public class TestsBase
    {
        protected Mock<IRepository>? repoMock;
        protected Mock<UserManager<ApplicationUser>> userManager;
        private readonly BlenderParadiseTestDb testDb = new();
        protected IFileService fileService = new LocalFileServiceMock();

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
