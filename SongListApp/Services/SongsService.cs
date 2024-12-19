using SongListApp.Data;
using Microsoft.EntityFrameworkCore;


namespace SongListApp.Services
{
    public class SongsService
    {
        private readonly AppDbContext _context;

        public SongsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Song>> GetSongsAsync()
        {
            return await _context.Songs.ToListAsync();
        }

        public async Task<Song> GetSongByIdAsync(string id)
        {
            return await _context.Songs.FindAsync(id);
        }

        public async Task<Song> InsertSongAsync(Song song)
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            return song;
        }

        public async Task<Song> UpdateSongAsync(string id, Song song)
        {
            var existingSong = await _context.Songs.FindAsync(id);
            if (existingSong == null) return null;

            existingSong.Title = song.Title;
            existingSong.Artist = song.Artist;
            existingSong.Year = song.Year;
            await _context.SaveChangesAsync();
            return existingSong;
        }

        public async Task<Song> DeleteSongAsync(string id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null) return null;

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return song;
        }
    }

}
