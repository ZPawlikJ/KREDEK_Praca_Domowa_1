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
        Dictionary<String,int> Resource = new Dictionary<string, int>()
        {
            {"Wood",15},
            {"Stone",5},
            {"Iron",3},
            {"Food",8}
        };

        public int Peasants = 1;
        public int Capacity = 8;

        Dictionary<String, int> BuildingLevel = new Dictionary<string, int>()
        {
            {"Wood",0},
            {"Stone",0},
            {"Iron",0},
            {"Food",0}
        };

        Dictionary<String, int> WorkingPeasants = new Dictionary<string, int>()
        {
            {"Wood",0},
            {"Stone",0},
            {"Iron",0},
            {"Food",0}
        };

        Dictionary<String, int> WorkingCapacity = new Dictionary<string, int>()
        {
            {"Wood",0},
            {"Stone",0},
            {"Iron",0},
            {"Food",0}
        };

        Dictionary<String, int> IncomeRate = new Dictionary<string, int>()
        {
            {"Wood",0},
            {"Stone",0},
            {"Iron",0},
            {"Food",0},
            {"Peasant",0}
        };

        Dictionary<String, int> IncomeRateByPeasant = new Dictionary<string, int>()
        {
            {"Wood",0},
            {"Stone",0},
            {"Iron",0},
            {"Food",0},
        };

        Dictionary<String, int> UpgradeCostWood = new Dictionary<string, int>()
        {
            {"Wood",15},
            {"Stone",5},
            {"Iron",3},
            {"Food",8}
        };
        Dictionary<String, int> UpgradeCostStone = new Dictionary<string, int>()
        {
            {"Wood",0},
            {"Stone",0},
            {"Iron",0},
            {"Food",0}
        };
        Dictionary<String, int> UpgradeCostIron = new Dictionary<string, int>()
        {
            {"Wood",0},
            {"Stone",0},
            {"Iron",0},
            {"Food",0}
        };
        Dictionary<String, int> UpgradeCostFood = new Dictionary<string, int>()
        {
            {"Wood",0},
            {"Stone",0},
            {"Iron",0},
            {"Food",0}
        };

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

            labelPeasantAssignWood.Text = WorkingPeasants["Wood"].ToString() + " / " + WorkingCapacity["Wood"].ToString();
            labelPeasantAssignStone.Text = WorkingPeasants["Stone"].ToString() + " / " + WorkingCapacity["Stone"].ToString();
            labelPeasantAssignIron.Text = WorkingPeasants["Iron"].ToString() + " / " + WorkingCapacity["Iron"].ToString();
            labelPeasantAssignFood.Text = WorkingPeasants["Food"].ToString() + " / " + WorkingCapacity["Food"].ToString();

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

                Resource["Wood"] -= UpgradeCostWood["Wood"];
                Resource["Stone"] -= UpgradeCostWood["Stone"];
                Resource["Iron"] -= UpgradeCostWood["Iron"];
                Resource["Food"] -= UpgradeCostWood["Food"];
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

                Resource["Wood"] -= UpgradeCostStone["Wood"];
                Resource["Stone"] -= UpgradeCostStone["Stone"];
                Resource["Iron"] -= UpgradeCostStone["Iron"];
                Resource["Food"] -= UpgradeCostStone["Food"];
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

                Resource["Wood"] -= UpgradeCostIron["Wood"];
                Resource["Stone"] -= UpgradeCostIron["Stone"];
                Resource["Iron"] -= UpgradeCostIron["Iron"];
                Resource["Food"] -= UpgradeCostIron["Food"];
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

                Resource["Wood"] -= UpgradeCostFood["Wood"];
                Resource["Stone"] -= UpgradeCostFood["Stone"];
                Resource["Iron"] -= UpgradeCostFood["Iron"];
                Resource["Food"] -= UpgradeCostFood["Food"];
            }
        }

        private void trackBarWoodPeasantAssign_ValueChanged(object sender, EventArgs e)
        {
            //trackBarWoodPeasantAssign.Maximum
        }
    }
}
