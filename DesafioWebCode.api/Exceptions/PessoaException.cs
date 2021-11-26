using System;

namespace DesafioWebCode.api.Exceptions
{
    /// <summary>
    /// Exception de tentativa de cadastramento de pessoa repetida
    /// </summary>
    public class PessoaException:Exception
    {
        /// <summary>
        /// Tentativa de cadastramento de pessoa repetida
        /// </summary>
        public PessoaException()
            : base("Essa pessoa já está cadastrada.")
            { }
    }
}
