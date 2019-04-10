using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EconomicGame
{
    public partial class Form1 : Form
    {
        public int tempPeasantWood = 0;
        public int tempPeasantStone = 0;
        public int tempPeasantIron = 0;
        public int tempPeasantFood = 0;

        Dictionary<String, int> Resource = new Dictionary<string, int>()
        {
            {"Wood", 15000},
            {"Stone", 50000},
            {"Iron", 300000},
            {"Food", 800000}
        };

        public int Peasants = 4;
        public int Capacity = 8;

        Dictionary<String, int> BuildingLevel = new Dictionary<string, int>()
        {
            {"Wood", 0},
            {"Stone", 0},
            {"Iron", 0},
            {"Food", 0}
        };

        Dictionary<String, int> WorkingPeasants = new Dictionary<string, int>()
        {
            {"Wood", 0},
            {"Stone", 0},
            {"Iron", 0},
            {"Food", 0}
        };

        Dictionary<String, int> WorkingCapacity = new Dictionary<string, int>()
        {
            {"Wood", 0},
            {"Stone", 0},
            {"Iron", 0},
            {"Food", 0}
        };

        Dictionary<String, int> IncomeRate = new Dictionary<string, int>()
        {
            {"Wood", 0},
            {"Stone", 0},
            {"Iron", 0},
            {"Food", 0},
            {"Peasant", 0}
        };

        Dictionary<String, int> IncomeRateByPeasant = new Dictionary<string, int>()
        {
            {"Wood", 12},
            {"Stone", 8},
            {"Iron", 4},
            {"Food", 100},
        };

        Dictionary<String, int> UpgradeCostWood = new Dictionary<string, int>()
        {
            {"Wood", 20},
            {"Stone", 12},
            {"Iron", 10},
            {"Food", 350}
        };

        Dictionary<String, int> UpgradeCostStone = new Dictionary<string, int>()
        {
            {"Wood", 50},
            {"Stone", 0},
            {"Iron", 16},
            {"Food", 200}
        };

        Dictionary<String, int> UpgradeCostIron = new Dictionary<string, int>()
        {
            {"Wood", 35},
            {"Stone", 15},
            {"Iron", 20},
            {"Food", 500}
        };

        Dictionary<String, int> UpgradeCostFood = new Dictionary<string, int>()
        {
            {"Wood", 25},
            {"Stone", 25},
            {"Iron", 6},
            {"Food", 250}
        };

        public int PalaceCostWood = 1000;
        public int PalaceCostStone = 600;
        public int PalaceCostIron = 400;
        public int PalaceCostFood = 5000;


        public Form1()
        {
            InitializeComponent();
            labelResourceWood.Text = Resource["Wood"].ToString();
            labelResourceStone.Text = Resource["Stone"].ToString();
            labelResourceIron.Text = Resource["Iron"].ToString();
            labelResourceFood.Text = Resource["Food"].ToString();
            labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();

            labelBuildingLevelWood.Text = "Level " + BuildingLevel["Wood"].ToString();
            labelBuildingLevelStone.Text = "Level " + BuildingLevel["Stone"].ToString();
            labelBuildingLevelIron.Text = "Level " + BuildingLevel["Iron"].ToString();
            labelBuildingLevelFood.Text = "Level " + BuildingLevel["Food"].ToString();

            labelPeasantAssignWood.Text =
                WorkingPeasants["Wood"].ToString() + " / " + WorkingCapacity["Wood"].ToString();
            labelPeasantAssignStone.Text =
                WorkingPeasants["Stone"].ToString() + " / " + WorkingCapacity["Stone"].ToString();
            labelPeasantAssignIron.Text =
                WorkingPeasants["Iron"].ToString() + " / " + WorkingCapacity["Iron"].ToString();
            labelPeasantAssignFood.Text =
                WorkingPeasants["Food"].ToString() + " / " + WorkingCapacity["Food"].ToString();

            trackBarWoodPeasantAssign.Maximum = BuildingLevel["Wood"] * 2;
            trackBarStonePeasantAssign.Maximum = BuildingLevel["Stone"] * 2;
            trackBarIronPeasantAssign.Maximum = BuildingLevel["Iron"] * 2;
            trackBarFoodPeasantAssign.Maximum = BuildingLevel["Food"] * 2;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpgradeBuildingWood_Click(object sender, EventArgs e)
        {
            if (Resource["Wood"] >= UpgradeCostWood["Wood"] &&
                Resource["Stone"] >= UpgradeCostWood["Wood"] &&
                Resource["Iron"] >= UpgradeCostWood["Wood"] &&
                Resource["Food"] >= UpgradeCostWood["Wood"])
            {
                IncomeRateByPeasant["Wood"] += 5;
                BuildingLevel["Wood"] += 1;
                labelBuildingLevelWood.Text = "Level " + BuildingLevel["Wood"].ToString();
                trackBarWoodPeasantAssign.Maximum =
                    Math.Min(BuildingLevel["Wood"] * 2, Peasants + trackBarWoodPeasantAssign.Value);
                trackBarWoodPeasantAssign.Value = tempPeasantWood;
                labelPeasantAssignWood.Text =
                    trackBarWoodPeasantAssign.Value + " / " + BuildingLevel["Wood"] * 2;

                Resource["Wood"] -= UpgradeCostWood["Wood"];
                Resource["Stone"] -= UpgradeCostWood["Stone"];
                Resource["Iron"] -= UpgradeCostWood["Iron"];
                Resource["Food"] -= UpgradeCostWood["Food"];

                labelResourceWood.Text = Resource["Wood"].ToString();
                labelResourceStone.Text = Resource["Stone"].ToString();
                labelResourceIron.Text = Resource["Iron"].ToString();
                labelResourceFood.Text = Resource["Food"].ToString();

            }
        }

        private void buttonUpgradeBuildingStone_Click(object sender, EventArgs e)
        {
            if (Resource["Wood"] >= UpgradeCostStone["Wood"] &&
                Resource["Stone"] >= UpgradeCostStone["Stone"] &&
                Resource["Iron"] >= UpgradeCostStone["Iron"] &&
                Resource["Food"] >= UpgradeCostStone["Food"])
            {
                IncomeRateByPeasant["Stone"] += 5;
                BuildingLevel["Stone"] += 1;
                labelBuildingLevelStone.Text = "Level " + BuildingLevel["Stone"].ToString();
                trackBarStonePeasantAssign.Maximum =
                    Math.Min(BuildingLevel["Stone"] * 2, Peasants + trackBarStonePeasantAssign.Value);
                trackBarStonePeasantAssign.Value = tempPeasantStone;
                labelPeasantAssignStone.Text =
                    trackBarStonePeasantAssign.Value + " / " + BuildingLevel["Stone"] * 2;

                Resource["Wood"] -= UpgradeCostStone["Wood"];
                Resource["Stone"] -= UpgradeCostStone["Stone"];
                Resource["Iron"] -= UpgradeCostStone["Iron"];
                Resource["Food"] -= UpgradeCostStone["Food"];

                labelResourceWood.Text = Resource["Wood"].ToString();
                labelResourceStone.Text = Resource["Stone"].ToString();
                labelResourceIron.Text = Resource["Iron"].ToString();
                labelResourceFood.Text = Resource["Food"].ToString();
            }
        }

        private void buttonUpgradeBuildingIron_Click(object sender, EventArgs e)
        {
            if (Resource["Wood"] >= UpgradeCostIron["Wood"] &&
                Resource["Stone"] >= UpgradeCostIron["Stone"] &&
                Resource["Iron"] >= UpgradeCostIron["Iron"] &&
                Resource["Food"] >= UpgradeCostIron["Food"])
            {
                IncomeRateByPeasant["Iron"] += 5;
                BuildingLevel["Iron"] += 1;
                labelBuildingLevelIron.Text = "Level " + BuildingLevel["Iron"].ToString();
                trackBarIronPeasantAssign.Maximum =
                    Math.Min(BuildingLevel["Iron"] * 2, Peasants + trackBarIronPeasantAssign.Value);
                trackBarIronPeasantAssign.Value = tempPeasantIron;
                labelPeasantAssignIron.Text =
                    trackBarIronPeasantAssign.Value + " / " + BuildingLevel["Iron"] * 2;

                Resource["Wood"] -= UpgradeCostIron["Wood"];
                Resource["Stone"] -= UpgradeCostIron["Stone"];
                Resource["Iron"] -= UpgradeCostIron["Iron"];
                Resource["Food"] -= UpgradeCostIron["Food"];

                labelResourceWood.Text = Resource["Wood"].ToString();
                labelResourceStone.Text = Resource["Stone"].ToString();
                labelResourceIron.Text = Resource["Iron"].ToString();
                labelResourceFood.Text = Resource["Food"].ToString();
            }
        }

        private void buttonUpgradeBuildingFood_Click(object sender, EventArgs e)
        {
            if (Resource["Wood"] >= UpgradeCostFood["Wood"] &&
                Resource["Stone"] >= UpgradeCostFood["Stone"] &&
                Resource["Iron"] >= UpgradeCostFood["Iron"] &&
                Resource["Food"] >= UpgradeCostFood["Food"])
            {
                IncomeRateByPeasant["Food"] += 5;
                BuildingLevel["Food"] += 1;
                labelBuildingLevelFood.Text = "Level " + BuildingLevel["Food"].ToString();
                trackBarFoodPeasantAssign.Maximum =
                    Math.Min(BuildingLevel["Food"] * 2, Peasants + trackBarFoodPeasantAssign.Value);
                trackBarFoodPeasantAssign.Value = tempPeasantFood;
                labelPeasantAssignFood.Text =
                    trackBarFoodPeasantAssign.Value + " / " + BuildingLevel["Food"] * 2;

                Resource["Wood"] -= UpgradeCostFood["Wood"];
                Resource["Stone"] -= UpgradeCostFood["Stone"];
                Resource["Iron"] -= UpgradeCostFood["Iron"];
                Resource["Food"] -= UpgradeCostFood["Food"];

                labelResourceWood.Text = Resource["Wood"].ToString();
                labelResourceStone.Text = Resource["Stone"].ToString();
                labelResourceIron.Text = Resource["Iron"].ToString();
                labelResourceFood.Text = Resource["Food"].ToString();
            }
        }

        private void buttonBuyPalace_Click(object sender, EventArgs e)
        {
            if (Resource["Wood"] >= PalaceCostWood &&
                Resource["Stone"] >= PalaceCostStone &&
                Resource["Iron"] >= PalaceCostIron &&
                Resource["Food"] >= PalaceCostFood)
            {

            }
        }

        private void trackBarWoodPeasantAssign_ValueChanged(object sender, EventArgs e)
        {
            if (trackBarWoodPeasantAssign.Value > tempPeasantWood)
            {
                if (Peasants <= 0)
                {
                    trackBarWoodPeasantAssign.Value = tempPeasantWood;
                }
                else
                {
                    Peasants -= (trackBarWoodPeasantAssign.Value - tempPeasantWood);
                    IncomeRate["Wood"] = trackBarWoodPeasantAssign.Value * IncomeRateByPeasant["Wood"];
                    labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();
                    labelPeasantAssignWood.Text =
                        trackBarWoodPeasantAssign.Value + " / " + BuildingLevel["Wood"] * 2;
                }
            }
            else
            {
                Peasants += (tempPeasantWood - trackBarWoodPeasantAssign.Value);
                IncomeRate["Wood"] = trackBarWoodPeasantAssign.Value * IncomeRateByPeasant["Wood"];
                labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();
                labelPeasantAssignWood.Text =
                    trackBarWoodPeasantAssign.Value + " / " + BuildingLevel["Wood"] * 2;
            }
            tempPeasantWood = trackBarWoodPeasantAssign.Value;
        }

        

        private void trackBarStonePeasantAssign_Scroll(object sender, EventArgs e)
        {
            if (trackBarStonePeasantAssign.Value > tempPeasantStone)
            {
                if (Peasants <= 0)
                {
                    trackBarStonePeasantAssign.Value = tempPeasantStone;
                }
                else
                {
                    Peasants -= (trackBarStonePeasantAssign.Value - tempPeasantStone);
                    IncomeRate["Iron"] = trackBarStonePeasantAssign.Value * IncomeRateByPeasant["Stone"];
                    labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();
                    labelPeasantAssignStone.Text =
                        trackBarStonePeasantAssign.Value + " / " + BuildingLevel["Stone"] * 2;
                }
            }
            else
            {
                Peasants += (tempPeasantStone - trackBarStonePeasantAssign.Value);
                IncomeRate["Stone"] = trackBarStonePeasantAssign.Value * IncomeRateByPeasant["Stone"];
                labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();
                labelPeasantAssignStone.Text =
                    trackBarStonePeasantAssign.Value + " / " + BuildingLevel["Stone"] * 2;
            }
            tempPeasantStone = trackBarStonePeasantAssign.Value;
        }

        private void trackBarIronPeasantAssign_Scroll(object sender, EventArgs e)
        {
            if (trackBarIronPeasantAssign.Value > tempPeasantIron)
            {
                if (Peasants <= 0)
                {
                    trackBarIronPeasantAssign.Value = tempPeasantIron;
                }
                else
                {
                    Peasants -= (trackBarIronPeasantAssign.Value - tempPeasantIron);
                    IncomeRate["Iron"] = trackBarIronPeasantAssign.Value * IncomeRateByPeasant["Iron"];
                    labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();
                    labelPeasantAssignIron.Text =
                        trackBarIronPeasantAssign.Value + " / " + BuildingLevel["Iron"] * 2;
                }
            }
            else
            {
                Peasants += (tempPeasantIron - trackBarIronPeasantAssign.Value);
                IncomeRate["Iron"] = trackBarIronPeasantAssign.Value * IncomeRateByPeasant["Iron"];
                labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();
                labelPeasantAssignIron.Text =
                    trackBarIronPeasantAssign.Value + " / " + BuildingLevel["Iron"] * 2;
            }
            tempPeasantIron = trackBarIronPeasantAssign.Value;
        }

        private void trackBarFoodPeasantAssign_Scroll(object sender, EventArgs e)
        {
            if (trackBarFoodPeasantAssign.Value > tempPeasantFood)
            {
                if (Peasants <= 0)
                {
                    trackBarFoodPeasantAssign.Value = tempPeasantFood;
                }
                else
                {
                    Peasants -= (trackBarFoodPeasantAssign.Value - tempPeasantFood);
                    IncomeRate["Food"] = trackBarFoodPeasantAssign.Value * IncomeRateByPeasant["Food"];
                    labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();
                    labelPeasantAssignFood.Text =
                        trackBarFoodPeasantAssign.Value + " / " + BuildingLevel["Food"] * 2;
                }
            }
            else
            {
                Peasants += (tempPeasantFood - trackBarFoodPeasantAssign.Value);
                IncomeRate["Food"] = trackBarFoodPeasantAssign.Value * IncomeRateByPeasant["Food"];
                labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();
                labelPeasantAssignFood.Text =
                    trackBarFoodPeasantAssign.Value + " / " + BuildingLevel["Food"] * 2;
            }
            tempPeasantFood = trackBarFoodPeasantAssign.Value;
        }
    }
}

