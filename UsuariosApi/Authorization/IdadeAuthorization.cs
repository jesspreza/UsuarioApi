using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UsuariosApi.Authorization
{
    public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
        {
            var dataNascimentoClaim = context
                .User.FindFirst(claim =>
                claim.Type == ClaimTypes.DateOfBirth);

            if (dataNascimentoClaim == null)
                return Task.CompletedTask;

            var dataNascimento = Convert.ToDateTime(dataNascimentoClaim.Value);

            var idadeUsuario = DateTime.Today.Year - dataNascimento.Year;

            if (dataNascimento > DateTime.Today.AddDays(-idadeUsuario))
                idadeUsuario--;

            if (idadeUsuario >= requirement.Idade)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
