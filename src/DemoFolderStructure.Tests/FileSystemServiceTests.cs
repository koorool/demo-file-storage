using System;
using System.Threading.Tasks;
using DemoFolderStructure.Services;
using DemoFolderStructure.DataAccess;
using DemoFolderStructure.DataAccess.Models;
using DemoFolderStructure.DataAccess.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DemoFolderStructure.Tests
{
    public class FileSystemServiceTests
    {
        [Fact]
        public async Task CanMoveFolder()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<FileSystemDbContext>()
                .UseInMemoryDatabase(databaseName: nameof(CanMoveFolder))
                .Options;

            var folderToMoveId = Guid.NewGuid();
            var newParentFolderId = Guid.NewGuid();
            var dbContext = new FileSystemDbContext(options);

            await dbContext.Folders.AddRangeAsync(new FolderEntity
                {
                    Id = folderToMoveId,
                    Name = "New Folder",
                    Index = "1",
                    ParentFolder = null
                },
                new FolderEntity
                {
                    Id = newParentFolderId,
                    Name = "New Folder(2)",
                    Index = "2",
                    ParentFolder = null
                }
            );
            await dbContext.SaveChangesAsync();
                
        
            var repository = new FileSystemRepository(dbContext);
            var sut = new FileSystemService(repository);
            
            //Action
            await sut.MoveFolderAsync(folderToMoveId, newParentFolderId);
            
            //Assert
            var movedFolder = await dbContext.Folders.SingleAsync(x => x.Id == folderToMoveId);
            movedFolder.Index.Should().Be("11");
            movedFolder.ParentFolder.Id.Should().Be(newParentFolderId);
        }
    }
}