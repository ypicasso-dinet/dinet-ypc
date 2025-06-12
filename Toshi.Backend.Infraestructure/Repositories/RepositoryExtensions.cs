using Toshi.Backend.Domain;

namespace Toshi.Backend.Infraestructure.Repositories
{
    public static class RepositoryExtensions
    {
        public static string ToStatus(this bool? value)
        {
            return value == true ? Constants.ACTIVO : Constants.INACTIVO;
        }
    }
}
