using System;
using System.Windows;



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
            int percent = 1;
            decimal recievedRunes = 0;
            try
            {
                recievedRunes = decimal.Parse(runesGainedTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Error: Please enter a number");
            }
            percent = findPercentNeeded();
            levelTextBlock.Text = FindLevel(recievedRunes, percent);
        }

        private void runeCalcButton_Click(object sender, RoutedEventArgs e)
        {
            int currentLevel = 0, desiredLevel = 0, currentRunes = 0, runesRequired = 0;
            try
            {
                currentLevel = int.Parse(currentLevelTextBox.Text);
                desiredLevel = int.Parse(desiredLevelTextBox.Text);
                currentRunes = int.Parse(currentRunesTextBox.Text);
            }
            catch
            {
                MessageBox.Show("Error: Please enter a whole number");
            }
            runesRequired = levelTable[desiredLevel, 1] + currentRunes - levelTable[currentLevel, 1];
            runesToReachLevel.Text = $"Level Desired: {desiredLevel}\n" +
                                     $"Runes required: {runesRequired:n0}";
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
               // Debug.WriteLine($"Player level: {i} runes required: {levelTable[i, 0]}");
            }
            for (int i = 1; i < levelTable.GetLength(0); i++)
            {
                incrementHolder += levelTable[i,0];
                levelTable[i,1] = incrementHolder;
            }
        }
        private static string FindLevel(decimal recievedRunes, int percentChange)
        {

            string levelsFound= "";
            for (int i = 1; i < levelTable.GetLength(0); i++)
            {
                decimal percentageOf = recievedRunes / percentChange * 100; //takes percentage of the runes and finds the value of 100% of them as close as possible
                if (percentageOf < levelTable[i,0] && percentageOf + 50 > levelTable[i,0]) //finds values within range of above value
                    levelsFound += $"Player Level: {i}\n";
            }
            if (levelsFound == "")
                levelsFound = "Invalid Rune Level";
            return levelsFound;
        }

        private int findPercentNeeded()
        {
            try
            {
                if ((bool)hostRadioButton.IsChecked)
                    return 15;

                else if ((bool)coopRadioButton.IsChecked || (bool)hunterRadioButton.IsChecked)
                    return 5;
                else if ((bool)invaderRadioButton.IsChecked)
                    return 4;

                else if ((bool)duelistHostRadioButton.IsChecked)
                    return 2;

                else if ((bool)duelistRadioButton.IsChecked || (bool)duelistCoopRadioButton.IsChecked)
                    return 1;
                else
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("Error: Please choose role you are playing");
                return 1;
            }
        }

    }
}
