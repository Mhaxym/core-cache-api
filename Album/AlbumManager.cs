namespace TodoApi.Cache;

public class AlbumManager : BaseManager
{

    private static readonly string[] ALBUM_NAMES = new[]{
        "Blue Train", "Kind of Blue", "Portrait in Jazz", "Monk's Dream", "KoKo", "Something Else", "Money Jungle", "Saxophone Colossus", "A Night at Birdland", "Empyrean Isles"};
    private static readonly string[] ARTIST_NAMES = new[]{
        "John Coltrane", "Miles Davis", "Thelonious Monk", "Sonny Rollins", "Charlie Parker", "Dizzy Gillespie", "Ornette Coleman", "Bill Evans", "Chet Baker"};


    private Dictionary<string, Album> _ALBUMS { get; set; } = new Dictionary<string, Album>();

    public override void Load()
    {
        foreach (Album album in this._loadAlbums())
        {
            _ALBUMS[album.ID.ToString()] = album;
        }
    }

    public static List<Album> GetAlbums()
    {
        return HYBCacheManager.GetManager<AlbumManager>(typeof(AlbumManager).Name)._ALBUMS.Values.ToList();
    }

    public static Album GetAlbum(long id)
    {
        return HYBCacheManager.GetManager<AlbumManager>(typeof(AlbumManager).Name)._ALBUMS[id.ToString()];
    }

    public static Album SetAlbum(Album album)
    {
        return HYBCacheManager.GetManager<AlbumManager>(typeof(AlbumManager).Name)._ALBUMS[album.ID.ToString()] = album;
    }

    private IEnumerable<Album> _loadAlbums()
    {
        return Enumerable.Range(0, 100000).Select(index => new Album
        {
            ID = index,
            Title = $"{ALBUM_NAMES[Random.Shared.Next(0, ALBUM_NAMES.Length)]} ({index})",
            Artist = ARTIST_NAMES[Random.Shared.Next(0, ARTIST_NAMES.Length)],
            Price = Math.Round(Random.Shared.NextDouble() * 10000) / 100,
        });
    }
}