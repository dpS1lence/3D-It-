namespace BlenderParadise.UnitTests.ServicesTests
{
    public class TestsBase
    {
        protected Mock<IRepository>? repoMock;
        protected Mock<UserManager<ApplicationUser>> userManager;
        protected readonly BlenderParadiseTestDb testDb = new();
        protected IFileService fileService = new FileServiceMock();
        public List<ApplicationUser> users;
        public List<Product> products;
        public List<Category> categories;


        [OneTimeSetUp]
        public void OnTimeSetUp()
        {
            this.userManager = UserManagerMock.MockUserManager<ApplicationUser>(new List<ApplicationUser>()
            {
                testDb.User,
                testDb.User2,
                testDb.UserWithNoUploads
            });

            users = testDb.users;
            products = testDb.products;
            categories = testDb.categories;
        }
    }
}
