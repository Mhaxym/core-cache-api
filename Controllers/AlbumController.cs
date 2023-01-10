using Microsoft.AspNetCore.Mvc;
using TodoApi.Cache;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumController : ControllerBase
{
    private readonly ILogger<AlbumController> _logger;

    public AlbumController(ILogger<AlbumController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetAlbums")]
    public IEnumerable<Album> GetAlbums()
    {
        return AlbumManager.GetAlbums();
    }

    [HttpGet("{id}")]
    public ActionResult<Album?> GetAlbum(long id)
    {
        return AlbumManager.GetAlbum(id);
    }

    [HttpPost]
    public ActionResult<Album> CreateAlbum(Album album)
    {
        AlbumManager.SetAlbum(album);
        return CreatedAtAction(nameof(GetAlbum), new { id = album.ID }, album);
    }
}

