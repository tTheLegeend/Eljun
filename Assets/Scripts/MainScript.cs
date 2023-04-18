using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainScript : MonoBehaviour
{
    //Resources variables declaration
    private int wood = 0;
    private int food = 15;
    private int leather = 0;
    private int stone = 0;
    private int metals = 0;
    private int heater = 0;
    private int medicine = 0;
    private int pop = 5;
    private int days = 1;

    //Workforce variables declaration
    private int w_wood = 0;
    private int w_food = 0;
    private int w_leather = 0;
    private int w_stone = 0;
    private int w_metals = 0;
    private int w_heater = 0;
    private int w_medicine = 0;

    //Time & Hour variables
    private int hour;
    private float timer;

    //External Assigment for UI text
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI leatherText;
    public TextMeshProUGUI stoneText;
    public TextMeshProUGUI metalsText;
    public TextMeshProUGUI heaterText;
    public TextMeshProUGUI medicineText;
    public TextMeshProUGUI popText;
    public TextMeshProUGUI daysText;
    public TextMeshProUGUI hourText;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CalculateHour", 3.0f, 3.0f); // Every 3 seconds an hour passes in game 
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;

        //Time & Day Update
        hourText.text = hour + ":00";
        daysText.text = "Day " + days;


        //Wood test
        if (Input.GetKeyDown("e"))
        {
            wood = wood + 1;
            Debug.Log("E PRESSED");
        }
        woodText.text = "" + wood;
        foodText.text = "" + food;

        // Debug Time test.
        if(Input.GetKeyDown("t"))
        {
            Debug.Log(timer);
        }
        

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
}
