using SpinSpin;
using SpinSpin.Operators;
using SpinSpin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinSpin.Interface
{
    public static class OperatorsFromButtonsGenerator
    {
        public static List<int[]> GenerateSymmetricOperators(List<Neighbour> neighbourhood)
        {
            List<int[]> completeOperators = new List<int[]>();
            List<Neighbour> rowDown = new List<Neighbour>();
            Neighbour? nextInRow = new Neighbour();
            List<int> antysymmetricOperator = new List<int>();

            int minX = neighbourhood.OrderBy(x => x.button.Location.X).First().button.Location.X;
            int minY = neighbourhood.OrderBy(x => x.button.Location.Y).First().button.Location.Y;
            int buttonWidth = neighbourhood.First().button.Width;

            Neighbour highestInColumn = neighbourhood.Where(x => x.button.Location.X > minX - buttonWidth * 0.25 &&
                x.button.Location.X < minX + buttonWidth * 0.25 &&
                x.button.Location.Y < minY + buttonWidth * 0.25 &&
                x.button.Location.Y > minY - buttonWidth * 0.25).FirstOrDefault();

            antysymmetricOperator.Add(int.Parse(highestInColumn.button.Text));

            if (highestInColumn.down != null || highestInColumn.right != null)
            {
                nextInRow = neighbourhood.Where(x => Object.ReferenceEquals(x.button, highestInColumn.right)).FirstOrDefault();
            }
            else { return completeOperators; }

            do
            {
                while (nextInRow.Value.button != null)
                {
                    antysymmetricOperator.Add(int.Parse(nextInRow.Value.button.Text));
                    nextInRow = neighbourhood.Where(x => Object.ReferenceEquals(x.button, nextInRow.Value.right)).FirstOrDefault();
                }
                if (antysymmetricOperator.Count > 1)
                {
                    completeOperators.Add(antysymmetricOperator.ToArray());

                }
                rowDown = neighbourhood.Where(x => x.button.Location.Y < highestInColumn.button.Location.Y + 1.5 * buttonWidth &&
                    x.button.Location.Y > highestInColumn.button.Location.Y + 0.85 * buttonWidth).ToList();
                if (rowDown.Any())
                {
                    antysymmetricOperator.Clear();
                    minY = rowDown.Select(x => x.button.Location.Y).OrderBy(x => x).First();
                    highestInColumn = rowDown.Where(x => x.button.Location.Y == minY).First();
                    nextInRow = highestInColumn;
                }
            } while (rowDown.Any());

            //List<int[]> partResult = new List<int[]>();
            ////więcej niż dwa w rzędzie
            //foreach (var operatorr in completeOperators)
            //{
            //    if (operatorr.Length > 2)
            //    {
            //        int first = operatorr[0];
            //        int second = operatorr[1];
            //        int third = operatorr[2];
            //        switch (operatorr.Length)
            //        {
            //            case 3:
            //                partResult.Add(new int[] { first, second });
            //                partResult.Add(new int[] { first, third });
            //                partResult.Add(new int[] { second, third });
            //                partResult.Add(new int[] { first, second, third });
            //                partResult.Add(new int[] { second, first, third });
            //                break;
            //            case 4:

            //                int fourth = operatorr[3];
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        partResult.Add(operatorr);
            //    }
            //}

            //List<int[]> result = new List<int[]>();
            //foreach (var operatorr in partResult)
            //{
            //    if (Permutator.IsOdd(operatorr)) result.Add(Permutator.TransformToEvenOperator(operatorr));
            //    else result.Add(operatorr);
            //}

            //return result;
            return completeOperators;
        }

        public static List<int[]> GenerateAntisymmetricOperators(List<Neighbour> neighbourhood)
        {
            List<int[]> completeOperators = new List<int[]>();
            List<Neighbour> columnOnRight = new List<Neighbour>();
            Neighbour? nextInColumn = new Neighbour();
            List<int> symetricOperator = new List<int>();

            int minX = neighbourhood.OrderBy(x => x.button.Location.X).First().button.Location.X;
            int minY = neighbourhood.OrderBy(x => x.button.Location.Y).First().button.Location.Y;
            int buttonWidth = neighbourhood.First().button.Width;

            Neighbour highestInRow = neighbourhood.Where(x => x.button.Location.X > minX - buttonWidth * 0.25 &&
                x.button.Location.X < minX + buttonWidth * 0.25 &&
                x.button.Location.Y < minY + buttonWidth * 0.25 &&
                x.button.Location.Y > minY - buttonWidth * 0.25).First();
            symetricOperator.Add(int.Parse(highestInRow.button.Text));
            if (highestInRow.down != null || highestInRow.right != null)
            {
                nextInColumn = neighbourhood.Where(x => Object.ReferenceEquals(x.button, highestInRow.down)).FirstOrDefault();
            }
            else { return completeOperators; }

            do
            {
                while (nextInColumn.Value.button != null)
                {
                    symetricOperator.Add(int.Parse(nextInColumn.Value.button.Text));
                    nextInColumn = neighbourhood.Where(x => Object.ReferenceEquals(x.button, nextInColumn.Value.down)).FirstOrDefault();
                }
                if (symetricOperator.Count > 1)
                {
                    completeOperators.Add(symetricOperator.ToArray());
                }
                columnOnRight = neighbourhood.Where(x => x.button.Location.X > highestInRow.button.Location.X + buttonWidth
                    && x.button.Location.X < highestInRow.button.Location.X + buttonWidth * 1.25).ToList();
                if (columnOnRight.Any())
                {
                    symetricOperator.Clear();
                    minX = columnOnRight.Select(x => x.button.Location.X).OrderBy(x => x).First();
                    highestInRow = columnOnRight.Where(x => x.button.Location.X == minX).First();
                    nextInColumn = highestInRow;

                }
            } while (columnOnRight.Any());

            return completeOperators;
        }

    }
}
