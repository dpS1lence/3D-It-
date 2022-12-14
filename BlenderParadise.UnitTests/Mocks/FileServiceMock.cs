using BlenderParadise.Data.Models;
using BlenderParadise.Repositories.Contracts;
using BlenderParadise.Services.Contracts;
using BlenderParadise.Tests.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlenderParadise.UnitTests.Mocks
{
    public class FileServiceMock : IFileService
    {
        public async Task<bool> DeleteFile(string fileName)
        {
            var file = await GetFile(fileName);

            if (file == null)
            {
                return false;
            }
            else return true;
        }

        public async Task<IActionResult> GetFile(string fileName)
        {
            await Task.Run(() => { });

            return new FileContentResult(Array.Empty<byte>(), "application/pdf");
        }

        public async Task<string> SaveFile(IFormFile fileData)
        {
            await Task.Run(() => { });

            return string.Empty;
        }
    }
}
