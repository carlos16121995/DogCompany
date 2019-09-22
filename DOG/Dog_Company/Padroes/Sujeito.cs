using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dog_Company.Padroes
{
    public interface ISujeito
    {

        void Adicionar(IObserver ob);
        void Remover(IObserver ob);
        List<string> Notificar();
    }
}
