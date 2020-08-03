using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019_12_16_Schach
{
    class Figuren
    {
        #region Variablen
        internal string Name;
        internal Point Point;
        internal Bitmap Texture;
        internal int Team;
        internal bool Start = true;
        internal readonly int Kastengroesse = Convert.ToInt32(Config.Feldgroesse);
        #endregion

        internal virtual List<Select> GetWhereCanMove(Figuren[] Figuren)
        {
            return null;
        }

        internal bool Add(Figuren[] Figuren, List<Select> Select, Point AddPoint)
        {
            for (int i = 0; i < Figuren.Length; i++)
            {
                if (Figuren[i] != null && AddPoint == Figuren[i].Point)
                {
                    if (Figuren[i].Team != Team)
                    {
                        Select.Add(new Select(Properties.Resources.SelectedRed, AddPoint, Color.Red));
                        return false;
                    }
                    return false;
                }
            }
            Select.Add(new Select(Properties.Resources.SelectedBlue, AddPoint, Color.Blue));
            return true;
        }

        internal void AddLauferMovement(Figuren[] Figuren, List<Select> Select)
        {
            for (int i = 1; i < 8; i++)
            {
                if (!Add(Figuren, Select, new Point(Point.X - Kastengroesse * i, Point.Y - Kastengroesse * i)))
                {
                    break;
                }
            }
            for (int i = 1; i < 8; i++)
            {
                if (!Add(Figuren, Select, new Point(Point.X + Kastengroesse * i, Point.Y - Kastengroesse * i)))
                {
                    break;
                }
            }
            for (int i = 1; i < 8; i++)
            {
                if (!Add(Figuren, Select, new Point(Point.X - Kastengroesse * i, Point.Y + Kastengroesse * i)))
                {
                    break;
                }
            }
            for (int i = 1; i < 8; i++)
            {
                if (!Add(Figuren, Select, new Point(Point.X + Kastengroesse * i, Point.Y + Kastengroesse * i)))
                {
                    break;
                }
            }
        }

        internal void AddTurmMovement(Figuren[] Figuren, List<Select> Select)
        {
            for (int i = 1; i < 9; i++)
            {
                if (!Add(Figuren, Select, new Point(Point.X, Point.Y - Kastengroesse * i)))
                {
                    break;
                }
            }
            for (int i = 1; i < 9; i++)
            {
                if (!Add(Figuren, Select, new Point(Point.X + Kastengroesse * i, Point.Y)))
                {
                    break;
                }
            }
            for (int i = 1; i < 9; i++)
            {
                if (!Add(Figuren, Select, new Point(Point.X, Point.Y + Kastengroesse * i)))
                {
                    break;
                }
            }
            for (int i = 1; i < 9; i++)
            {
                if (!Add(Figuren, Select, new Point(Point.X - Kastengroesse * i, Point.Y)))
                {
                    break;
                }
            }
        }
    }

    #region Figuren
    class Bauer : Figuren
    {
        public Bauer(int Team, Point Point)
        {
            Name = "Bauer";
            this.Team = Team;
            this.Point = Point;
            if (Team == -1)
            {
                Texture = Properties.Resources.Bauer0;
            }
            else
            {
                Texture = Properties.Resources.Bauer1;
            }
        }
        internal override List<Select> GetWhereCanMove(Figuren[] Figuren)
        {
            List<Select> Select = new List<Select>();
            AddBauerMovement(Figuren, Select);
            return Select;
        }

        private void AddBauerMovement(Figuren[] Figuren, List<Select> Select)
        {
            bool OneStep = true;
            bool SecondStep = true;
            for (int i = 0; i < Figuren.Length; i++)
            {
                if (Figuren[i] != null)
                {
                    if (new Point(Point.X, (Point.Y - Kastengroesse * 2 * Team)) == Figuren[i].Point)
                    {
                        SecondStep = false;
                    }
                    if (new Point(Point.X, Point.Y - Kastengroesse * Team) == Figuren[i].Point)
                    {
                        OneStep = false;
                        SecondStep = false;
                    }
                    if (Figuren[i].Team != Team)
                    {
                        if (new Point(Point.X - Kastengroesse, Point.Y - Kastengroesse * Team) == Figuren[i].Point)
                        {
                            Select.Add(new Select(Properties.Resources.SelectedRed, new Point(Point.X - Kastengroesse, Point.Y - Kastengroesse * Team), Color.Red));
                        }
                        if (new Point(Point.X + Kastengroesse, Point.Y - Kastengroesse * Team) == Figuren[i].Point)
                        {
                            Select.Add(new Select(Properties.Resources.SelectedRed, new Point(Point.X + Kastengroesse, Point.Y - Kastengroesse * Team), Color.Red));
                        }
                    }
                }
            }
            if (OneStep)
            {
                Select.Add(new Select(Properties.Resources.SelectedBlue, new Point(Point.X, Point.Y - Kastengroesse * Team), Color.Blue));
            }
            if (Start && SecondStep)
            {
                Select.Add(new Select(Properties.Resources.SelectedBlue, new Point(Point.X, Point.Y - Kastengroesse * 2 * Team), Color.Blue));
            }
        }
    }

    class Laufer : Figuren
    {
        public Laufer(int Team, Point Point)
        {
            Name = "Laufer";
            this.Team = Team;
            this.Point = Point;
            if (Team == -1)
            {
                Texture = Properties.Resources.Lauefer0;
            }
            else
            {
                Texture = Properties.Resources.Lauefer1;
            }
        }
        internal override List<Select> GetWhereCanMove(Figuren[] Figuren)
        {
            List<Select> Select = new List<Select>();
            AddLauferMovement(Figuren, Select);
            return Select;
        }
    }

    class Pferd : Figuren
    {
        public Pferd(int Team, Point Point)
        {
            Name = "Pferd";
            this.Team = Team;
            this.Point = Point;
            if (Team == -1)
            {
                Texture = Properties.Resources.Pferd0;
            }
            else
            {
                Texture = Properties.Resources.Pferd1;
            }
        }
        internal override List<Select> GetWhereCanMove(Figuren[] Figuren)
        {
            List<Select> Select = new List<Select>();
            Add(Figuren, Select, new Point(Point.X + Kastengroesse, Point.Y - 2 * Kastengroesse));
            Add(Figuren, Select, new Point(Point.X + Kastengroesse * 2, Point.Y - Kastengroesse));
            Add(Figuren, Select, new Point(Point.X + Kastengroesse * 2, Point.Y + Kastengroesse));
            Add(Figuren, Select, new Point(Point.X + Kastengroesse, Point.Y + 2 * Kastengroesse));
            Add(Figuren, Select, new Point(Point.X - Kastengroesse, Point.Y + 2 * Kastengroesse));
            Add(Figuren, Select, new Point(Point.X - Kastengroesse * 2, Point.Y - Kastengroesse));
            Add(Figuren, Select, new Point(Point.X - Kastengroesse * 2, Point.Y + Kastengroesse));
            Add(Figuren, Select, new Point(Point.X - Kastengroesse, Point.Y - 2 * Kastengroesse));
            return Select;
        }
    }

    class Turm : Figuren
    {
        public Turm(int Team, Point Point)
        {
            Name = "Turm";
            this.Team = Team;
            this.Point = Point;
            if (Team == -1)
            {
                Texture = Properties.Resources.Turm0;
            }
            else
            {
                Texture = Properties.Resources.Turm1;
            }
        }
        internal override List<Select> GetWhereCanMove(Figuren[] Figuren)
        {
            List<Select> Select = new List<Select>();
            AddTurmMovement(Figuren, Select);
            return Select;
        }
    }

    class Dame : Figuren
    {
        public Dame(int Team, Point Point)
        {
            Name = "Dame";
            this.Team = Team;
            this.Point = Point;
            if (Team == -1)
            {
                Texture = Properties.Resources.Dame0;
            }
            else
            {
                Texture = Properties.Resources.Dame1;
            }
        }
        internal override List<Select> GetWhereCanMove(Figuren[] Figuren)
        {
            List<Select> Select = new List<Select>();
            AddLauferMovement(Figuren, Select);
            AddTurmMovement(Figuren, Select);
            return Select;
        }
    }

    class Koenig : Figuren
    {
        public Koenig(int Team, Point Point)
        {
            Name = "Koenig";
            this.Team = Team;
            this.Point = Point;
            if (Team == -1)
            {
                Texture = Properties.Resources.Koenig0;
            }
            else
            {
                Texture = Properties.Resources.Koenig1;
            }
        }
        internal override List<Select> GetWhereCanMove(Figuren[] Figuren)
        {
            List<Select> Select = new List<Select>();
            for (int i = -1; i < 2; i++)
            {
                Add(Figuren, Select, new Point(Point.X + Kastengroesse * i, Point.Y + Kastengroesse));
            }
            for (int i = -1; i < 2; i++)
            {
                Add(Figuren, Select, new Point(Point.X + Kastengroesse * i, Point.Y - Kastengroesse));
            }
            Add(Figuren, Select, new Point(Point.X - Kastengroesse, Point.Y));
            Add(Figuren, Select, new Point(Point.X + Kastengroesse, Point.Y));
            return Select;
        }
    }
    #endregion
}
