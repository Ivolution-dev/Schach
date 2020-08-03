using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2019_12_16_Schach
{
    class Game
    {
        readonly int Kastengroesse = Convert.ToInt32(Config.Feldgroesse);
        internal Figuren[] Figuren = new Figuren[32];
        private List<Select> Select = new List<Select>();
        private List<Select> GegnerSelect = new List<Select>();
        internal int SelectedIndex = -1;
        private int AmZug = 1;
        private int Player = 0;
        readonly Spielfeld Spielfeld = new Spielfeld();

        internal event EventHandler OnRefresh;
        internal event EventHandler OnNewGame;

        public Game()
        {
            StartGame();
        }

        private void StartGame()
        {
            #region Setze Spielfiguren
            #region Bauern
            for (int i = 0; i < 8; i++)
            {
                Figuren[i] = new Bauer(-1, new Point(i * Kastengroesse, Kastengroesse));
            }
            for (int i = 16; i < 24; i++)
            {
                Figuren[i] = new Bauer(1, new Point((i - 16) * Kastengroesse, 6 * Kastengroesse));
            }
            #endregion
            #region Tuerme
            Figuren[8] = new Turm(-1, new Point(0, 0));
            Figuren[9] = new Turm(-1, new Point(Kastengroesse * 7, 0));
            Figuren[24] = new Turm(1, new Point(0, Kastengroesse * 7));
            Figuren[25] = new Turm(1, new Point(Kastengroesse * 7, Kastengroesse * 7));
            #endregion
            #region Pferde
            Figuren[10] = new Pferd(-1, new Point(Kastengroesse, 0));
            Figuren[11] = new Pferd(-1, new Point(Kastengroesse * 6, 0));
            Figuren[26] = new Pferd(1, new Point(Kastengroesse, Kastengroesse * 7));
            Figuren[27] = new Pferd(1, new Point(Kastengroesse * 6, Kastengroesse * 7));
            #endregion
            #region Lauefer
            Figuren[12] = new Laufer(-1, new Point(Kastengroesse * 2, 0));
            Figuren[13] = new Laufer(-1, new Point(Kastengroesse * 5, 0));
            Figuren[28] = new Laufer(1, new Point(Kastengroesse * 2, Kastengroesse * 7));
            Figuren[29] = new Laufer(1, new Point(Kastengroesse * 5, Kastengroesse * 7));
            #endregion
            #region Dame
            Figuren[14] = new Dame(-1, new Point(Kastengroesse * 3, 0));
            Figuren[30] = new Dame(1, new Point(Kastengroesse * 3, Kastengroesse * 7));
            #endregion
            #region Koenig
            Figuren[15] = new Koenig(-1, new Point(Kastengroesse * 4, 0));
            Figuren[31] = new Koenig(1, new Point(Kastengroesse * 4, Kastengroesse * 7));
            #endregion
            #endregion
            OnRefresh?.Invoke(this, EventArgs.Empty);
        }

        internal void Hit(Point Point)
        {
            for (int i = 0; i < Select.Count; i++)
            {
                if (Hitted(Point, Select[i].Point, Kastengroesse, Kastengroesse))
                {
                    Figuren[SelectedIndex].Point = Select[i].Point;
                    Figuren[SelectedIndex].Start = false;
                    AmZug *= -1;
                    for (int l = 0; l < Figuren.Length; l++)
                    {
                        if (Figuren[l] != null && l != SelectedIndex && Figuren[l].Point == Figuren[SelectedIndex].Point)
                        {
                            if (Figuren[l].Name == "Koenig")
                            {
                                Figuren[l] = null;
                                OnRefresh?.Invoke(this, EventArgs.Empty);
                                MessageBox.Show("König wurde geschlagen!");
                                OnNewGame?.Invoke(this, EventArgs.Empty);
                            }
                            Figuren[l] = null;
                            break;
                        }
                    }
                }
            }
            Select.Clear();
            SelectedIndex = -1;
            for (int i = 0; i < Figuren.Length; i++)
            {
                if ((Player == AmZug || Player == 0) && Figuren[i] != null && Hitted(Point, Figuren[i].Point, Kastengroesse, Kastengroesse) && Figuren[i].Team == AmZug)
                {
                    Select = Figuren[i].GetWhereCanMove(Figuren);
                    SelectedIndex = i;
                    OnRefresh?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
            OnRefresh?.Invoke(this, EventArgs.Empty);
        }

        internal void Draw(Graphics iGr)
        {
            DrawGame(iGr);
        }

        private void DrawGame(Graphics iGr)
        {
            Spielfeld.Draw(iGr);
            for (int i = 0; i < Select.Count; i++)
            {
                iGr.DrawImage(Select[i].Texture, Select[i].Point.X, Select[i].Point.Y, Kastengroesse, Kastengroesse);
            }
            for (int i = 0; i < GegnerSelect.Count; i++)
            {
                iGr.DrawImage(GegnerSelect[i].Texture, GegnerSelect[i].Point.X, GegnerSelect[i].Point.Y, Kastengroesse, Kastengroesse);
            }
            for (int i = 0; i < Figuren.Length; i++)
            {
                if (Figuren[i] != null)
                    iGr.DrawImage(Figuren[i].Texture, Figuren[i].Point.X, Figuren[i].Point.Y, Convert.ToInt32(Config.Feldgroesse), Convert.ToInt32(Config.Feldgroesse));
            }
        }

        private bool Hitted(Point Point1, Point Point2, int ObjectWidth, int ObjectHeight)
        {
            if (Point1.X > Point2.X && Point1.X < Point2.X + ObjectWidth && Point1.Y > Point2.Y && Point1.Y < Point2.Y + ObjectHeight)
            {
                return true;
            }
            return false;
        }
    }

    class Select
    {
        internal Point Point;
        internal Bitmap Texture;
        internal Color Color;

        public Select(Bitmap Texture, Point Point, Color Color)
        {
            this.Texture = Texture;
            this.Point = Point;
            this.Color = Color;
        }
    }
}
