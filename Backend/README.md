## BACKEND MAIN SERVER

**Start Docker**

- Path to Docker scripts - `ToolsData\Docker\`.
- Run `environment.yml` (There is an instruction for running in file).

**Postman Collection**

- Path to collections and environments - `ToolsData\Postman`.
- Import files to your own Postman app.

**Users**

- `Add-Migration "Name" -Project MusicalQuiz.Main.Modules -Context UsersStorage -O Users/Storage/Migrations` - To add migration for UsersStorage
- `Update-Database -Project MusicalQuiz.Main.Modules -Context UsersStorage` - To apply migrations for UsersStorage.

**Playlists**

- `Add-Migration "Name" -Project MusicalQuiz.Main.Modules -Context PlaylistsStorage -O Playlists/Storage/Migrations` - To add migration for PlaylistsStorage
- `Update-Database -Project MusicalQuiz.Main.Modules -Context PlaylistsStorage` - To apply migrations for PlaylistsStorage.
