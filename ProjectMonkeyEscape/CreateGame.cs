using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMonkey
{
    class CreateGame
    {
        Grove grove = new Grove(1920, 1080);
        List<Tree> trees = new List<Tree>();
        Dictionary<string, Tree> treeRepository = new Dictionary<string, Tree>();
        List<Monkey> monkeys = new List<Monkey>();
        List<Monkey> monkeyPaths = new List<Monkey>();
        Random random = new Random();
        string[] monkeyNames = new string[] { "Georges", "Jackson", "Samuel", "Emperor", "Diehard", "Poppy", "Jackass", "Lucas", "BumBum", "Boris", "Archibald", "Maartens" };
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

        public CreateGame()
        {
            PopulateGroveWithTrees();
            PopulateTreesWithApes();
            SetMonkeysLoose();
            DrawForrest(grove.groveWidth, grove.groveLength);
        }


        void PopulateGroveWithTrees()
        {
            int numberOfTrees = (grove.groveLength * grove.groveWidth) / 250;
            while (trees.Count < numberOfTrees)
            {
                int x = random.Next(grove.groveWidth);
                int y = random.Next(grove.groveLength);
                Tree tree = new Tree(x, y);
                trees.Add(tree);
            }
            List<int> dupVal = new List<int>();
            for (int i = 0; i < trees.Count; i++)
            {
                string duplicate = $"{ trees[i].treeX}{trees[i].treeY}";
                int number = int.Parse(duplicate);
                dupVal.Add(number);
                if (dupVal.Contains(number))
                {
                    trees.RemoveAt(i);
                }
            }

        }

        void PopulateTreesWithApes()
        {
            for (int i = 0; i < monkeyNames.Length; i++)
            {
                int treePosition = random.Next(0, trees.Count);
                Tree monkeyTree = trees[treePosition];
                trees[treePosition].visited = true;
                Monkey monkey = new Monkey(monkeyTree.treeTag, monkeyTree.treeX, monkeyTree.treeY, monkeyNames[i]);
                monkeys.Add(monkey);
            }
        }

        void SetMonkeysLoose()
        {
            int numberOfTrees = trees.Count;
            double distance = 0;
            bool abord = false;

            foreach (Monkey rogueMonkey in monkeys)
            {
                List<int> indexes = new List<int>();
                while (rogueMonkey.isEscaped == false)
                {
                    Monkey monkeyRecord = new Monkey("", 0, 0, "");
                    double shortestPath = 0;
                    int currentTreeIndex = 0;
                    bool start = true;

                    for (int i = 0; i < numberOfTrees; i++)
                    {
                        distance = CalculateDistance(rogueMonkey.monkeyX, trees[i].treeX, rogueMonkey.monkeyY, trees[i].treeY);

                        if (start)
                        {
                            shortestPath = distance;
                            currentTreeIndex = i;
                            start = false;
                        }
                        else if (distance != 0 && shortestPath > distance && trees[i].visited == false)
                        {
                            shortestPath = distance;
                            currentTreeIndex = i;
                        }

                    }
                    indexes.Add(currentTreeIndex);
                    if (indexes.Count > 1)
                    {
                        int currentIndex = indexes[indexes.Count - 1];
                        int lastIndex = indexes[indexes.Count - 2];
                        if (currentTreeIndex == lastIndex)
                        {
                            Console.WriteLine($"{rogueMonkey.monkeyName} is trapped, awaits firefighters");
                            abord = true;
                            break;
                        }

                    }
                    trees[currentTreeIndex].visited = true;
                    rogueMonkey.monkeyX = trees[currentTreeIndex].treeX;
                    rogueMonkey.monkeyY = trees[currentTreeIndex].treeY;
                    rogueMonkey.treeId = trees[currentTreeIndex].treeTag;
                    monkeyRecord.monkeyX = trees[currentTreeIndex].treeX;
                    monkeyRecord.monkeyY = trees[currentTreeIndex].treeY;
                    monkeyRecord.monkeyName = rogueMonkey.monkeyName;
                    monkeyPaths.Add(monkeyRecord);


                    Console.WriteLine($"{rogueMonkey.monkeyName} moved to X: {trees[currentTreeIndex].treeX} Y: {trees[currentTreeIndex].treeY}");
                    if (shortestPath > rogueMonkey.monkeyX)
                    {
                        rogueMonkey.isEscaped = true;

                    }
                    else if (shortestPath > rogueMonkey.monkeyY)
                    {
                        rogueMonkey.isEscaped = true;

                    }
                    else if (shortestPath > grove.groveWidth - rogueMonkey.monkeyX)
                    {
                        rogueMonkey.isEscaped = true;

                    }
                    else if (shortestPath > grove.groveLength - rogueMonkey.monkeyY)
                    {
                        rogueMonkey.isEscaped = true;
                    }

                    if (rogueMonkey.isEscaped == true)
                    {
                        foreach (Tree branch in trees)
                        {
                            branch.visited = false;
                        }
                        Console.WriteLine($"{rogueMonkey.monkeyName} escaped!");
                    }

                }
                if (abord)
                {
                    continue;
                }
            }

        }

        double CalculateDistance(int x1, int x2, int y1, int y2)
        {
            double distance = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            return distance;
        }

        private Bitmap DrawForrest(int width, int height)
        {
            Random color = new Random();
            Bitmap bitmap = new Bitmap(width, height);
            Pen green = new Pen(Brushes.Green);
            int x1 = 0;
            int x2 = 0;
            int y1 = 0;
            int y2 = 0;

            using (Graphics graph = Graphics.FromImage(bitmap))
            {
                Rectangle rectangle = new Rectangle(0, 0, width, height);
                graph.FillRectangle(Brushes.MintCream, rectangle);
                for (int i = 0; i < trees.Count; i++)
                {
                    graph.DrawEllipse(green, trees[i].treeX - 3, trees[i].treeY - 3, 6, 6);
                    graph.FillEllipse(Brushes.Green, trees[i].treeX - 3, trees[i].treeY - 3, 6, 6);
                }
                for (int i = 0; i < monkeyNames.Length; i++)
                {
                    Pen randomColor = new Pen(Color.FromArgb(color.Next(0, 255), color.Next(0, 255), color.Next(0, 255)),4);
                    int launcher = 1;
                    var ape = monkeyPaths.Where(a => a.monkeyName == monkeyNames[i]);
                    foreach (var primate in ape)
                    {
                        graph.DrawEllipse(randomColor, primate.monkeyX - 3, primate.monkeyY - 3, 6, 6);
    
                        if (launcher == 1)
                        {
                            x1 = primate.monkeyX;
                            y1 = primate.monkeyY;
                            launcher++;
                        }
                        if (launcher == 2)
                        {
                            x2 = primate.monkeyX;
                            y2 = primate.monkeyY;
                            launcher = 2;
                            graph.DrawLine(randomColor, x1, y1, x2, y2);
                            x1 = x2;
                            y1 = y2;
                        }
                    }
                }

            }
            bitmap.Save(Path.Combine(path, grove.GroveTag + "_escapeRoutes.jpg"), ImageFormat.Jpeg);
            return bitmap;
        }
    }
}
