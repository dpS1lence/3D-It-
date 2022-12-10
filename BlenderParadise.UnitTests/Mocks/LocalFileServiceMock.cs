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
    public class LocalFileServiceMock : IFileService
    {
        public bool DeleteFile(string fileName)
        {
            string filePath = GetPath(fileName);

            if (File.Exists(filePath))
            {

            }
            else return false;

            return true;
        }

        public string GetPath(string fileName)
        {
            return "F:\\3_DavidDocuments\\Work\\SOFT-UNI\\ASP.NET Project for exam\\GitHub\\3D-It-\\BlenderParadise\\wwwroot\\databaseFiles\\374970cf-e643-497a-b77b-0b38bcf8ef1b_Hoe.blend";
        }

        public async Task<string> SaveFile(IFormFile fileData)
        {
            await Task.Run(() => { });

            return "a";
        }
    }
}
