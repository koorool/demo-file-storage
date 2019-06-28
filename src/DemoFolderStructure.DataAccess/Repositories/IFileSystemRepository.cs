using System;
using System.Threading.Tasks;
using DemoFolderStructure.DataAccess.Models;

namespace DemoFolderStructure.DataAccess.Repositories
{
    public interface IFileSystemRepository
    {
        Task<FolderEntity> GetFolderAsync(Guid folderId);

        Task<FolderEntity[]> GetChildFolders(Guid folderId);

        Task<FileEntity[]> GetChildFiles(Guid folderId);

        Task SaveChangesAsync();
    }
}