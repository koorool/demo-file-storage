using DemoFolderStructure.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoFolderStructure.DataAccess
{
    public class FileSystemDbContext: DbContext
    {
        public FileSystemDbContext(DbContextOptions<FileSystemDbContext> options) : base(options)
        { }

        public DbSet<FileEntity> Files { get; set; }
        
        public DbSet<FolderEntity> Folders { get; set; }
    }
}