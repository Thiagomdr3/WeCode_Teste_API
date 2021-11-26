using System;

namespace DesafioWebCode.api.Exceptions
{
    /// <summary>
    /// Exception de tentativa de cadastramento de filme repetido
    /// </summary>
    public class FilmeException:Exception
    {
        /// <summary>
        /// Tentativa de cadastrar filme repetido
        /// </summary>
        public FilmeException()
            : base("Esse filme já foi cadastrado")
            { }
    }
}
