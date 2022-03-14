using System;
using Windows.UI.Xaml.Controls;

namespace EbaucheBN.Classes
{
    //This class' purpose is to display every cell from a BattleShipGrid on a Grid as well as column letters and row indexes; 

    #region GridManagerClassDefinition
    internal class GridManager
    {
        #region Setup
        public void Setup(Grid CurrentGrid, BattleShipGrid CurrentBattleShipGrid, bool Ally)        //Proceeds to setup the Grid.
        {
            bool initialized = false;                                           //Definition of a bool to determine whether or not columns have been already created
                                                                                //Since it is going to go through every column of every row, it is possible to declare 
                                                                                //a new row every time the rowindex increments. However, it is necessary to define every
                                                                                //column only once for the first row as to avoid declaring unnecessary columns.
            
            
            Grid UI = Ally ? MainPage.Instance.GetPlayerGrid() : MainPage.Instance.GetAIGrid();  //Retrieves either the player's grid or the AI's.

            for (int i = 0; i <= GameDesign.GridSizeX; i++)                      //For every row
            {
                CurrentGrid.RowDefinitions.Add(new RowDefinition());                  //Addition of a new row

                for (int j = 0; j <= GameDesign.GridSizeY; j++)                             //For every column             
                {
                    if (!initialized)                                                              //If every column has not been added yet :
                    {
                        CurrentGrid.ColumnDefinitions.Add(new ColumnDefinition());                       //Addition of a new column
                        initialized = j == GameDesign.GridSizeY ? true : false;                          //Initialization complete when the every column has been covered.
                    }
                    if (j == 0 && i != 0)                                                           //If the corresponding index couple is on the left end:
                    {                                                                               //(With the negligence of the topleft corner)

                        Index NewIndex = new Index(i.ToString(), UI);                                   //Creation of a new index corresponding to the row index.
                        CurrentGrid.Children.Add(NewIndex.button);                                      //Addition to the children of the grid.
                        Grid.SetRow(NewIndex.button, i);                                                //Index' row position set to the corresponding row.
                        Grid.SetColumn(NewIndex.button, j);                                             //Index' column position set to the corresponding column.
                    }
                    else if (i == 0)                                                                //Else if the corresponding index couple is on the top end :
                    {                                                                                               
                        if (j != 0)                                                                     //If the corresponding index couple is not on the left end :
                        {
                            char currentChar = Convert.ToChar(j + 64);                                      //Conversion of the index into the corresponding letter.
                            Index NewIndex = new Index(currentChar.ToString(), UI);                         //Creation of a new index corresponding to the column letter.
                            CurrentGrid.Children.Add(NewIndex.button);                                      //Addition to the children of the grid.
                            Grid.SetRow(NewIndex.button, i);                                                //Index' row position set to the corresponding row.
                            Grid.SetColumn(NewIndex.button, j);                                             //Index' column position set to the corresponding column.
                        }
                    }
                    else                                                                            //Else, meaning neither on the top nor left end :
                    {                                                                                           
                        CurrentGrid.Children.Add(CurrentBattleShipGrid.grid[i - 1, j - 1].cellButton);   //Addition of the corresponding BattleShipGrid cell
                                                                                                         //to the children of the grid.
                        Button CurrentButton = CurrentBattleShipGrid.grid[i - 1, j - 1].cellButton;      //Definition of the correponding button as a shortcut.
                        Grid.SetRow(CurrentButton, i);                                                   //Button's row position set to the corresponding row.
                        Grid.SetColumn(CurrentButton, j);                                                //Button's column positon set to the corresponding column.
                    }
                }
            }
        }
        #endregion
    }
    #endregion
}
