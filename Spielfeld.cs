using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019_12_16_Schach
{
    class Spielfeld
    {
        internal int Kastengroesse;

        public Spielfeld()
        {
            Kastengroesse = Convert.ToInt32(Config.Feldgroesse);
        }

        internal void Draw(Graphics iGr)
        {
            iGr.FillRectangle(new SolidBrush(Color.BurlyWood), 0, 0, 8 * Kastengroesse, 8 * Kastengroesse);
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    iGr.FillRectangle(new SolidBrush(Color.White), x * 2 * Kastengroesse, y * 2 * Kastengroesse, Kastengroesse, Kastengroesse);
                    iGr.FillRectangle(new SolidBrush(Color.White), x * 2 * Kastengroesse + Kastengroesse, y * 2 * Kastengroesse + Kastengroesse, Kastengroesse, Kastengroesse);
                }
            }
        }
    }
}
