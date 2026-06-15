using eVote360Pro.Core.Application.Dtos.User;
using eVote360Pro.Core.Application.DTOs;
using eVote360Pro.Core.Application.DTOs.Email;
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
        private readonly IEmailService _emailService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IEmailService emailService)
        {
            _usuarioRepository = usuarioRepository;
            _emailService = emailService;
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

        public async Task CreateAsync(UsuarioDto dto)
        {
            if (await _usuarioRepository.ExisteNombreUsuarioAsync(dto.NombreUsuario))
            {
                throw new Exception("Ya existe un usuario con este nombre de usuario.");
            }

            if (await _usuarioRepository.ExisteCorreoElectronicoAsync(dto.CorreoElectronico))
            {
                throw new Exception("Ya existe un usuario con este correo electrónico.");
            }

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

            try
            {
                await _emailService.SendAsync(new EmailRequestDTO
                {
                    To = entity.CorreoElectronico,
                    Subject = "Usuario creado - eVote360 Pro",
                    HtmlBody = $@"
                <h2>Bienvenido a eVote360 Pro</h2>
                <p>Hola {entity.Nombre} {entity.Apellido}, tu usuario ha sido creado correctamente.</p>
                <p><strong>Usuario:</strong> {entity.NombreUsuario}</p>
                <p><strong>Rol:</strong> {entity.RolUsuario}</p>
            "
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task UpdateAsync(UsuarioDto dto)
        {
            if (await _usuarioRepository.ExisteNombreUsuarioAsync(dto.NombreUsuario, dto.Id))
            {
                throw new Exception("Ya existe otro usuario con este nombre de usuario.");
            }

            if (await _usuarioRepository.ExisteCorreoElectronicoAsync(dto.CorreoElectronico, dto.Id))
            {
                throw new Exception("Ya existe otro usuario con este correo electrónico.");
            }
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

        public async Task<bool> LoginAsync(LoginDto dto)
        {
            var usuario = await _usuarioRepository.LoginAsync(
                dto.NombreUsuario.Trim(),
                dto.Contrasena
            );

            if (usuario == null)
                return false;

            if (!usuario.Estado)
                return false;

            if (usuario.RolUsuario == RolUsuario.Dirigente &&
                usuario.PartidoPoliticoId == null)
                return false;

            return true;
        }
    }
}