using System.Threading.Tasks;
using System;
using MusicalQuiz.Main.Modules.Infrastructure.Dependencies;
using MusicalQuiz.Main.Modules.Playlists.Model.Repository;

namespace MusicalQuiz.Main.Modules.Playlists.Api;

[DefaultTransientImplementation]
public class PlaylistsApi : IPlaylistsApi
{
    private readonly IPlaylistsModelMapper _mapper;
    private readonly IPlaylistsRepository _repository;
    private readonly IPlaylistsContractReconstructionFactory _factory;

    public PlaylistsApi(IPlaylistsModelMapper mapper, IPlaylistsRepository repository, IPlaylistsContractReconstructionFactory factory)
    {
        _mapper = mapper;
        _repository = repository;
        _factory = factory;
    }

    public async Task<Guid> Create(PlaylistContract contract)
    {
        var playlist = _mapper.Map(contract);
        await _repository.Create(playlist);
        return playlist.Id;
    }

    public async Task<PlaylistContract> Get(Guid id)
    {
        var playlist = await _repository.Get(id);
        return _factory.Create(playlist);
    }
}