using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2019_12_16_Schach
{
    public partial class Fenster : Form
    {
        Game Spiel;
        public Fenster()
        {
            InitializeComponent();
            Spiel_OnNewGame(this, EventArgs.Empty);
        }

        private void Spiel_OnNewGame(object sender, EventArgs e)
        {
            Spiel = new Game();
            Spiel.OnRefresh += Spiel_OnRefresh;
            Spiel.OnNewGame += Spiel_OnNewGame;
        }

        private void Spiel_OnRefresh(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Fenster_Paint(object sender, PaintEventArgs e)
        {
            Spiel.Draw(e.Graphics);
            Application.DoEvents();
        }

        private void Fenster_MouseDown(object sender, MouseEventArgs e)
        {
            Spiel.Hit(e.Location);
        }
    }
}
