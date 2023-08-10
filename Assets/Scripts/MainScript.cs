using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MainScript : MonoBehaviour
{
    //Resources variables declaration
    private float wood = 0;
    private float food = 15;
    private float leather = 0;
    private float stone = 0;
    private float metals = 0;
    private float heaters = 0;
    private float medicine = 0;
    private float clothes = 0;
    private int pop = 5;
    private int popFree = 5;
    private int popBusy = 0;
    private int days = 1;

    //Workforce variables declaration
    private int w_wood = 0;
    private int w_food = 0; // Also increase leather
    private int w_stone = 0; // Also increase metals
    private int w_heaters = 0;
    private int w_medicine = 0;
    private int w_clothes = 0;

    //Tools variables declaration
    private int axe = 0;
    private int bow = 0;
    private int pickaxe = 0;

    //Buildings Levels variables declaration
    private float l_lumber = 0.0f;
    private float l_hunting = 0.0f;
    private int l_house = 10;
    private int l_storage = 25;
    private float l_artisan = 0.0f;
    private float l_mine = 0.0f;

    // Variables for leveling up the house
    private int t1_house = 0;
    private int t2_house = 10;
    private int t3_house = 10;

    // Variables for leveling up the storage
    private int t1_storage = 0;
    private int t2_storage = 10;
    private int t3_storage = 10;

    // Variable for leveling up the artisan building
    private int t1_artisan = 0;

    // Variable for leveling up the mine
    private int t1_mine = 0;
    private int t2_mine = 10;
    private int t3_mine = 10;
    private int t4_mine = 10;

    private int cW1_mine = 20;

    private int cW2_mine = 50;
    private int cM2_mine = 10;

    private int cW3_mine = 100;
    private int cM3_mine = 50;

    private int cW4_mine = 350;
    private int cM4_mine = 300;

    private int cW_artisan = 20;
    private int cS_artisan = 10;
    private int cM_artisan = 5;
    private int cL_artisan = 2;

    //Time & Hour variables
    private int hour;
    public float secondsPerHour = 3.0f;
    int gameSpeed = 3;
    // used in changing speed
    // 1 - /4
    // 2 - /2
    // 3 - x1
    // 4 - x2
    // 5 - x4
    // 6 - x6
    // 7 - x8
    // 8 - x10

    // Notification Variables
    int nrOfNotifs = 0;
    int nrOfNotifsPrio = 0;

    // Bools to change color on no res for passive craft
    bool gotRawMaterials = false;
    bool storageCap = false;

    // Crisis Declarations
    //Variables used to check heat levels.
    bool bHeatAll = false;
    bool bHeatSome = false;

    int plague = 7;
    bool plagueActive = false;
    int plagueCount = 7;

    int freeze = 20;
    bool freezeActive = false;
    int freezeCount = 20;

    // End Screen Stats Variables
    int crisisCount = 0;
    int popDeaths = 0;
    int popStarvedDeaths = 0;
    int popSickDeaths = 0;
    int popFrozenDeaths = 0;
    int score = 0;
    int scoreHigh;

    // Dev Controls
    bool console = false;


    //Above variables are taken and inserted into the their own texts in the scene
    public TextMeshProUGUI crisisCountText;
    public TextMeshProUGUI popDeathsText;
    public TextMeshProUGUI popStarvedDeathsText;
    public TextMeshProUGUI popSickDeathsText;
    public TextMeshProUGUI popFrozenDeathsText;
    public TextMeshProUGUI daysSurvivedText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreHighText;
    [SerializeField] private GameObject GameOverScene;

    //External Assigment for UI text
    //Resources Count
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI leatherText;
    public TextMeshProUGUI stoneText;
    public TextMeshProUGUI metalsText;
    public TextMeshProUGUI heaterText;
    public TextMeshProUGUI medicineText;
    public TextMeshProUGUI tailorText;

    public TextMeshProUGUI popText;
    // Clock
    public TextMeshProUGUI daysText;
    public TextMeshProUGUI hourText;

    //  Job Sub-menu UI
    public TextMeshProUGUI woodJobs;
    public TextMeshProUGUI foodJobs;
    public TextMeshProUGUI leatherJobs;
    public TextMeshProUGUI stoneJobs;
    public TextMeshProUGUI metalsJobs;
    public TextMeshProUGUI heaterJobs;
    public TextMeshProUGUI medicineJobs;
    public TextMeshProUGUI tailorJobs;

    //  Craft Sub-menu UI
    public TextMeshProUGUI axeCount;
    public TextMeshProUGUI pickaxeCount;
    public TextMeshProUGUI bowCount;
    public TextMeshProUGUI clothesCount;
    public TextMeshProUGUI heatersCount;
    public TextMeshProUGUI medicineCount;

    //  Build Sub-menu UI
    // Building Level
    public TextMeshProUGUI LumberHutLevel;
    public TextMeshProUGUI HuntingLodgeLevel;
    public TextMeshProUGUI HouseLevel;
    public TextMeshProUGUI StorageLevel;
    public TextMeshProUGUI ArtisanWorkshopLevel;
    public TextMeshProUGUI MineLevel;

    //  Build Cost
    public TextMeshProUGUI LumberHutCost;
    public TextMeshProUGUI HuntingLodgeCost;
    public TextMeshProUGUI HouseCost;
    public TextMeshProUGUI StorageCost;
    public TextMeshProUGUI ArtisanWorkshopCost;
    public TextMeshProUGUI MineCost;

    //Remove Upgrade Buttons once building are maxed
    [SerializeField] private GameObject LumberUpgrade;
    [SerializeField] private GameObject HuntingHutUpgrade;
    [SerializeField] private GameObject HouseUpgrade;
    [SerializeField] private GameObject StorageUpgrade;
    [SerializeField] private GameObject MineUpgrade;
    [SerializeField] private GameObject ArtisanUpgrade;

    //Artisan create building & remove blockers so jobs, tools and building upgrades can be used
    [SerializeField] private GameObject ArtisanBuildButton;
    [SerializeField] private GameObject ArtisanBuild;
    [SerializeField] private GameObject JobMedicBlocker;
    [SerializeField] private GameObject JobArtisanBlocker;
    [SerializeField] private GameObject JobTailorBlocker;
    [SerializeField] private GameObject CraftMedicineBlocker;
    [SerializeField] private GameObject CraftHeaterBlocker;
    [SerializeField] private GameObject CraftClothesBlocker;

    //Notification Text
    public TextMeshProUGUI notificationText;
    public TextMeshProUGUI notificationPrioText;

    // Start is called before the first frame update
    void Start()
    {
        ResetInvoked();
    }

    // Update is called once per frame
    private void Update()
    {
        //Time & Day Update
        hourText.text = hour + ":00";
        daysText.text = "Day " + days;

        //Cheats/Used for testing
        #region
        if (Input.GetKeyDown("`") && console == false) console = true;
        else if(Input.GetKeyDown("`") && console == true) console = false;
        if(console == true)
        {
            if (Input.GetKeyDown("q"))
            {
                wood = wood + 100;
            }
            if (Input.GetKeyDown("a"))
            {
                wood = wood - 100;
            }
            if (Input.GetKeyDown("w"))
            {
                food = food + 100;
            }
            if (Input.GetKeyDown("s"))
            {
                food = food - 100;
            }
            if (Input.GetKeyDown("e"))
            {
                leather = leather + 100;
            }
            if (Input.GetKeyDown("d"))
            {
                leather = leather - 100;
            }
            if (Input.GetKeyDown("r"))
            {
                stone = stone + 100;
            }
            if (Input.GetKeyDown("f"))
            {
                stone = stone - 100;
            }
            if (Input.GetKeyDown("t"))
            {
                metals = metals + 100;
            }
            if (Input.GetKeyDown("g"))
            {
                metals = metals - 100;
            }
            if (Input.GetKeyDown("y"))
            {
                medicine = medicine + 100;
            }
            if (Input.GetKeyDown("h"))
            {
                medicine = medicine - 100;
            }
            if (Input.GetKeyDown("u"))
            {
                heaters = heaters + 100;
            }
            if (Input.GetKeyDown("j"))
            {
                heaters = heaters - 100;
            }
            if (Input.GetKeyDown("i"))
            {
                clothes = clothes + 100;
            }
            if (Input.GetKeyDown("k"))
            {
                clothes = clothes - 100;
            }

            if (Input.GetKeyDown("l"))
            {
                wood = 00;
                food = 0;
                leather = 0;
                stone = 0;
                metals = 0;
                medicine = 0;
                heaters = 0;
                clothes = 0;
                Debug.Log("Reset Res");
            }

            if (Input.GetKeyDown("z"))
            {
                pop = pop + 10;
                popFree = popFree + 10;
                Debug.Log("Pop Increased by 10!");
            }

            if (Input.GetKeyDown("x"))
            {
                pop = pop + 1;
                popFree = popFree + 1;
                Debug.Log("Pop Increased by 1!");
            }

            if (Input.GetKeyDown("c"))
            {
                PopulationDecrease();
                Debug.Log("Pop decreased by 1!");
            }

            if (Input.GetKeyDown("v"))
            {
                for (int i = 0; i < 10; i++)
                {
                    PopulationDecrease();
                }
                Debug.Log("Pop decreased by 10!");
            }

            if (Input.GetKeyDown("o"))
            {
                days += 1;
                Debug.Log("One day was added!");
            }

            if (Input.GetKeyDown("p"))
            {
                days -= 1;
                Debug.Log("One day was substracted!");
            }

            if(Input.GetKeyDown("m"))
            {
                PlayerPrefs.DeleteAll();
                Debug.Log("Highscore deleted!");
            }
        }
        
        #endregion

        // Insert resource values in the text boxes of unity
        woodText.text = "" + (int)wood;
        foodText.text = "" + (int)food;
        leatherText.text = "" + (int)leather;
        stoneText.text = "" + (int)stone;
        metalsText.text = "" + (int)metals;
        medicineText.text = "" + (int)medicine;
        heaterText.text = "" + (int)heaters;
        tailorText.text = "" + (int)clothes;

        // Population [Total Nr / Non Employed / Sick / Freezing]
        if (popFree > 0 && freeze > 0 && freezeActive == true) popText.text = (int)pop + " (" + popFree + ") " + "<color=#add8e6ff> (" + (int)freeze + ") </color>";
        else if (popFree > 0 && plague > 0 && plagueActive == true) popText.text = (int)pop + " (" + popFree + ") " + "<color=#00ff00ff> (" + (int)plague + ") </color>";
        else if (freeze > 0 && freezeActive == true) popText.text = (int)pop + "<color=#add8e6ff> (" + (int)freeze + ") </color>";
        else if (plague > 0 && plagueActive == true) popText.text = (int)pop + "<color=#00ff00ff> (" + (int)plague + ") </color>";
        else if (popFree > 0) popText.text = (int)pop + " (" + popFree + ")";
        else popText.text = "" + (int)pop;

        // Add & Subtract from Jobs
        woodJobs.text = "" + w_wood;    
        foodJobs.text = "" + w_food;
        //leatherJobs.text = "" + w_leather;
        stoneJobs.text = "" + w_stone;
        //metalsJobs.text = "" + w_metals;
        heaterJobs.text = "" + w_heaters;
        medicineJobs.text = "" + w_medicine;
        tailorJobs.text = "" + w_clothes;

        // Add to tools
        // If/else statements are there so game displays only population which needs the tools.
        if (w_wood - 5 <= 0) axeCount.text = axe + "/0";
        else axeCount.text = axe + "/" + (w_wood - 5);

        if(w_food - 10 <= 0) bowCount.text = bow + "/0";
        else bowCount.text = bow + "/" + (w_food - 10);

        if (w_stone - 1 <= 0) pickaxeCount.text = pickaxe + "/0";
        else pickaxeCount.text = pickaxe + "/" + (w_stone - 1);

        heatersCount.text = (int)heaters + "/" + pop; ;
        clothesCount.text = (int)clothes + "/" + pop; ;
        medicineCount.text = (int)medicine + "/" + pop; ;

        // Game Over
        if ((int)pop <= 0) GameOver();

    }

    private void CalculateHour()        // Function to calculate current hour
    {
        hour = hour + 1;
        if (hour == 24)
        {
            hour = 0;
            days++;
        }
    }

    // Function to calculate production.
    private (float, float) CalculateProduction(float resource, int workers, float productionIncrease, float ResourceModifier, float rawMaterial, int cost)
    {
        float ResPerSec = (workers / ResourceModifier);

        if (resource < l_storage)
        {
            storageCap = false;
            if (rawMaterial > (cost / ResourceModifier) * ResPerSec && workers > 0)
            {
                gotRawMaterials = true;
                rawMaterial = (rawMaterial - (cost * workers) / ResourceModifier);
                if (rawMaterial <= 0) rawMaterial = 0;
                resource = ResPerSec + ResPerSec * productionIncrease + resource;
            }
            else gotRawMaterials = false;
        }
        else
        {
            storageCap = true;
        }
        return (resource, rawMaterial);
    }

    // Function to calculate production.
    private (float, float, float) CalculateProduction(float resource, int workers, float productionIncrease, float ResourceModifier, float rawMaterial, int cost, float rawMaterial2, int cost2)
    {
        float ResPerSec = (workers / ResourceModifier);

        if (resource < l_storage)
        {
            storageCap = false;
            if (rawMaterial > (cost / ResourceModifier) * ResPerSec && rawMaterial2 > (cost2 / ResourceModifier) * ResPerSec && workers > 0)
            {
                gotRawMaterials = true;
                rawMaterial = (rawMaterial - (cost * workers) / ResourceModifier);
                rawMaterial2 = (rawMaterial2 - (cost2 * workers) / ResourceModifier);
                if (rawMaterial <= 0) rawMaterial = 0;
                if (rawMaterial2 <= 0) rawMaterial2 = 0;
                resource = ResPerSec + ResPerSec * productionIncrease + resource;
            }
            else gotRawMaterials = false;
        }
        else
        {
            storageCap = true;
        }
        return (resource, rawMaterial, rawMaterial2);
    }

    // Function to calculate production with tools
    private float CalculateProduction(float resource, int workers, float productionIncrease, int tool, float ResourceModifier, int FreeJobsNoTools)
    {
        float r_treshhold = workers - FreeJobsNoTools; // R_treshhold is the people who don't have tools and need tools
        float ResPerSec = (workers / ResourceModifier);

        if (r_treshhold < 0) r_treshhold = 0;

        if (resource < l_storage)
        {
            storageCap = false;
            // If tools are not enough, production is severly affected
            if (tool >= r_treshhold) resource = ResPerSec + ResPerSec * productionIncrease + r_treshhold/ResourceModifier + resource;
            if (tool < r_treshhold) resource = ResPerSec + ResPerSec * productionIncrease - ((r_treshhold - tool) *0.75f)/ResourceModifier + resource;
        }
        else
        {
            storageCap = true;
        }
        return resource;
    }

    private void ResourcesResults()        // Function to calculate current resources
    {
        wood = CalculateProduction(wood, w_wood, l_lumber, axe, 1f, 5);             // Take workers, building level and current tools in and calculate current res
        if (storageCap == true) woodText.color = new Color32(220, 20, 60, 255);     // If storage cap is full change color to red
        else woodText.color = new Color(1f, 1f, 1f);                                // If storage cap is not full change color to white

        food = CalculateProduction(food, w_food, l_hunting, bow, 2f, 10);           // Take workers, building level and current tools in and calculate current res
        if (storageCap == true) foodText.color = new Color32(220, 20, 60, 255);     // If storage cap is full change color to red
        else foodText.color = new Color(1f, 1f, 1f);                                // If storage cap is not full change color to white

        leather = CalculateProduction(leather, w_food, l_hunting, bow, 4f, 3);      // Take workers, building level and current tools in and calculate current res
        if (storageCap == true) leatherText.color = new Color32(220, 20, 60, 255);  // If storage cap is full change color to red
        else leatherText.color = new Color(1f, 1f, 1f);                             // If storage cap is not full change color to white

        stone = CalculateProduction(stone, w_stone, l_mine, pickaxe, 1f, 10);       // Take workers, building level and current tools in and calculate current res
        if (storageCap == true) stoneText.color = new Color32(220, 20, 60, 255);    // If storage cap is full change color to red
        else stoneText.color = new Color(1f, 1f, 1f);                               // If storage cap is not full change color to white

        metals = CalculateProduction(metals, w_stone, l_mine, pickaxe, 9f, 1);      // Take workers, building level and current tools in and calculate current res
        if (storageCap == true) metalsText.color = new Color32(220, 20, 60, 255);   // If storage cap is full change color to red
        else metalsText.color = new Color(1f, 1f, 1f);                              // If storage cap is not full change color to white

        (medicine, food) = CalculateProduction(medicine, w_medicine, l_artisan, 12f, food, 5);  // Take workers, building level and current tools in and calculate current res
        if (storageCap == true) medicineText.color = new Color32(220, 20, 60, 255);             // If storage cap is full change color to red
        else if (gotRawMaterials == false) medicineText.color = new Color(0.9f, 0.9f, 0.5f);    // If storage cap is not full change color to white
        else medicineText.color = new Color(1f, 1f, 1f);

        (clothes, leather) = CalculateProduction(clothes, w_clothes, l_artisan, 12f, leather, 5);  // Take workers, building level and current tools in and calculate current res
        if (storageCap == true) tailorText.color = new Color32(220, 20, 60, 255);                  // If storage cap is full change color to red
        else if (gotRawMaterials == false) tailorText.color = new Color(0.9f, 0.9f, 0.5f);         // If storage cap is not full change color to white
        else tailorText.color = new Color(1f, 1f, 1f);

        (heaters, stone, metals) = CalculateProduction(heaters, w_heaters, l_artisan, 25f, stone, 5, metals, 5);  // Take workers, building level and current tools in and calculate current res
        if (storageCap == true) heaterText.color = new Color32(220, 20, 60, 255);                                 // If storage cap is full change color to red
        else if (gotRawMaterials == false) heaterText.color = new Color(0.9f, 0.9f, 0.5f);                        // If storage cap is not full change color to white
        else heaterText.color = new Color(1f, 1f, 1f);
    }

    // Functions to increase or decrease workers values
    #region
    //Wood
    public void AddToWoodJobs()
    {
        if(popFree > 0)
        {
            w_wood = w_wood + 1;
            popFree = popFree - 1;
            popBusy = popBusy + 1;
        }     
    }

    public void SubFromWoodJobs()
    {
        if (w_wood > 0)
        { 
            w_wood = w_wood - 1; 
            popFree = popFree + 1;
            popBusy = popBusy - 1;
        }
    }

    // Hunters = Food
    public void AddToHunterJobs()
    {        
        if (popFree > 0)
        {
            w_food = w_food + 1;
            popFree = popFree - 1;
            popBusy = popBusy + 1;
        }
    }

    public void SubFromHunterJobs()
    {
        if (w_food > 0)
        {
            w_food = w_food - 1;
            popFree = popFree + 1;
            popBusy = popBusy - 1;
        }
    }

    // Miners
    public void AddToMinerJobs()
    {
        if (popFree > 0)
        {
            w_stone = w_stone + 1;
            popFree = popFree - 1;
            popBusy = popBusy + 1;
        }        
    }

    public void SubFromMinerJobs()
    {
        if (w_stone > 0)
        {
            w_stone = w_stone - 1;
            popFree = popFree + 1;
            popBusy = popBusy - 1;
        }
    }

    // Medics
    public void AddToMedicJobs()
    {        
        if (popFree > 0)
        {
            w_medicine = w_medicine + 1;
            popFree = popFree - 1;
            popBusy = popBusy + 1;
        }
    }

    public void SubFromMedicJobs()
    {
        if (w_medicine > 0)
        {
            w_medicine = w_medicine - 1;
            popFree = popFree + 1;
            popBusy = popBusy - 1;
        }
    }

    // Artisans
    public void AddToArtisanJobs()
    {
        
        if (popFree > 0)
        {
            w_heaters = w_heaters + 1;
            popFree = popFree - 1;
            popBusy = popBusy + 1;
        }
    }

    public void SubFromArtisanJobs()
    {
        if (w_heaters > 0)
        {
            w_heaters = w_heaters - 1;
            popFree = popFree + 1;
            popBusy = popBusy - 1;
        }
    }

    // Tailors
    public void AddToTailorJobs()
    {
        if (popFree > 0)
        {
            w_clothes = w_clothes + 1;
            popFree = popFree - 1;
            popBusy = popBusy + 1;
        }
    }

    public void SubFromTailorJobs()
    {
        if (w_clothes > 0)
        {
            w_clothes = w_clothes - 1;
            popFree = popFree + 1;
            popBusy = popBusy - 1;
        }
    }
    #endregion

    // Functions to increase number of tools
    #region
    //Axe
    public void AddAxe()
    {
        if(wood >= 5 && metals >= 1)
        {
            axe = axe + 1;
            wood = wood - 5;
            metals = metals - 1;
        }
        else
        {
            StartCoroutine(Notification("No resources to craft!", 5));
            //print("No resource to craft that!");
        }
    }

    // Pickaxe
    public void AddPickaxe()
    {
        if (wood >= 5 && metals >= 5)
        {
            pickaxe = pickaxe + 1;
            wood = wood - 5;
            metals = metals - 5;
        }
        else
        {
            StartCoroutine(Notification("No resources to craft!", 5));
            //print("No resource to craft that!");
        }
        
    }

    // Bow
    public void AddBow()
    {
        if (wood >= 5)
        {
            bow = bow + 1;
            wood = wood - 5;
        }
        else
        {
            StartCoroutine(Notification("No resources to craft!", 5));
            //print("No resource to craft that!");
        }
    }

    // Clothes
    public void AddClothes()
    {
        if (leather >= 20 && l_storage >= clothes)
        {
            clothes = clothes + 1;
            leather = leather - 20;
        }
        else
        {
            StartCoroutine(Notification("No resources to craft!", 5));
            //print("No resource to craft that!");
        }
    }

    // Heaters
    public void AddHeater()
    {
        if (stone >= 10 && metals >= 10 && l_storage >= heaters)
        {
            heaters = heaters + 1;
            stone = stone - 10;
            metals = metals - 10;
        }
        else
        {
            StartCoroutine(Notification("No resources to craft!", 5));
            //print("No resource to craft that!");
        }
    }

    // Medicine
    public void AddMedicine()
    {
        if (food >= 30 && l_storage >= medicine)
        {
            medicine = medicine + 1;
            food = food - 30;
        }
        else
        {
            StartCoroutine(Notification("No resources to craft!", 5));
            //print("No resource to craft that!");
        }
    }

    #endregion

    // Functions to increase building level
    #region

    //Wood
    public void LevelUpLumberHut()
    {
        if(l_lumber == 0 && wood >= 10)
        {
            l_lumber = 0.1f;
            wood = wood - 10;
            LumberHutCost.text = "20 Wood";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            return;
        }
        if (l_lumber == 0.1f && wood >= 20)
        {
            l_lumber = 0.2f;
            wood = wood - 20;
            LumberHutCost.text = "40 Wood";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            return;
        }
        if (l_lumber == 0.2f && wood >= 40)
        {
            l_lumber = 0.3f;
            wood = wood - 40;
            LumberHutCost.text = "60 Wood";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            return;
        }
        if (l_lumber == 0.3f && wood >= 60)
        {
            l_lumber = 0.4f;
            wood = wood - 60;
            LumberHutCost.text = "75 Wood";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            return;
        }
        if (l_lumber == 0.4f && wood >= 75)
        {
            l_lumber = 0.5f;
            wood = wood - 75;
            LumberHutCost.text = "90 Wood";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            return;
        }
        if (l_lumber == 0.5f && wood >= 90)
        {
            l_lumber = 0.6f;
            wood = wood - 90;
            LumberHutCost.text = "120 Wood";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            return;
        }
        if (l_lumber == 0.6f && wood >= 120)
        {
            l_lumber = 0.7f;
            wood = wood - 120;
            LumberHutCost.text = "140 Wood";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            return;
        }
        if (l_lumber == 0.7f && wood >= 140)
        {
            l_lumber = 0.8f;
            wood = wood - 140;
            LumberHutCost.text = "160 Wood";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            return;
        }
        if (l_lumber == 0.8f && wood >= 160)
        {
            l_lumber = 0.9f;
            wood = wood - 160;
            LumberHutCost.text = "180 Wood 20 Stone";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            return;
        }
        if (l_lumber == 0.9f && wood >= 180 && stone >= 20)
        {
            l_lumber = 1.0f;
            wood = wood - 180;
            stone = stone - 20;
            LumberHutCost.text = "200 Wood 40 Stone";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            return;
        }
        if (l_lumber == 1.0f && wood >= 200 && stone >= 40)
        {
            l_lumber = 1.1f;
            wood = wood - 200;
            stone = stone - 40;
            LumberHutCost.text = "250 Wood 60 Stone";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            return;
        }
        if (l_lumber == 1.1f && wood >= 250 && stone >= 60)
        {
            l_lumber = 1.2f;
            wood = wood - 250;
            stone = stone - 60;
            LumberHutCost.text = "280 Wood 80 Stone";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            return;
        }
        if (l_lumber == 1.2f && wood >= 280 && stone >= 80)
        {
            l_lumber = 1.5f;
            wood = wood - 280;
            stone = stone - 80;
            LumberHutCost.text = "350 W 300 S 50 M";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            return;
        }
        if (l_lumber == 1.5f && wood >= 350 && stone >= 300 && metals >= 50)
        {
            l_lumber = 2.0f;
            wood = wood - 350;
            stone = stone - 300;
            metals = metals - 50;
            LumberHutCost.text = "Max Level";
            LumberHutLevel.text = "+" + l_lumber * 100 + "%";
            LumberUpgrade.SetActive(false); //Should I do this?
            return;
        }
    }

    // Hunters
    public void LevelUpHuntingLodge()
    {
        if (l_hunting == 0 && wood >= 20)
        {
            l_hunting = 0.1f;
            wood = wood - 20;
            HuntingLodgeCost.text = "30 Wood";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            return;
        }
        if (l_hunting == 0.1f && wood >= 30)
        {
            l_hunting = 0.2f;
            wood = wood - 30;
            HuntingLodgeCost.text = "40 Wood";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            return;
        }
        if (l_hunting == 0.2f && wood >= 40)
        {
            l_hunting = 0.3f;
            wood = wood - 40;
            HuntingLodgeCost.text = "50 Wood";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            return;
        }
        if (l_hunting == 0.3f && wood >= 50)
        {
            l_hunting = 0.4f;
            wood = wood - 50;
            HuntingLodgeCost.text = "55 Wood";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            return;
        }
        if (l_hunting == 0.4f && wood >= 55)
        {
            l_hunting = 0.5f;
            wood = wood - 55;
            HuntingLodgeCost.text = "60 Wood";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            return;
        }
        if (l_hunting == 0.5f && wood >= 60)
        {
            l_hunting = 0.6f;
            wood = wood - 60;
            HuntingLodgeCost.text = "30 Wood 10 Stone";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            return;
        }
        if (l_hunting == 0.6f && wood >= 30 && stone >= 10)
        {
            l_hunting = 0.7f;
            wood = wood - 30;
            stone = stone - 10;
            HuntingLodgeCost.text = "30 Wood 15 Stone";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            return;
        }
        if (l_hunting == 0.7f && wood >= 30 && stone >= 15)
        {
            l_hunting = 0.8f;
            wood = wood - 30;
            stone = stone - 15;
            HuntingLodgeCost.text = "30 Wood 20 Stone";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            return;
        }
        if (l_hunting == 0.8f && wood >= 30 && stone >= 20)
        {
            l_hunting = 0.9f;
            wood = wood - 30;
            stone = stone - 20;
            HuntingLodgeCost.text = "30 Wood 25 Stone";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            return;
        }
        if (l_hunting == 0.9f && wood >= 30 && stone >= 25)
        {
            l_hunting = 1.0f;
            wood = wood - 30;
            stone = stone - 25;
            HuntingLodgeCost.text = "60 Wood 50 Stone";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            return;
        }
        if (l_hunting == 1.0f && wood >= 60 && stone >= 50)
        {
            l_hunting = 1.1f;
            wood = wood - 60;
            stone = stone - 50;
            HuntingLodgeCost.text = "80 Wood 60 Stone";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            return;
        }
        if (l_hunting == 1.1f && wood >= 80 && stone >= 70)
        {
            l_hunting = 1.2f;
            wood = wood - 80;
            stone = stone - 70;
            HuntingLodgeCost.text = "100 Wood 100 Stone";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            return;
        }
        if (l_hunting == 1.2f && wood >= 100 && stone >= 100)
        {
            l_hunting = 1.5f;
            wood = wood - 100;
            stone = stone - 100;
            HuntingLodgeCost.text = "200 W 200 S 50 M";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            return;
        }
        if (l_hunting == 1.5f && wood >= 200 && stone >= 200 && metals >= 50)
        {
            l_hunting = 2.0f;
            wood = wood - 200;
            stone = stone - 200;
            metals = metals - 50;
            HuntingLodgeCost.text = "Max Level";
            HuntingLodgeLevel.text = "+" + l_hunting * 100 + "%";
            HuntingHutUpgrade.SetActive(false); //Should I do this?
            return;
        }
    }

    // Population Space
    public void LevelUpHouse()
    {
        if (wood >= 20 && stone >= 10  && leather >= 5 && t1_house < 4)
        {
            l_house = l_house + 10;
            wood = wood - 20;
            stone = stone - 10;
            leather = leather - 5;
            t1_house = t1_house + 1;
            HouseCost.text = "20W 10S 5L";
            HouseLevel.text = "" + l_house;

            if (t1_house == 4)
            {
                t2_house = 0;
                HouseCost.text = "30W 15S 10L";
            }
            return;
        }

        if (wood >= 30 && stone >= 15 && leather >= 10 && t2_house < 5)
        {
            l_house = l_house + 10;
            wood = wood - 30;
            stone = stone - 15;
            leather = leather - 10;
            t2_house = t2_house + 1;
            HouseCost.text = "30W 15S 10L";
            HouseLevel.text = "" + l_house;

            if (t2_house == 5)
            {
                t3_house = 0;
                HouseCost.text = "100W 100S 50L 10M";
            }
            return;
        }

        if (wood >= 100 && stone >= 100 && leather >= 50 && metals >= 10 && t3_house < 5)
        {
            l_house = l_house + 10;
            wood = wood - 100;
            stone = stone - 100;
            leather = leather - 50;
            metals = metals - 10;
            t3_house = t3_house + 1;
            HouseCost.text = "100W 100S 50L 10M";
            HouseLevel.text = "" + l_house;

            if (t3_house == 5)
            {
                HouseUpgrade.SetActive(false);    //Should I do this?
                HouseCost.text = "Max Level";
            }
            return;
        }
    }

    // Resource Storage Space
    public void LevelUpStorage()
    {
        if (wood >= 25 && stone >= 15 && t1_storage < 4)
        {
            l_storage = l_storage + 25;
            wood = wood - 25;
            stone = stone - 15;
            t1_storage = t1_storage + 1;
            StorageCost.text = "25 Wood 15 Stone";
            StorageLevel.text = "" + l_storage;

            if (t1_storage == 4)
            {
                t2_storage = 0;
                StorageCost.text = "50 Wood 80 Stone";
            }
            return;
        }

        if (wood >= 50 && stone >= 80 && t2_storage < 5)
        {
            l_storage = l_storage + 25;
            wood = wood - 50;
            stone = stone - 80;
            t2_storage = t2_storage + 1;
            StorageCost.text = "50 Wood 80 Stone";
            StorageLevel.text = "" + l_storage;

            if (t2_storage == 5)
            {
                t3_storage = 0;
                StorageCost.text = "150 Wood 150 Stone";
            }
            return;
        }

        if (wood >= 150 && stone >= 150 && t3_storage < 5)
        {
            l_storage = l_storage + 25;
            wood = wood - 150;
            stone = stone - 150;
            t3_storage = t3_storage + 1;
            StorageCost.text = "150 Wood 150 Stone";
            StorageLevel.text = "" + l_storage;

            if (t3_storage == 5)
            {
                StorageUpgrade.SetActive(false);    //Should I do this?
                StorageCost.text = "Max Level";
                l_storage = l_storage + 25;
                StorageLevel.text = "" + l_storage;
            }
            return;
        }
    }

    // Artisans
    public void LevelUpArtisanWorkshop()
    {
        if (wood >= cW_artisan && stone >= cS_artisan && metals >= cM_artisan && leather >= cL_artisan && t1_artisan < 11)
        {
            l_artisan = l_artisan + 0.1f;

            wood = wood - cW_artisan;
            cW_artisan = cW_artisan + 5;

            stone = stone - cS_artisan;
            cS_artisan = cS_artisan + 10;

            metals = metals - cM_artisan;
            cM_artisan = cM_artisan + 7;

            leather = leather - cL_artisan;
            cL_artisan = cL_artisan + 10;

            t1_artisan = t1_artisan + 1;

            ArtisanWorkshopCost.text = cW_artisan + "W " + cS_artisan + "S " + cM_artisan + "M " + cL_artisan + "L ";
            ArtisanWorkshopLevel.text = "+" + (int)(l_artisan * 100) + "%";

            if (t1_artisan == 11)
            {
                l_artisan = 1.1f;
                ArtisanWorkshopCost.text = " 150W 200S 100M 150L ";
            }
            return;
        }
        if (l_artisan == 1.1f && wood >= 150 && stone >= 200 && metals >= 100 && leather >= 150)
        {
            l_artisan = 1.2f;
            wood = wood - 150;
            stone = stone - 200;
            metals = metals - 100;
            leather = leather - 150;
            ArtisanWorkshopCost.text = " 200W 250S 200M 250L ";
            ArtisanWorkshopLevel.text = "+" + l_artisan * 100 + "%";
            return;
        }
        if (l_artisan == 1.2f && wood >= 200 && stone >= 250 && metals >= 200 && leather >= 250)
        {
            l_artisan = 1.5f;
            wood = wood - 200;
            stone = stone - 250;
            metals = metals - 200;
            leather = leather - 250;
            ArtisanWorkshopCost.text = " 350W 350S 250M 300L ";
            ArtisanWorkshopLevel.text = "+" + l_artisan * 100 + "%";
            return;
        }
        if (l_artisan == 1.5f && wood >= 350 && stone >= 350 && metals >= 250 && leather >= 300)
        {
            l_artisan = 2.0f;
            wood = wood - 350;
            stone = stone - 350;
            metals = metals - 250;
            leather = leather - 300;
            ArtisanWorkshopCost.text = "Max Level";
            ArtisanWorkshopLevel.text = "+" + l_artisan * 100 + "%";
            ArtisanUpgrade.SetActive(false);    //Should I do this?
            return;
        }

    }

    // Miners
    public void LevelUpMine()
    {
        if (wood >= cW1_mine && t1_mine < 5)
        {
            l_mine = l_mine + 0.1f;

            wood = wood - cW1_mine;
            cW1_mine = cW1_mine + 20;

            t1_mine = t1_mine + 1;

            MineCost.text = cW1_mine + " Wood ";
            MineLevel.text = "+" + (int)(l_mine * 100) + "%";

            if (t1_mine == 5)
            {
                MineCost.text = cW2_mine + " Wood " + cM2_mine + " Metals";
                t2_mine = 0;
            }
            return;
        }

        if (wood >= cW2_mine && metals >= cM2_mine && t2_mine < 3)
        {
            l_mine = l_mine + 0.1f;

            wood = wood - cW2_mine;
            cW2_mine = cW2_mine + 20;

            metals = metals - cM2_mine;
            cM2_mine = cM2_mine + 10;

            t2_mine = t2_mine + 1;

            MineCost.text = cW2_mine + " Wood " + cM2_mine + " Metals";
            MineLevel.text = "+" + (int)(l_mine * 100) + "%";

            if (t2_mine == 3)
            {
                MineCost.text = cW3_mine + " Wood " + cM3_mine + " Metals";
                t3_mine = 0;
            }
            return;
        }

        if (wood >= cW3_mine && metals >= cM3_mine && t3_mine < 5)
        {
            l_mine = l_mine + 0.1f;
            wood = wood - cW3_mine;
            cW3_mine = cW3_mine + 30;

            metals = metals - cM3_mine;
            cM3_mine = cM3_mine + 20;

            t3_mine = t3_mine + 1;

            MineCost.text = cW3_mine + " Wood " + cM3_mine + " Metals";
            MineLevel.text = "+" + (int)(l_mine * 100) + "%";

            if (t3_mine == 5)
            {
                MineCost.text = cW4_mine + " Wood " + cM4_mine + " Metals";
                t4_mine = 0;
            }
            return;
        }
        if (wood >= cW4_mine && metals >= cM4_mine && t4_mine == 0)
        {
            //220 130
            print(l_mine);
            l_mine = l_mine + 0.7f;
            wood = wood - cW4_mine;
            metals = metals - cM4_mine;

            MineCost.text = "Max Level";
            MineLevel.text = "+" + (int)(l_mine * 100) + "%";
            MineUpgrade.SetActive(false);    //Should I do this?
            return;
        }
    }

    #endregion

    public void ArtisanConstruction()
    {
        if (wood >= 60 && stone >= 150 && metals >= 30 && leather >= 15)
        {
            wood = wood - 60;
            stone = stone - 150;
            metals = metals - 30;
            leather = leather - 15;
            
            ArtisanWorkshopCost.text = "20W 10S 5M 2L";

            //Activate buttons for artisans and construct building
            ArtisanBuild.SetActive(true);
            JobArtisanBlocker.SetActive(true);
            JobMedicBlocker.SetActive(true);
            JobTailorBlocker.SetActive(true);
            CraftClothesBlocker.SetActive(true);
            CraftHeaterBlocker.SetActive(true);
            CraftMedicineBlocker.SetActive(true);

            ArtisanBuildButton.SetActive(false);  //Disable blocker

            StartCoroutine(Notification("Artisan Workshop Constructed", 5));
        }
        else
        {
            StartCoroutine(Notification("No resources to build!", 5));
        }
    }

    //Chance for the pop to increase per day
    private void PopulationIncrease()
    {
        bool maxHousing = false;
        if (l_house == 150 && pop >= 150) { maxHousing = true; }

        if (l_house >= pop && maxHousing == false && plagueActive == false && freezeActive == false)
        {
            popText.color = new Color(1f, 1f, 1f);
            int popChance = Random.Range(1, 100);

            if (popChance < 50)
            {
                if (popChance < 10)
                {
                    pop = pop + 5;
                    popFree = popFree + 5;
                }
                else
                {
                    pop = pop + 2;
                    popFree = popFree + 2;
                }
            }
        }
        else if (l_house >= pop && maxHousing == false)
        {
            popText.color = new Color(1f, 1f, 1f);
            // No pop
        }
        else if (l_house < pop && maxHousing == false)
        {
            StartCoroutine(Notification("Max Pop!", 5));
            popText.color = new Color32(220, 20, 60, 255);
        }
        else if (maxHousing == true)
        {
            print("Population Saturated."); 
            popText.color = new Color(0.9f, 0.9f, 0.5f);
        }
        else
        {
            print("ERROR 001");
            StartCoroutine(Notification("ERROR 001", 5));
        }
    }

    //Decrease Pop by one, also includes the order of priotization
    private void PopulationDecrease()
    {
        if (pop > 0) 
        {
            pop -= 1;
            popDeaths++;
        }
        if (popFree == 0 && pop >= 0)
        {
            popBusy -= 1;
            if (w_stone > 0) { w_stone -= 1; return; }
            if (w_wood > 0) { w_wood -= 1; return; }
            if (w_clothes > 0) { w_clothes -= 1; return; }
            if (w_medicine > 0) { w_medicine -=  1; return; }
            if (w_heaters > 0) { w_heaters -= 1; return; }
            if (w_food > 0) { w_food -= 1; return; }
        }
        if (popFree > 0) popFree = popFree - 1;
        GameOver();
    }

    //Passive food consumption from pop
    private void FoodConsumption()
    {
        float starving;

        food = food - popBusy * 2 - popFree;
        starving = food * -1;
        if (starving < 0) starving = 0;

        if (food < 0) food = 0;
        if (food <= 0 && starving > 0)
        {
            for (int i = 0; i < starving; i++)
            {
                PopulationDecrease();
                popStarvedDeaths++;
            }
            StartCoroutine(NotificationPrio("Day " + days + ": " + (int)starving + " Died of Starvation", 5));
        }   
            
    }

    //Passive wood consumption from heaters
    private void WoodConsumption()
    {
        if (heaters > 0 && pop > heaters)
        {
            if (wood >= heaters * 2)
            {
                wood = wood - heaters * 2;
                bHeatSome = true; /* Increase Health and resistence to cold */
                if (wood < 0) wood = 0;
            }
            else if (wood <= heaters * 2)
            {
                // No cold or sickness Ressitance
                bHeatAll = false;
                bHeatSome = false;
                StartCoroutine(NotificationPrio("Day " + days + ": " + "No Firewood for Heaters!", 5));
            }
        }
        else if (heaters > 0 && heaters >= pop)
        {
            if (wood > pop * 2)
            {
                wood = wood - pop * 2;
                bHeatAll = true;   /* Increase some Health and resistence to cold */
                if (wood < 0) wood = 0;
            }
            else if (wood <= pop * 2)
            {
                // No cold or sickness Ressitance
                bHeatAll = false;
                bHeatSome = false;
                StartCoroutine(NotificationPrio("Day " + days + ": " + "No Firewood for Heaters!", 5));
            }
        }
        else 
        { 
            bHeatAll = false;
            bHeatSome = false;
        }
    }

    // Wear of Clothes, Heaters and medicine
    private void AdvancedResourcesWear()
    {
        clothes -= clothes * 0.10f;
        heaters -= heaters * 0.10f;
        medicine -= medicine * 0.10f;
    }

    //Change Game Speed
    public void IncreaseGameSpeed()
    {
        if (gameSpeed < 8)
        {
            secondsPerHour = secondsPerHour / 2;
            gameSpeed = gameSpeed + 1;
            ResetInvoked();
            StartCoroutine(Notification("Speed Increased! Intensity:" + gameSpeed, 1));
            return;
        }
        if (gameSpeed == 8)
        {
            StartCoroutine(Notification("Fastest Speed!", 1));
            return;
        }
    }
    public void DecreaseGameSpeed()
    {
        if (gameSpeed > 0)
        {
            secondsPerHour = secondsPerHour * 2;
            gameSpeed = gameSpeed - 1;
            ResetInvoked();
            StartCoroutine(Notification("Speed Lowered! Intensity:" + gameSpeed, 1));
            return;
        }
        if (gameSpeed == 0)
        {
            StartCoroutine(Notification("Slowest Speed!", 1));
            return;
        }
    }

    // Used for Notifications
    IEnumerator Notification(string text, int time)
    {
        nrOfNotifs++;
        if (nrOfNotifs == 6) { nrOfNotifs = 0; notificationText.text = ""; }
        notificationText.text = notificationText.text + text + "<br>";
        yield return new WaitForSeconds(time);       
    }
    IEnumerator NotificationPrio(string text, int time)
    {
        nrOfNotifsPrio++;
        if (nrOfNotifsPrio == 5) { nrOfNotifsPrio = 0; notificationPrioText.text = ""; }
        notificationPrioText.text = notificationPrioText.text + text + "<br>";
        yield return new WaitForSeconds(time);
    }

    // Plague Crisis
    private void Plague()
    {
        if(plagueActive == true)
        {
            if (plague <= 0) plague = 0;
            if (plague >= pop) plague = pop;
            int DieChance;
            float Death25 = Random.Range(1, plague * 0.25f); // 25% of the plagued people die
            float Death75 = Random.Range(1, plague * 0.75f); // 75% of the plagued people die

            // Check if crisis ended
            if (plague <= 0)
            {
                plagueActive = false;
                plagueCount += 3;
                plague = plagueCount;
            }
            else if ((CheckFood() + CheckHeaters() + CheckMedicine()) == 10)
            {
                // 0% Death Chance
                plague -= Random.Range(0, plague+1);
                if (plague <= 1) plague = 0;
            }
            else if ((CheckFood() + CheckHeaters() + CheckMedicine()) >= 5)
            {
                // 25% Death Chance
                DieChance = Random.Range(1, 5);
                if (DieChance == 4)
                {
                    for (int i = 0; i < Death25; i++)
                    {
                        PopulationDecrease();
                        plague--;
                        popSickDeaths++;
                    }
                    if ((int)Death25 != 0) StartCoroutine(NotificationPrio("Day " + days + ": " + (int)Death25 + " Died of Sickness", 5));
                }
                else
                {
                    float cured = plague * 0.75f;
                    plague -= Random.Range(1, (int)cured);
                    if (plague <= 1) plague = 0;
                }
            }
            else if ((CheckFood() + CheckHeaters() + CheckMedicine()) > 0)
            {
                // 75% Death chance
                DieChance = Random.Range(1, 5);
                if (DieChance != 4)
                {                    
                    for (int i = 0; i < Death75; i++)
                    {
                        PopulationDecrease();
                        plague--;
                        popSickDeaths++;
                    }
                    if ((int)Death75 != 0) StartCoroutine(NotificationPrio("Day " + days + ": " + (int)Death75 + " Died of Sickness", 5));
                }
                else
                {
                    float cured = plague * 0.25f;
                    plague -= Random.Range(1, (int)cured);
                    if (plague <= 1) plague = 0;
                }
            }
            else if ((CheckFood() + CheckHeaters() + CheckMedicine()) <= 0)
            {
                // 100% Death ratio
                for (int i = 0; i < plague; i++)
                {
                    PopulationDecrease();
                    plague--;
                    popSickDeaths++;
                }
                StartCoroutine(NotificationPrio("Day " + days + ": " + plague + " Died of Sickness", 5));
            }
            else StartCoroutine(NotificationPrio("Error 002", 5));
        }
    }

    // Frost Crisis
    private void Freeze()
    {
        if (freezeActive == true)
        {
            if (freeze <= 0) freeze = 0;
            if (freeze >= pop) freeze = pop;
            int DieChance;
            float Death25 = Random.Range(1, freeze * 0.25f); // 25% of the plagued people die
            float Death75 = Random.Range(1, freeze * 0.75f); // 75% of the plagued people die

            // Check if blizzard crisis ended, otherwise continue crisis
            if (freeze <= 0)
            {
                freezeCount += 10;
                freezeActive = false;
                freeze = freezeCount;
            }
            if ((CheckFood() + CheckHeaters() + CheckClothing()) == 10)
            {
                // 0% Death Chance
                freeze -= Random.Range(0, freeze + 1);
                if (freeze <= 1) freeze = 0;
            }
            else if ((CheckFood() + CheckHeaters() + CheckClothing()) >= 5)
            {
                // 25% Death Chance
                DieChance = Random.Range(1, 5);
                if (DieChance == 4)
                {
                    for (int i = 0; i < Death25; i++)
                    {
                        PopulationDecrease();
                        freeze--;
                        popFrozenDeaths++;
                    }
                    if ((int)Death25 != 0) StartCoroutine(NotificationPrio("Day " + days + ": " + (int)Death25 + " Died of Cold", 5));
                }
                else
                {
                    float cured = freeze * 0.75f;
                    freeze -= Random.Range(1, (int)cured);
                    if (freeze <= 1) freeze = 0;
                }
            }
            else if ((CheckFood() + CheckHeaters() + CheckClothing()) > 0)
            {
                // 75% Death chance
                DieChance = Random.Range(1, 5);
                if (DieChance != 4)
                {
                    for (int i = 0; i < Death75; i++)
                    {
                        PopulationDecrease();
                        freeze--;
                        popFrozenDeaths++;
                    }
                    if ((int)Death75 != 0) StartCoroutine(NotificationPrio("Day " + days + ": " + (int)Death75 + " Died of Cold", 5));
                }
                else
                {
                    float cured = freeze * 0.25f;
                    freeze -= Random.Range(1, (int)cured);
                    if (freeze <= 1) freeze = 0;
                }
            }
            else if ((CheckFood() + CheckHeaters() + CheckClothing()) <= 0)
            {
                // 100% Death ratio
                for (int i = 0; i < freeze; i++)
                {
                    PopulationDecrease();
                    freeze--;
                    popFrozenDeaths++;
                }
                StartCoroutine(NotificationPrio("Day " + days + ": " + freeze + " Died of Cold", 5));
            }
            else StartCoroutine(NotificationPrio("Error 003", 5));

        }
    }

    //Calculate Food effects on crisis
    private int CheckFood()
    {
        // At least double food
        if (food >= (popBusy * 2 + popFree) * 2)
        {
            return 2;
        }
        // Normal Food
        else if (food >= popBusy * 2 + popFree) return 1; 
        // Food Deficit
        else return 0;
    }

    //Calculate medicine effects on crisis
    private int CheckMedicine()
    {
        // At least double medicine
        if (medicine >= pop * 2)
        {
            medicine = medicine - pop * 1.5f;
            return 6;
        }
        // Normal medicine
        else if (medicine >= pop)
        {
            medicine -= pop;
            return 3;
        }
        // Medicine Deficit
        else return 0;
    }

    //Calculate heaters effects on crisis
    private int CheckHeaters()
    {
        // Enough heaters
        if (heaters >= pop && bHeatAll == true) 
        {
            heaters -= pop * 0.10f; 
            return 2;
        }
        // Some heaters not enough heat
        else if (heaters >= pop && bHeatSome == true)
        {
            heaters -= pop * 0.05f;
            return 1;
        }
        // Some Heaters
        else if (heaters < pop && heaters > 0 && bHeatSome == true)
        {
            heaters -= pop * 0.05f;
            return 1;
        }

        // Heaters Deficit
        else return 0;
    }

    //Calculate clothing effects on crisis
    private int CheckClothing()
    {
        // At least double clothing
        if (clothes >= pop * 2)
        {
            clothes -= pop;
            return 6;
        }
        // Normal clothing
        else if (clothes >= pop)
        {
            clothes -= pop / 2;
            return 3;
        }
        // Clothing Deficit
        else return 0;
    }

    //Probability to trigger a crisis event daily.
    private void CrisisEvent()
    {
        // Crisis Startup on day 10 for plague and after 2 plagues for frost
        // 20% chance for the plague to trigger while between after 10 days
        // 25% chance for the plague to trigger and 5% for the frost to trigger after plague triggered at least twice
        if (days >= 10 && plagueActive == false && freezeActive == false)
        {
            if (crisisCount >= 2)
            {
                int chanceOfCrisis = Random.Range(1, 101);
                if (chanceOfCrisis <= 5)
                {
                    freezeActive = true;
                    crisisCount++;
                }
                else if (chanceOfCrisis <= 25) 
                {
                    crisisCount++;
                    plagueActive = true;
                }
            }
            else // Plague < 2
            {
                int chanceOfCrisis = Random.Range(1, 101);
                if (chanceOfCrisis <= 20)
                {
                    plagueActive = true;
                    crisisCount++;
                }
            }
        }
    }    
    
    // Start dailiy functions
    // Pause game via time buttons 
    public void DeleteInvoked()
    {
        StartCoroutine(Notification("Pause", 1));
        CancelInvoke(nameof(CalculateHour));
        CancelInvoke(nameof(ResourcesResults));
        CancelInvoke(nameof(PopulationIncrease));
        CancelInvoke(nameof(FoodConsumption));
        CancelInvoke(nameof(WoodConsumption));
        CancelInvoke(nameof(Plague));
        CancelInvoke(nameof(Freeze));
        CancelInvoke(nameof(CrisisEvent));
        CancelInvoke(nameof(AdvancedResourcesWear));
    }
    public void ResetInvoked()
    {
        CancelInvoke(nameof(CalculateHour));
        CancelInvoke(nameof(ResourcesResults));
        CancelInvoke(nameof(PopulationIncrease));
        CancelInvoke(nameof(FoodConsumption));
        CancelInvoke(nameof(WoodConsumption));
        CancelInvoke(nameof(Plague));
        CancelInvoke(nameof(Freeze));
        CancelInvoke(nameof(CrisisEvent));
        CancelInvoke(nameof(AdvancedResourcesWear));

        InvokeRepeating(nameof(CalculateHour), secondsPerHour, secondsPerHour); // Every 3 seconds an hour passes in game 
        InvokeRepeating(nameof(ResourcesResults), secondsPerHour, secondsPerHour); // Every 3 seconds the resources are updated 
        InvokeRepeating(nameof(PopulationIncrease), (secondsPerHour * 12), (secondsPerHour * 12));
        InvokeRepeating(nameof(FoodConsumption), (secondsPerHour * 12), (secondsPerHour * 12));
        InvokeRepeating(nameof(WoodConsumption), (secondsPerHour * 12), (secondsPerHour * 12));
        InvokeRepeating(nameof(Plague), (secondsPerHour * 9), (secondsPerHour * 9)); 
        InvokeRepeating(nameof(Freeze), (secondsPerHour * 24), (secondsPerHour * 24)); 
        InvokeRepeating(nameof(CrisisEvent), (secondsPerHour * 24), (secondsPerHour * 24)); 
        InvokeRepeating(nameof(AdvancedResourcesWear), (secondsPerHour * 72), (secondsPerHour * 72)); // Every 3 days some advanced resources expire/wear down 
    }

    //Resume Button
    public void ResumeResetInvoked()
    {
        StartCoroutine(Notification("Resume", 1));
        CancelInvoke(nameof(CalculateHour));
        CancelInvoke(nameof(ResourcesResults));
        CancelInvoke(nameof(PopulationIncrease));
        CancelInvoke(nameof(FoodConsumption));
        CancelInvoke(nameof(WoodConsumption));
        CancelInvoke(nameof(Plague));
        CancelInvoke(nameof(Freeze));
        CancelInvoke(nameof(CrisisEvent));
        CancelInvoke(nameof(AdvancedResourcesWear));

        InvokeRepeating(nameof(CalculateHour), secondsPerHour, secondsPerHour); // Every 3 seconds an hour passes in game 
        InvokeRepeating(nameof(ResourcesResults), secondsPerHour, secondsPerHour); // Every 3 seconds the resources are updated 
        InvokeRepeating(nameof(PopulationIncrease), (secondsPerHour * 12), (secondsPerHour * 12));
        InvokeRepeating(nameof(FoodConsumption), (secondsPerHour * 12), (secondsPerHour * 12));
        InvokeRepeating(nameof(WoodConsumption), (secondsPerHour * 12), (secondsPerHour * 12));
        InvokeRepeating(nameof(Plague), (secondsPerHour * 9), (secondsPerHour * 9));
        InvokeRepeating(nameof(Freeze), (secondsPerHour * 24), (secondsPerHour * 24));
        InvokeRepeating(nameof(CrisisEvent), (secondsPerHour * 24), (secondsPerHour * 24));
        InvokeRepeating(nameof(AdvancedResourcesWear), (secondsPerHour * 72), (secondsPerHour * 72)); // Every 3 days some advanced resources expire/wear down 
    }

    // End the game if pop under 0 and show stats
    public void GameOver()
    {
        // End Game
        if ((int)pop <= 0)
        {
            Time.timeScale = 0f;

            // Calculate end Screen
            score = (days * 100) - popDeaths;
            scoreText.text = "Score: " + score;
            daysSurvivedText.text = "Days Survived: " + days;
            crisisCountText.text = "Crisis Endured: " + crisisCount;
            popDeathsText.text = "Deaths: " + popDeaths;
            popStarvedDeathsText.text = "Starved Deaths: " + popStarvedDeaths;
            popSickDeathsText.text = "Plague Deaths: " + popSickDeaths;
            popFrozenDeathsText.text = "Frozen Deaths: " + popFrozenDeaths;

            // High Score
            scoreHigh = PlayerPrefs.GetInt("HS");
            print(scoreHigh);
            if (score > scoreHigh)
            {
                PlayerPrefs.SetInt("HS", score);
                scoreHigh = PlayerPrefs.GetInt("HS");
                scoreHighText.text = "Highscore: " + scoreHigh;
                PlayerPrefs.Save();
            }
            else scoreHighText.text = "Highscore: " + scoreHigh;

            GameOverScene.SetActive(true);
        }
    }

}