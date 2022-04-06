using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;


namespace LevelCalcEnemyLevel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static int incrementHolder = 0;
        static int[,] levelTable = new int[714, 2];
        static int[] levelTableIncrementally = new int[714];
        public MainWindow()
        {
            InitializeComponent();
            CreateTables();
        }

        private void enemyLevelCalcButton_Click(object sender, RoutedEventArgs e)
        {
            decimal recievedRunes = 0;
            try
            {
                recievedRunes = decimal.Parse(runesGainedTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Error: Please enter a number");
            }
            levelTextBlock.Text = FindLevel(recievedRunes);
        }

        private void runeCalcButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch
            {

            }
        }
        private static void CreateTables()
        {
            levelTable[1,0] = 673;
            levelTable[2,0] = 689;
            levelTable[3,0] = 706;
            levelTable[4,0] = 723;
            levelTable[5,0] = 740;
            levelTable[6,0] = 757;
            levelTable[7,0] = 775;
            levelTable[8,0] = 793;
            levelTable[9,0] = 811;
            levelTable[10,0] = 829;
            levelTable[11,0] = 847;
            for (int i = 12; i < 714; i++)
            {

                int currentLevelMath = (int)(0.02 * Math.Pow((i + 1), 3) + 3.06 * Math.Pow((i + 1), 2) + 105.6 * (i + 1) - 895);
                levelTable[i,0] = currentLevelMath;
                Debug.WriteLine($"Player level: {i} runes required: {levelTable[i, 0]}");
            }
            for (int i = 1; i < levelTable.GetLength(0); i++)
            {
                incrementHolder += levelTable[i,0];
                levelTable[i,1] = incrementHolder;
            }
        }
        private static string FindLevel(decimal recievedRunes)
        {
            string levelsFound= "";
            for (int i = 1; i < levelTable.GetLength(0); i++)
            {
                decimal fourPercent = recievedRunes / 4 * 100;
                if (fourPercent < levelTable[i,0] && fourPercent + 50 > levelTable[i,0])
                    levelsFound += $"Player Level: {i}\n";
            }
            if (levelsFound == "")
                levelsFound = "Invalid Rune Level";
            return levelsFound;
        }

    }
}
