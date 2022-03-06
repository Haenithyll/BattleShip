using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace EbaucheBN.Classes
{
    public class GameManager
    {
        private AI enemyAI = new AI();
        public bool AItoPlay = new bool();

        public void Initialize()
        {
            AItoPlay = false;
            enemyAI.Initialize();
        }

        public void Hit(Cell cellHit)
        {
            AItoPlay = AItoPlay ? false : true;

            cellHit.HitYet = true;
            cellHit.cellButton.Background = new SolidColorBrush(cellHit.typeOfCell == cellType.Water ? GameDesign.WaterColor : GameDesign.ShipHitColor);

            if (AItoPlay)
                enemyAI.RandomHit();
        }

        public void VerifyShipSunk()
        {

        }
    }
}
