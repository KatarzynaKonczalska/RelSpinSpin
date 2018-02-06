using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpinSpin.Interface
{
    public struct Neighbour
    {
        public Button button;
        public Button right;
        public Button left;
        public Button up;
        public Button down;
    }

    public static class NeiborhoodCreator
    {
        public static List<Neighbour> CreateNeighborhood(List<Button> buttons)
        {
            List<Neighbour> neighborhood = new List<Neighbour>();

            foreach (var button in buttons)
            {
                Neighbour neighbour = new Neighbour();
                neighbour.button = button;

                foreach (var element in buttons)
                {
                    Point p = new Point(element.Location.X, element.Location.Y);

                    //prawo
                    if (p.X > button.Location.X &&
                        p.X < button.Location.X + button.Width * 1.5 &&
                        p.Y > button.Location.Y - 0.5 * button.Width &&
                        p.Y < button.Location.Y + 0.5 * button.Width)
                    {
                        neighbour.right = element;
                    }

                    //lewo
                    if (p.X < button.Location.X &&
                        p.X > button.Location.X - button.Width * 1.5 &&
                        p.Y > button.Location.Y - 0.5 * button.Width &&
                        p.Y < button.Location.Y + 0.5 * button.Width)
                    {
                        neighbour.left = element;
                    }

                    //gora
                    if (p.X > button.Location.X - button.Width * 0.25 &&
                        p.X < button.Location.X + button.Width * 0.25 &&
                        p.Y >= button.Location.Y + button.Width &&
                        p.Y < button.Location.Y + 1.3 * button.Width)
                    {
                        neighbour.down = element;
                    }

                    //dol
                    if (p.X > button.Location.X - button.Width * 0.25 &&
                        p.X < button.Location.X + button.Width * 0.25 &&
                        p.Y <= button.Location.Y - button.Width &&
                        p.Y > button.Location.Y - button.Width * 1.25)
                    {
                        neighbour.up = element;
                    }
                }
                neighborhood.Add(neighbour);
            }
            return neighborhood;

        }

    }
}
