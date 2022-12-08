using BlenderParadise.Data.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderParadise.UnitTests.Mocks
{
    public class UserManagerMock
    {

        public static Mock<UserManager<ApplicationUser>> MockUserManager<TUser>(List<ApplicationUser> userList)
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success).Callback<ApplicationUser, string>((x, y) => userList.Add(x));

            mgr.Setup(um => um.FindByNameAsync(
                It.IsAny<string>()))!
            .ReturnsAsync((string username) =>
                userList.FirstOrDefault(u => u.UserName == username));

            mgr.Setup(a => a.Users).Returns(userList.AsQueryable());

            mgr.Setup(um => um.FindByIdAsync(
                It.IsAny<string>()))!
            .ReturnsAsync((string id) =>
                userList.FirstOrDefault(u => u.Id == id));

            mgr.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            return mgr;
        }
    }
}
