using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.ViewModels.Usuario;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Enums;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuarioIndexViewModel>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllList();

            return usuarios.Select(x => new UsuarioIndexViewModel
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Apellido = x.Apellido,
                NombreUsuario = x.NombreUsuario,
                CorreoElectronico = x.CorreoElectronico,
                RolUsuario = x.RolUsuario,
                Estado = x.Estado,
                PartidoPoliticoId = x.PartidoPoliticoId
            }).ToList();
        }

        public async Task<UsuarioEditViewModel?> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario == null)
                return null;

            return new UsuarioEditViewModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                CorreoElectronico = usuario.CorreoElectronico,
                RolUsuario = usuario.RolUsuario,
                Estado = usuario.Estado,
                PartidoPoliticoId = usuario.PartidoPoliticoId
            };
        }

        public async Task<UsuarioActivarViewModel?> GetActivarViewModelAsync(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario == null)
                return null;

            return new UsuarioActivarViewModel
            {
                Id = usuario.Id,
                NombreCompleto = $"{usuario.Nombre} {usuario.Apellido}",
                NombreUsuario = usuario.NombreUsuario,
                CorreoElectronico = usuario.CorreoElectronico,
                Rol = usuario.RolUsuario.ToString()
            };
        }

        public async Task<UsuarioDesactivarViewModel?> GetDesactivarViewModelAsync(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario == null)
                return null;

            return new UsuarioDesactivarViewModel
            {
                Id = usuario.Id,
                NombreCompleto = $"{usuario.Nombre} {usuario.Apellido}",
                NombreUsuario = usuario.NombreUsuario,
                CorreoElectronico = usuario.CorreoElectronico,
                Rol = usuario.RolUsuario.ToString()
            };
        }

        public async Task CreateAsync(UsuarioDTO dto)
        {
            var entity = new Usuario
            {
                Nombre = dto.Nombre.Trim(),
                Apellido = dto.Apellido.Trim(),
                NombreUsuario = dto.NombreUsuario.Trim(),
                CorreoElectronico = dto.CorreoElectronico.Trim(),
                Contrasena = dto.Contrasena,
                RolUsuario = dto.RolUsuario,
                Estado = true,
                PartidoPoliticoId = dto.RolUsuario == RolUsuario.Dirigente
                    ? dto.PartidoPoliticoId
                    : null
            };

            await _usuarioRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(UsuarioDTO dto)
        {
            var usuario = await _usuarioRepository.GetById(dto.Id);

            if (usuario == null)
                return;

            usuario.Nombre = dto.Nombre.Trim();
            usuario.Apellido = dto.Apellido.Trim();
            usuario.NombreUsuario = dto.NombreUsuario.Trim();
            usuario.CorreoElectronico = dto.CorreoElectronico.Trim();
            usuario.RolUsuario = dto.RolUsuario;
            usuario.Estado = dto.Estado;
            usuario.PartidoPoliticoId = dto.RolUsuario == RolUsuario.Dirigente
                ? dto.PartidoPoliticoId
                : null;

            if (!string.IsNullOrWhiteSpace(dto.Contrasena))
            {
                usuario.Contrasena = dto.Contrasena;
            }

            await _usuarioRepository.UpdateAsync(usuario.Id, usuario);
        }

        public async Task ActivarAsync(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario == null)
                return;

            usuario.Estado = true;

            await _usuarioRepository.UpdateAsync(usuario.Id, usuario);
        }

        public async Task DesactivarAsync(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);

            if (usuario == null)
                return;

            if (usuario.RolUsuario == RolUsuario.Administrador)
            {
                var adminsActivos = await _usuarioRepository.CountAdministradoresActivosAsync();

                if (adminsActivos <= 1)
                    return;
            }

            usuario.Estado = false;

            await _usuarioRepository.UpdateAsync(usuario.Id, usuario);
        }

        public async Task<bool> LoginAsync(LoginViewModel vm)
        {
            var usuario = await _usuarioRepository.LoginAsync(
                vm.NombreUsuario.Trim(),
                vm.Contrasena
            );

            if (usuario == null)
                return false;

            if (!usuario.Estado)
                return false;

            if (usuario.RolUsuario == RolUsuario.Dirigente && usuario.PartidoPoliticoId == null)
                return false;

            return true;
        }
    }
}