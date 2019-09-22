using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dog_Company.Padroes
{
    public interface IObserver
    {
        string Cpf { get; }

        //cliente
        string Update(string nomeAnimal, bool adotado);
    }
}
