using System;
using System.Collections.Generic;
using DemoFolderStructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoFolderStructure.Controllers
{
    [Route("api/folders")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly IFileSystemService _fileSystemService;

        public FoldersController(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }
        
        [HttpPost("move")]
        public ActionResult<IEnumerable<string>> MoveFolder([FromBody] Guid folderId, [FromBody] Guid newParentFolderId)
        {
            var result = _fileSystemService.MoveFolderAsync(folderId, newParentFolderId);
            return Ok();
        }
    }
}