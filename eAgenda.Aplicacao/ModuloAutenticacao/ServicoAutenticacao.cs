using eAgenda.Dominio.ModuloAutenticacao;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgenda.Aplicacao.ModuloAutenticacao
{
    public class ServicoAutenticacao : ServicoBase<Usuario, ValidadorUsuario>
    {
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;

        public ServicoAutenticacao(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<Result<Usuario>> RegistrarUsuario(Usuario usuario, string senha)
        {
            Log.Logger.Debug("Tentando registrar usuário... {@u}", usuario);

            Result resultado = Validar(usuario);

            if (resultado.IsFailed)
                return Result.Fail(resultado.Errors);

            try
            {
                var usuarioResult = await userManager.CreateAsync(usuario, senha);

                if(usuarioResult.Succeeded == false)
                {
                    var erros = usuarioResult.Errors.Select(IdentityError => new Error(IdentityError.Description));

                    return Result.Fail(erros);
                }

                Log.Logger.Information("Usuário {TarefaId} inserido com sucesso", usuario.Id);

                return Result.Ok(usuario);
            }
            catch (Exception ex)
            {
                //contextoPersistencia.DesfazerAlteracoes();

                string msgErro = "Falha no sistema ao tentar inserir o Usuário";

                Log.Logger.Error(ex, msgErro + " {UsuarioId} ", usuario.Id);

                return Result.Fail(msgErro);
            }

            return Result.Ok(usuario);
        }

        public async Task<Result<Usuario>> AutenticarUsuario(string email, string senha)
        {
            Log.Logger.Debug("Tentando autenticar usuário... {@u}", email);

            var loginResult = await signInManager.PasswordSignInAsync(email, senha, false, true);

            if (loginResult.Succeeded == false && loginResult.IsLockedOut)
            {
                string msgErro = "Usuario Bloqueado";

                Log.Logger.Warning(msgErro + " {UsuarioEmail} ", email);

                return Result.Fail(msgErro);
            }

            if (loginResult.Succeeded == false)
            {
                string msgErro = "Usuario ou senha incorretos";

                Log.Logger.Warning(msgErro + " {UsuarioEmail} ", email);

                return Result.Fail(msgErro);
            }

            var usuario = await userManager.FindByEmailAsync(email);

            return Result.Ok(usuario);
        }

        public async Task<Result<Usuario>> Sair(string email)
        {
            await signInManager.SignOutAsync();

            Log.Logger.Debug("Sessão do usuário {@email} removida...", email);

            return Result.Ok();
        }
    }
}
