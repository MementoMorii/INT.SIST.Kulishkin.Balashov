using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Libraries
{
    internal static class EventBus
    {
        public static event EventHandler<Cell> OnCellBorn;
        public static event EventHandler<Cell> OnCellDie;

        public static void OnBorn(Cell cell)
        {
            OnCellBorn?.Invoke(null, cell);
        }

        public static void OnDie(Cell cell)
        {
            OnCellDie?.Invoke(null, cell);
        }
    }
}
