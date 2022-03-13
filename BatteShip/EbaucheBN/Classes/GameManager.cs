using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;
using Windows.UI;

namespace EbaucheBN.Classes
{
    public class GameManager
    {
        private AI enemyAI = new AI();

        private Grid UI = new Grid();

        public List<Ship> PlayerShips = new List<Ship>();
        public List<Ship> AIShips = new List<Ship>();

        public bool AItoPlay = new bool();
        private int currentHitCounter = new int();

        public List<Cell> AIShipCellHits = new List<Cell>();
        public List<Cell> PlayerShipCellHits = new List<Cell>();

        public void Initialize()
        {
            AItoPlay = false;
            enemyAI.Initialize();

            PlayerShips.AddRange(MainPage.Instance.allyShips);
            AIShips.AddRange(enemyAI.AIShips);

            UpdateAIShipCellType();
            UpdatePlayerShipCellType();

            InitializeUI();
        }
        private void InitializeUI()
        {
            UI = MainPage.Instance.GetInGameUI();
            UI.RowDefinitions.Add(new RowDefinition());

            for (int rowIndex = 0; rowIndex < 4; rowIndex++)
            {
                UI.RowDefinitions.Add(new RowDefinition());

                Button UIButton = new Button();
                UIButton.Background = new SolidColorBrush(GameDesign.DefaultColor);
                UIButton.Width = 2 * UI.Width / 3;
                UIButton.Height = UI.Height / 6;

                TextBlock UITextBlock = new TextBlock();
                UITextBlock.FontSize = rowIndex % 2 == 0 ? 26 : 32;
                UITextBlock.Foreground = new SolidColorBrush(Colors.Black);

                switch (rowIndex)
                {
                    case 0:
                        UITextBlock.Text = "Player Ship Count";
                        break;
                    case 1:
                        UITextBlock.Text = PlayerShips.Count.ToString();
                        break;
                    case 2:
                        UITextBlock.Text = "AI Ship Count";
                        break;
                    case 3:
                        UITextBlock.Text = AIShips.Count.ToString();
                        break;
                }

                UIButton.Content = UITextBlock;
                UI.Children.Add(UIButton);
                UIButton.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetRow(UIButton, rowIndex + (rowIndex < 2 ? 0 : 1));
            }
        }
        public void Hit(Cell cellHit, bool AIHit)
        {
            cellHit.HitYet = true;

            if (AIHit)
            {
                if (cellHit.typeOfCell == cellType.Ship)
                {
                    AppendPlayerCellHit(cellHit);

                    if (!TestShipSunk(AIHit))
                    {
                        cellHit.cellButton.Background = new SolidColorBrush(GameDesign.ShipHitColor);
                        enemyAI.AddPotentialShipPosition(cellHit);
                    }
                    else
                        enemyAI.UpdatePotentialShipPosition(PlayerShipCellHits);
                }
                else
                    cellHit.cellButton.Background = new SolidColorBrush(GameDesign.WaterColor);
            }
            else
            {
                if (cellHit.typeOfCell == cellType.Ship)
                {
                    AppendAICellHit(cellHit);

                    if (!TestShipSunk(false))
                        cellHit.cellButton.Background = new SolidColorBrush(GameDesign.ShipHitColor);
                }
                else
                    cellHit.cellButton.Background = new SolidColorBrush(GameDesign.WaterColor);
            }

            ManagePlayerTurn();

            if (AItoPlay)
                enemyAI.AIHit(PlayerShipCellHits);
        }
        public bool TestShipSunk(bool AI)
        {
            int hitCount;
            List<Cell> CellsToTest = AI ? PlayerShipCellHits : AIShipCellHits;

            foreach (Ship ship in AI ? PlayerShips : AIShips)
            {
                hitCount = 0;

                foreach (Cell position in ship.Position)
                {
                    if (CellsToTest.Contains(position))
                        hitCount++;
                    else
                        break;
                }
                if (hitCount == ship.Size)
                {
                    SinkShip(ship, AI);
                    return true;
                }
            }
            return false;
        }
        private void SinkShip(Ship shipToSink, bool AI)
        {
            List<Ship> shipToRemove = AI ? PlayerShips : AIShips;
            List<Cell> hitCellsToRemove = AI ? PlayerShipCellHits : AIShipCellHits;

            foreach (Cell position in shipToSink.Position)
            {
                hitCellsToRemove.Remove(position);
                position.cellButton.Background = new SolidColorBrush(GameDesign.ShipSunkColor);
            }

            shipToRemove.Remove(shipToSink);
        }
        private void UpdatePlayerShipCellType()
        {
            foreach (Ship ship in MainPage.Instance.allyShips)
            {
                foreach (Cell cell in ship.Position)
                    cell.typeOfCell = cellType.Ship;
            }
        }
        public void UpdateAIShipCellType()
        {
            foreach (Ship ship in AIShips)
            {
                foreach (Cell cell in ship.Position)
                    cell.typeOfCell = cellType.Ship;
            }
        }
        private void ManagePlayerTurn()
        {
            currentHitCounter++;
            if (currentHitCounter == GameDesign.HitPerTurn)
            {
                currentHitCounter = 0;
                AItoPlay = AItoPlay ? false : true;
            }
        }
        public void AppendPlayerCellHit(Cell cellHit)
        {
            PlayerShipCellHits.Add(cellHit);
        }
        public void AppendAICellHit(Cell cellHit)
        {
            AIShipCellHits.Add(cellHit);
        }
    }
}
