﻿using EnvisionCreationsNew.Data.Models;
using EnvisionCreationsNew.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnvisionCreationsNew.Services.Contracts
{
    public interface IDownloadService
    {
        Task<List<ViewProductModel>> GetAllAsync();
        Task<DownloadProductModel> GetOneAsync(int? id);

        Task<IActionResult> DownloadAsync(int? id);
    }
}
