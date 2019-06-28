using System;
using System.Threading.Tasks;

namespace DemoFolderStructure.Services
{
    public interface IFileSystemService
    {
        Task MoveFolderAsync(Guid folderId, Guid parentId);
    }
}