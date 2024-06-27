using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Danilo_sFood
{
    public interface IProduto
    {
        string GetNome();
        double GetPreco();
        int GetCodigo();
    }
}
