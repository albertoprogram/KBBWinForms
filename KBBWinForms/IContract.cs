using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBBWinForms
{
    public interface IContract
    {
        void Compartir(List<(int, string)> values);
    }
}
