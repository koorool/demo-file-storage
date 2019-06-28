using System;
using System.Linq;
using System.Threading.Tasks;
using DemoFolderStructure.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoFolderStructure.DataAccess.Repositories
{
    public class FileSystemRepository : IFileSystemRepository
    {
        private readonly FileSystemDbContext _dbContext;

        public FileSystemRepository(FileSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<FolderEntity> GetFolderAsync(Guid folderId)
        {
            return _dbContext.Folders.FirstOrDefaultAsync(x => x.Id == folderId);
        }

        public async Task<FolderEntity[]> GetChildFolders(Guid folderId)
        {
            return await _dbContext.Folders
                .Where(x => x.ParentFolder.Id == folderId)
                .ToArrayAsync();
        }

        public async Task<FileEntity[]> GetChildFiles(Guid folderId)
        {
            return await _dbContext.Files
                .Where(x => x.ParentFolder.Id == folderId)
                .ToArrayAsync();
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}