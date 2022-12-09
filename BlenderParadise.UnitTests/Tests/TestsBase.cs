namespace BlenderParadise.UnitTests.Tests
{
    public class TestsBase
    {
        protected Mock<IRepository>? repoMock;
        protected Mock<UserManager<ApplicationUser>> userManager;
        protected readonly BlenderParadiseTestDb testDb = new();
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
