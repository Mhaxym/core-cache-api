using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using TodoApi.Cache;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumController : ControllerBase
{

    private readonly IDatabase _database;

    private readonly ILogger<AlbumController> _logger;

    public AlbumController(ILogger<AlbumController> logger, IDatabase database)
    {
        _logger = logger;
        _database = database;
    }

    [HttpGet(Name = "GetAlbums")]
    public IEnumerable<Album> GetAlbums()
    {
        return AlbumManager.GetAlbums();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Album?>> GetAlbum(long id)
    {
        var album = await _database.StringGetAsync("{AlbumManager}/99997");
        if (album != RedisValue.Null && album.HasValue)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Album>(album);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public ActionResult<Album> CreateAlbum(Album album)
    {
        AlbumManager.SetAlbum(album);
        return CreatedAtAction(nameof(GetAlbum), new { id = album.ID }, album);
    }
}

