using System;
using System.Threading.Tasks;
using DemoFolderStructure.DataAccess.Models;
using DemoFolderStructure.DataAccess.Repositories;

namespace DemoFolderStructure.Services
{
    public class FileSystemService : IFileSystemService
    {
        private readonly IFileSystemRepository _fileSystemRepository;

        public FileSystemService(IFileSystemRepository fileSystemRepository)
        {
            _fileSystemRepository = fileSystemRepository;
        }

        public async Task MoveFolderAsync(Guid folderId, Guid parentId)
        {
            //Run tasks in parallel
            var folderToMove = await _fileSystemRepository.GetFolderAsync(folderId);
            var newParentFolder = await _fileSystemRepository.GetFolderAsync(parentId);
            
            if (folderToMove == null)
                throw new ArgumentException("Folder doesn't exist");
            
            if (newParentFolder == null)
                throw new ArgumentException("Parent folder doesn't exist");
            
            if (folderToMove.ParentFolder != null)
            {
                await UpdateIndexes(folderToMove.ParentFolder);
            }

            folderToMove.ParentFolder = newParentFolder;

            //TODO check MovedFolder indexes already updated so we don't need to repeat
            //This may happen if ParentFolder is still up in hierarchy from new position
            await UpdateIndexes(newParentFolder);

            await _fileSystemRepository.SaveChangesAsync();
        }

        private async Task UpdateIndexes(FolderEntity folder)
        {
            var childFolders = await _fileSystemRepository.GetChildFolders(folder.Id);
            for(var i = 0; i < childFolders.Length; i++)
            {
                childFolders[i].Index = $"{folder.Index}{i + 1}";
                await UpdateIndexes(childFolders[i]);
            }

            var childFiles = await _fileSystemRepository.GetChildFiles(folder.Id);
            for (var i = 0; i < childFiles.Length; i++)
            {
                childFiles[i].Index = $"{folder.Index}{childFolders.Length + i + 1}";
            }
        }
    }
}