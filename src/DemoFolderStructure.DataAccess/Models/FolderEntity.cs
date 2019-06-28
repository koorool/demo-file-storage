using System;
using System.ComponentModel.DataAnnotations;

namespace DemoFolderStructure.DataAccess.Models
{
    public class FolderEntity
    {
        public Guid Id { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Index { get; set; }
        
        public FolderEntity ParentFolder { get; set; }
    }
}