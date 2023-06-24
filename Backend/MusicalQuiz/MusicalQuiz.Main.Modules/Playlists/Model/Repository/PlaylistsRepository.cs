using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicalQuiz.Main.Modules.Infrastructure.Dependencies;
using MusicalQuiz.Main.Modules.Playlists.Exceptions;
using MusicalQuiz.Main.Modules.Playlists.Storage;

namespace MusicalQuiz.Main.Modules.Playlists.Model.Repository;

[DefaultTransientImplementation]
public class PlaylistsRepository : IPlaylistsRepository
{
    private readonly PlaylistsStorage _storage;
    private readonly IPlaylistStorageMapper _storageMapper;
    private readonly IPlaylistsReconstructionFactory _reconstructionFactory;

    public PlaylistsRepository(IPlaylistStorageMapper storageMapper, PlaylistsStorage storage, IPlaylistsReconstructionFactory reconstructionFactory)
    {
        _storageMapper = storageMapper;
        _storage = storage;
        _reconstructionFactory = reconstructionFactory;
    }

    public async Task Create(Playlist playlist)
    {
        var storagePlaylist = _storageMapper.Map(playlist);
        var existingTracks = await GetExistingTracks(playlist.Tracks.Select(t => t.Id).ToList());
        if (existingTracks.Count > 0)
        {
            foreach (var track in existingTracks)
            {
                var index = storagePlaylist.Tracks.FindIndex(t => t.Id == track.Id);
                storagePlaylist.Tracks[index] = track;
            }
        }
        await _storage.Playlists.AddAsync(storagePlaylist);
        await _storage.SaveChangesAsync();
    }

    public async Task<Playlist> Get(Guid id)
    {
        var playlist = await Get().FirstOrDefaultAsync(p => p.Id == id);
        if (playlist == null)
        {
            throw new PlaylistNotFoundException(id);
        }

        return _reconstructionFactory.Create(playlist);
    }

    private IQueryable<Storage.Playlist> Get() => _storage.Playlists.Include(p => p.Tracks);

    private async Task<List<Storage.Track>> GetExistingTracks(ICollection<string> tracksIds)
    {
        return await _storage.Tracks.Where(t => tracksIds.Contains(t.Id)).ToListAsync();
    }
}