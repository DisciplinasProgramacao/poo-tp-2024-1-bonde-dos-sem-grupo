using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danilo_sFood
{
    public class Cliente
    {
        private string nome;
        private string cpf;

        public Cliente(string nome, string cpf)
        {
            this.nome = nome;
            this.cpf = cpf;
        }

        public string GetNome()
        {
            return nome;
        }

        public string GetCpf()
        {
            return cpf;
        }
    }
}
