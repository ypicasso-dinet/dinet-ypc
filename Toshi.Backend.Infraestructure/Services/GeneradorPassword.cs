using System.Security.Cryptography;

namespace Toshi.Backend.Infraestructure.Services
{
    public sealed class GeneradorPassword
    {
        public static string GenerarPassword(int longitud)
        {
            if (longitud < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.");

            const string numeros = "0123456789";
            const string mayusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string minusculas = "abcdefghijklmnopqrstuvwxyz";
            const string especiales = "!@#$%^&*()-_=+<>?";

            // Garantizar que la contraseña contenga al menos un carácter de cada categoría
            char[] password = new char[longitud];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();

            password[0] = numeros[GetRandomInt(rng, numeros.Length)];
            password[1] = mayusculas[GetRandomInt(rng, mayusculas.Length)];
            password[2] = minusculas[GetRandomInt(rng, minusculas.Length)];
            password[3] = especiales[GetRandomInt(rng, especiales.Length)];

            // Llenar el resto de la contraseña con caracteres aleatorios de todos los conjuntos combinados
            string todos = numeros + mayusculas + minusculas + especiales;

            for (int i = 4; i < longitud; i++)
            {
                password[i] = todos[GetRandomInt(rng, todos.Length)];
            }

            // Mezclar los caracteres aleatoriamente para evitar que los primeros siempre sean de cada categoría
            return new string(password.OrderBy(_ => GetRandomInt(rng, longitud)).ToArray());
        }

        private static int GetRandomInt(RandomNumberGenerator rng, int max)
        {
            byte[] buffer = new byte[4];
            rng.GetBytes(buffer);
            return BitConverter.ToInt32(buffer, 0) & int.MaxValue % max;
        }
    }
}
