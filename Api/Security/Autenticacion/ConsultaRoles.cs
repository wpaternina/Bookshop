using Api.Persistences;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Security.Autenticacion
{
    public class ConsultaRoles
    {
        public class ListaRoles : IRequest<List<IdentityRole>> { }

        public class ManejadorRoles : IRequestHandler<ListaRoles, List<IdentityRole>>
        {
            private readonly BookshopContext _bookshopContext;
            public ManejadorRoles(BookshopContext bookshopContext)
            {
                _bookshopContext = bookshopContext;
            }
            public async Task<List<IdentityRole>> Handle(ListaRoles request, CancellationToken cancellationToken)
            {
                var rol = await _bookshopContext.Roles.ToListAsync();
                return rol;
            }
        }
    }
}
