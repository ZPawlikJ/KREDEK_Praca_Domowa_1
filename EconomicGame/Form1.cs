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
        public int tempPeasantWood = 0; //wartość suwaka z wieśniakami przed jego zmianą
        public int tempPeasantStone = 0;
        public int tempPeasantIron = 0;
        public int tempPeasantFood = 0;
        public int tempPeasantSpear = 0;

        public int Spearmen = 3; // ilość włóczników
        public float SpearmenStrength = 1; //jaką wartość ma 1 włócznik, może być modyfikowane (np przez obecność specjalnego budynku)

        public int DefensePoints = 0;
        public int TickCounter = 0;
        public int BanditStrength = 8;

        Dictionary<String, int> Resource = new Dictionary<string, int>()//zasoby, które wykorzystujemy do budowy i funkcjonowania
        {
            {"Wood", 15000},//na potrzeby testowania, ustawiam na bardzo wysokie wartości
            {"Stone", 50000},
            {"Iron", 300000},
            {"Food", 800000}
        };

        Dictionary<String, int> Militaria = new Dictionary<string, int>()//zasoby, które wykorzystujemy do trenowania zbrojnych do obrony przed bandytami
        {
            {"Spear", 15},
        };

        public int Peasants = 4; //wieśniaków wykorzystujemy do pracy w budynkach
        public int Capacity = 8; //nie możemy mieć więcej niż tyle wieśniaków na raz
        public float PeasantIncreasePointsTick = 20; //dodawana wartość do PeasantCurrentIncreasePoints za pomocą timerPeasant
        public float PeasantCurrentIncreasePoints = 0; 
        public float PeasantIncreasePoints = 150; //gdy PeasantCurrentIncreasePoints osiągnie tą wartość, dodajemy wieśniaka, chyba że już nie ma miejsca (Capacity)

        Dictionary<String, int> BuildingLevel = new Dictionary<string, int>()//poziom budynku, im większy, tym więcej wieśniaków zmieści
        {
            {"Wood", 1}, //tartak (Lumber Mill)
            {"Stone", 1}, //kamieniołom (Quarry)
            {"Iron", 1}, //kopalnia żelaza (Iron Mine)
            {"Food", 1} //sad jabłoni (Aplle Orchard)
        };

        Dictionary<String, int> MilitariaLevel = new Dictionary<string, int>()//poziom budynku, im większy, tym więcej wieśniaków zmieści
        {
            {"Spear", 1}, //Grotnik (Spear maker)
        };

        Dictionary<String, int> WorkingPeasants = new Dictionary<string, int>()//ile jest wieśniaków w budynkach
        {
            {"Wood", 0},
            {"Stone", 0},
            {"Iron", 0},
            {"Food", 0},
            {"Spear", 0}
        };

        Dictionary<String, int> WorkingCapacity = new Dictionary<string, int>()//ile może być maksymalnie wieśniaków w budynkach
        {
            {"Wood", 0},
            {"Stone", 0},
            {"Iron", 0},
            {"Food", 0},
            {"Spear", 0}
        };

        Dictionary<String, int> IncomeRate = new Dictionary<string, int>() //tyle zasobów zostanie nam dodanych za każdym tikiem timera
        {
            {"Wood", 0},
            {"Stone", 0},
            {"Iron", 0},
            {"Food", 0},
            {"Peasant", 0},
            {"Spear",0}
        };

        Dictionary<String, int> IncomeRateByPeasant = new Dictionary<string, int>() //każdy pracujący wieśniak może wytwarzać tyle zasobów
        {
            {"Wood", 12},
            {"Stone", 8},
            {"Iron", 4},
            {"Food", 100},
            {"Spear", 1}
        };

        Dictionary<String, int> UpgradeCostWood = new Dictionary<string, int>() //koszt ulepszeń tartaku
        {
            {"Wood", 20},
            {"Stone", 12},
            {"Iron", 10},
            {"Food", 350}
        };

        Dictionary<String, int> UpgradeCostStone = new Dictionary<string, int>() //koszt ulepszeń kamieniołomu
        {
            {"Wood", 50},
            {"Stone", 0},
            {"Iron", 16},
            {"Food", 200}
        };

        Dictionary<String, int> UpgradeCostIron = new Dictionary<string, int>() //koszt ulepszeń kopalni żelaza
        {
            {"Wood", 35},
            {"Stone", 15},
            {"Iron", 20},
            {"Food", 500}
        };

        Dictionary<String, int> UpgradeCostFood = new Dictionary<string, int>() //koszt ulepszeń sadu jabłoni
        {
            {"Wood", 25},
            {"Stone", 25},
            {"Iron", 6},
            {"Food", 250}
        };

        Dictionary<String, int> UpgradeCostSpear = new Dictionary<string, int>() //koszt ulepszeń grotnika
        {
            {"Wood", 250},
            {"Stone", 50},
            {"Iron", 50},
            {"Food", 50}
        };

        public int PalaceCostWood = 1000; //koszt pałacu, gdy go zbudujemy, wygrywamy grę
        public int PalaceCostStone = 600;
        public int PalaceCostIron = 400;
        public int PalaceCostFood = 5000;

        public int PalisadeCostWood = 800;
        public int PalisadeCostIron = 20;

        public Form1()
        {
            InitializeComponent();
            labelResourceWood.Text = Resource["Wood"].ToString(); //przypisywanie wartości do odpowiednich labelek
            labelResourceStone.Text = Resource["Stone"].ToString();
            labelResourceIron.Text = Resource["Iron"].ToString();
            labelResourceFood.Text = Resource["Food"].ToString();
            labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();

            labelResourceSpear.Text = Militaria["Spear"].ToString();

            labelBuildingLevelWood.Text = "Level " + BuildingLevel["Wood"].ToString();
            labelBuildingLevelStone.Text = "Level " + BuildingLevel["Stone"].ToString();
            labelBuildingLevelIron.Text = "Level " + BuildingLevel["Iron"].ToString();
            labelBuildingLevelFood.Text = "Level " + BuildingLevel["Food"].ToString();

            labelMilitariaLevelSpear.Text = "Level " + BuildingLevel["Food"].ToString();

            labelPeasantAssignWood.Text =
                WorkingPeasants["Wood"].ToString() + " / " + WorkingCapacity["Wood"].ToString();
            labelPeasantAssignStone.Text =
                WorkingPeasants["Stone"].ToString() + " / " + WorkingCapacity["Stone"].ToString();
            labelPeasantAssignIron.Text =
                WorkingPeasants["Iron"].ToString() + " / " + WorkingCapacity["Iron"].ToString();
            labelPeasantAssignFood.Text =
                WorkingPeasants["Food"].ToString() + " / " + WorkingCapacity["Food"].ToString();

            labelPeasantAssignSpear.Text =
                WorkingPeasants["Spear"].ToString() + " / " + WorkingCapacity["Spear"].ToString();

            trackBarWoodPeasantAssign.Maximum = BuildingLevel["Wood"] * 2;
            trackBarStonePeasantAssign.Maximum = BuildingLevel["Stone"] * 2;
            trackBarIronPeasantAssign.Maximum = BuildingLevel["Iron"] * 2;
            trackBarFoodPeasantAssign.Maximum = BuildingLevel["Food"] * 2;

            trackBarSpearPeasantAssign.Maximum = MilitariaLevel["Spear"];

            timerResource.Start();
            timerPeasant.Start();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpgradeBuildingWood_Click(object sender, EventArgs e) //przycisk ulepszający tartak
        {
            if (Resource["Wood"] >= UpgradeCostWood["Wood"] &&
                Resource["Stone"] >= UpgradeCostWood["Wood"] &&
                Resource["Iron"] >= UpgradeCostWood["Wood"] &&
                Resource["Food"] >= UpgradeCostWood["Wood"]) //możemy ulepszyć tylko jeśli mamy wymaganą ilość zasobów
            {
                //IncomeRateByPeasant["Wood"] += 5; //zwiększenie skuteczności wieśniaków
                BuildingLevel["Wood"] += 1; //zwiększenie poziomu, a co za tym idzie, pojemności wieśniaków
                labelBuildingLevelWood.Text = "Level " + BuildingLevel["Wood"].ToString(); //wysyłanie nowych danych do labelek
                trackBarWoodPeasantAssign.Maximum =
                    Math.Min(BuildingLevel["Wood"] * 2, Peasants + trackBarWoodPeasantAssign.Value); //zmiana maksimum wieśniaów na trackbarze
                trackBarWoodPeasantAssign.Value = tempPeasantWood;
                labelPeasantAssignWood.Text =
                    trackBarWoodPeasantAssign.Value + " / " + BuildingLevel["Wood"] * 2; //zmiana labelki nad trackbarem

                Resource["Wood"] -= UpgradeCostWood["Wood"]; //odejmowanie kosztów ulepszenia
                Resource["Stone"] -= UpgradeCostWood["Stone"];
                Resource["Iron"] -= UpgradeCostWood["Iron"];
                Resource["Food"] -= UpgradeCostWood["Food"];

                labelResourceWood.Text = Resource["Wood"].ToString(); //wyświetlanie nowych zasobów
                labelResourceStone.Text = Resource["Stone"].ToString();
                labelResourceIron.Text = Resource["Iron"].ToString();
                labelResourceFood.Text = Resource["Food"].ToString();

            }
        }

        private void buttonUpgradeBuildingStone_Click(object sender, EventArgs e) //przycisk ulepszania kamieniołomu, wszystko musi być analogicznie do procesu ulepszania tartaku
        {
            if (Resource["Wood"] >= UpgradeCostStone["Wood"] &&
                Resource["Stone"] >= UpgradeCostStone["Stone"] &&
                Resource["Iron"] >= UpgradeCostStone["Iron"] &&
                Resource["Food"] >= UpgradeCostStone["Food"])
            {
                //IncomeRateByPeasant["Stone"] += 5;
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

        private void buttonUpgradeBuildingIron_Click(object sender, EventArgs e) //przycisk ulepszania kopalni żelaza
        {
            if (Resource["Wood"] >= UpgradeCostIron["Wood"] &&
                Resource["Stone"] >= UpgradeCostIron["Stone"] &&
                Resource["Iron"] >= UpgradeCostIron["Iron"] &&
                Resource["Food"] >= UpgradeCostIron["Food"])
            {
                //IncomeRateByPeasant["Iron"] += 5;
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

        private void buttonUpgradeBuildingFood_Click(object sender, EventArgs e) //przycisk ulepszania sadu jabłoni
        {
            if (Resource["Wood"] >= UpgradeCostFood["Wood"] &&
                Resource["Stone"] >= UpgradeCostFood["Stone"] &&
                Resource["Iron"] >= UpgradeCostFood["Iron"] &&
                Resource["Food"] >= UpgradeCostFood["Food"])
            {
                //IncomeRateByPeasant["Food"] += 5;
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

        private void buttonBuyPalace_Click(object sender, EventArgs e) //kupno pałacu
        {
            if (Resource["Wood"] >= PalaceCostWood &&
                Resource["Stone"] >= PalaceCostStone &&
                Resource["Iron"] >= PalaceCostIron &&
                Resource["Food"] >= PalaceCostFood)
            {
                //wygrywasz
            }
        }

        private void trackBarWoodPeasantAssign_ValueChanged(object sender, EventArgs e) //co się dzieje podczas poruszania trackbarem przy tartaku
        {
            if (trackBarWoodPeasantAssign.Value > tempPeasantWood) //jeżeli wartość na którą chcemy zmienić jest większa od poprzedniej
            {
                if (Peasants <= 0) //jeżeli nie ma już dostępnych wieśniaków
                {
                    trackBarWoodPeasantAssign.Value = tempPeasantWood; //nie pozwalamy na zmianę wartości
                }
                else
                {
                    Peasants -= (trackBarWoodPeasantAssign.Value - tempPeasantWood); //zmniejszamy ilość dostępnych wieśniaków
                    IncomeRate["Wood"] = trackBarWoodPeasantAssign.Value * IncomeRateByPeasant["Wood"]; //dostosowanie przychodu
                    labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString(); //nowy stan wieśniaków na labelce
                    labelPeasantAssignWood.Text =
                        trackBarWoodPeasantAssign.Value + " / " + BuildingLevel["Wood"] * 2; //nowy stan wieśniaków w tartaku na labelce
                }
            }
            else //jeżeli jest mniejsza, to nie musimy się martwić, że zabraknie nam wieśniaków
            {
                Peasants += (tempPeasantWood - trackBarWoodPeasantAssign.Value);
                IncomeRate["Wood"] = trackBarWoodPeasantAssign.Value * IncomeRateByPeasant["Wood"];
                labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();
                labelPeasantAssignWood.Text =
                    trackBarWoodPeasantAssign.Value + " / " + BuildingLevel["Wood"] * 2;
            }
            tempPeasantWood = trackBarWoodPeasantAssign.Value; //to musi być na samym dole, po przyjęciu nowej wartości, musi być ona przypisana do kolejnego porównywania
        }

        private void trackBarStonePeasantAssign_Scroll(object sender, EventArgs e) //analogicznie jak przy tartaku
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

        private void trackBarIronPeasantAssign_Scroll(object sender, EventArgs e) //analogicznie jak przy tartaku
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

        private void trackBarFoodPeasantAssign_Scroll(object sender, EventArgs e) //analogicznie jak przy tartaku
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

        private void timerResource_Tick(object sender, EventArgs e) //główny wskaźnik czasu w grze, za każdym tikiem, następuje zmiana zasobów
        {
            Resource["Wood"] += IncomeRate["Wood"];
            labelResourceWood.Text = Resource["Wood"].ToString();

            Resource["Stone"] += IncomeRate["Stone"];
            labelResourceStone.Text = Resource["Stone"].ToString();

            Resource["Iron"] += IncomeRate["Iron"];
            labelResourceIron.Text = Resource["Iron"].ToString();

            Resource["Food"] += IncomeRate["Food"];
            labelResourceFood.Text = Resource["Food"].ToString();

            Militaria["Spear"] += IncomeRate["Spear"];
            labelResourceSpear.Text = Militaria["Spear"].ToString();
        }

        private void timerPeasant_Tick(object sender, EventArgs e) //dodatkowy wskaźnik czasu, tutaj następuje przyrost wieśniaków, zakładam możliwość modyfikacji, więc operujemy na typie float
        {
            if (PeasantCurrentIncreasePoints >= PeasantIncreasePoints && Peasants<Capacity)
            {
                PeasantCurrentIncreasePoints = 0;
                Peasants++;
                labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();
            }
            else
                PeasantCurrentIncreasePoints += PeasantIncreasePointsTick;

            if (TickCounter == 300)
            {
                DefensePoints = DefensePoints - BanditStrength;
                if (DefensePoints < 0)
                {
                    //przegrywasz
                }
                else
                {
                    TickCounter = 0;
                    BanditStrength = BanditStrength * 2;
                }
            }

            TickCounter++;
        }

        private void trackBarSpearPeasantAssign_Scroll(object sender, EventArgs e)
        {
            if (trackBarSpearPeasantAssign.Value > tempPeasantSpear) //jeżeli wartość na którą chcemy zmienić jest większa od poprzedniej
            {
                if (Peasants <= 0) //jeżeli nie ma już dostępnych wieśniaków
                {
                    trackBarSpearPeasantAssign.Value = tempPeasantSpear; //nie pozwalamy na zmianę wartości
                }
                else
                {
                    Peasants -= (trackBarSpearPeasantAssign.Value - tempPeasantSpear); //zmniejszamy ilość dostępnych wieśniaków
                    IncomeRate["Spear"] = trackBarSpearPeasantAssign.Value * IncomeRateByPeasant["Spear"]; //dostosowanie przychodu
                    labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString(); //nowy stan wieśniaków na labelce
                    labelPeasantAssignSpear.Text =
                        trackBarSpearPeasantAssign.Value + " / " + MilitariaLevel["Spear"]; //nowy stan wieśniaków w tartaku na labelce
                }
            }
            else //jeżeli jest mniejsza, to nie musimy się martwić, że zabraknie nam wieśniaków
            {
                Peasants += (tempPeasantSpear - trackBarSpearPeasantAssign.Value);
                IncomeRate["Spear"] = trackBarSpearPeasantAssign.Value * IncomeRateByPeasant["Spear"];
                labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();
                labelPeasantAssignSpear.Text =
                    trackBarSpearPeasantAssign.Value + " / " + MilitariaLevel["Spear"];
            }
            tempPeasantSpear = trackBarSpearPeasantAssign.Value;
        }

        private void buttonTrainSpearman_Click(object sender, EventArgs e)
        {
            if (Militaria["Spear"] > 0 && Peasants > 0)
            {
                Militaria["Spear"]--;
                labelResourceSpear.Text = Militaria["Spear"].ToString();
                Spearmen++;
                Peasants--;
                labelMilitariaSpearmen.Text = Spearmen.ToString();
                labelResourcePeasant.Text = Peasants.ToString() + " / " + Capacity.ToString();
                DefensePoints = (int) (Spearmen * SpearmenStrength);
                labelDefensePoints.Text = DefensePoints.ToString();
            }
        }

        private void buttonUpgradeMilitariaSpear_Click(object sender, EventArgs e)
        {
            if (Resource["Wood"] >= UpgradeCostFood["Wood"] &&
                Resource["Stone"] >= UpgradeCostFood["Stone"] &&
                Resource["Iron"] >= UpgradeCostFood["Iron"] &&
                Resource["Food"] >= UpgradeCostFood["Food"])
            {
                MilitariaLevel["Spear"] += 1;
                labelBuildingLevelFood.Text = "Level " + MilitariaLevel["Spear"].ToString();
                trackBarSpearPeasantAssign.Maximum =
                    Math.Min(MilitariaLevel["Spear"], Peasants + trackBarSpearPeasantAssign.Value);
                trackBarSpearPeasantAssign.Value = tempPeasantSpear;
                labelPeasantAssignSpear.Text =
                    trackBarSpearPeasantAssign.Value + " / " + MilitariaLevel["Spear"];

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

        private void buttonBuyPalisade_Click(object sender, EventArgs e)
        {
            if (Resource["Wood"] > PalisadeCostWood && Resource["Iron"] > PalisadeCostIron)
            {
                SpearmenStrength = SpearmenStrength * 2;
                Resource["Wood"] -= PalisadeCostWood;
                Resource["Iron"] -= PalisadeCostIron;
            }
        }
    }
}

