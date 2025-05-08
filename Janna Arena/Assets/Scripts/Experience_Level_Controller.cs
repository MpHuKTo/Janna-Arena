using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static UpgradesScript;

public class Experience_Level_Controller : MonoBehaviour
{
    public static Experience_Level_Controller instance;
    public int CurrentExperience;
    public Experience_Pickup pickup1, pickup2;
    public List<int> expLevels;
    public int CurrentLevel = 1, LevelCount = 30;

    
    private UpgradesScript upgScript;

    [SerializeField]
    public GameObject UpgUI;
    [SerializeField]
    public GameObject upgButton1;
    [SerializeField]
    public GameObject upgButton2;
    [SerializeField]
    public GameObject upgButton3;

    private PlayerController playerController;


    private List<Upgrade> allUpgrades;
    private UpgradeButtonScript UBS1;
    private UpgradeButtonScript UBS2;
    private UpgradeButtonScript UBS3;


    void Start()
    {
        while (LevelCount > expLevels.Count)
        expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.5f));

        UBS1 = upgButton1.GetComponent<UpgradeButtonScript>();
        UBS2 = upgButton2.GetComponent<UpgradeButtonScript>();
        UBS3 = upgButton3.GetComponent<UpgradeButtonScript>();


        playerController = FindObjectOfType<PlayerController>();


        upgScript = FindObjectOfType<UpgradesScript>();
        allUpgrades = upgScript.allUpgrades;
    }

    private void Awake()
    {
        instance = this;
    } 


    public void GetExp(int amountToget)
    {
        CurrentExperience += amountToget;

        if (CurrentExperience >= expLevels[CurrentLevel])
        {
            LevelUp();
            
        }

        UIControler.instance.UpdateExp(CurrentExperience, expLevels[CurrentLevel], CurrentLevel);
    }


    public void SpawnExp1(Vector3 position)
    {
        Instantiate(pickup1, position, Quaternion.identity);
    }

    public void SpawnExp2(Vector3 position)
    {
        Instantiate(pickup2, position, Quaternion.identity);
    }



    public int pickAnUpgrade()
    {

        System.Random rng = new System.Random();


        int generatedNum = rng.Next(allUpgrades.Count);

        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        // ������� ��� �������� ���������� ��������
        bool IsInvalidUpgrade(Upgrade upg, int num)
        {
            // ��������� ��������� ������������� ������ (0-2)
            if ((num == 0 && playerController.Whip.HasWeapon) ||
                (num == 1 && playerController.Staff.HasWeapon) ||
                (num == 2 && playerController.Bow.HasWeapon) ||
                (num == 3 && playerController.AoeWpn.HasWeapon))
            {
                return true; // ������ ��� ���� > ��������� ���� �������
            }

            // ����������: ���� num �� 0 �� 2 � ��� �������� ������������� ������, �� ����� ����������
            if (num >= 0 && num <= 3)
            {
                return false; // ��������� ������������� ������
            }

            // ��������� ���������, ���� ������ �� �������
            if ((upg.UpgradeFor == "Whip" && !playerController.Whip.HasWeapon) ||
                (upg.UpgradeFor == "Staff" && !playerController.Staff.HasWeapon) ||
                (upg.UpgradeFor == "Bow" && !playerController.Bow.HasWeapon) ||
                (upg.UpgradeFor == "Aoe" && !playerController.AoeWpn.HasWeapon))
            {
                return true; // ������ ������� > ��������� ���������
            }

            return false; // ������� ��������
        }


        // ���������� �����, ���� ��� �� ����� ����������
        do
        {
            generatedNum = rng.Next(allUpgrades.Count);
        }
        while (IsInvalidUpgrade(allUpgrades[generatedNum], generatedNum));


        return generatedNum;


    }
    void LevelUp()
    {
        CurrentExperience -= expLevels[CurrentLevel];

        CurrentLevel++;

        if (CurrentLevel >= expLevels.Count)
        {
            CurrentLevel = expLevels.Count - 1;
        }


        

        UpgUI.SetActive(true);

        Upgrade upgradeButton1 = allUpgrades[pickAnUpgrade()];
        Upgrade upgradeButton2;
        Upgrade upgradeButton3;
        do
        {
            upgradeButton2 = allUpgrades[pickAnUpgrade()];

        }
        while (upgradeButton2 == upgradeButton1);

        do
        {
            upgradeButton3 = allUpgrades[pickAnUpgrade()];

        }
        while (upgradeButton3 == upgradeButton1 || upgradeButton3 == upgradeButton2 || upgradeButton3 == upgradeButton1 || upgradeButton3 == upgradeButton1 );


        UBS1.updateButton(upgradeButton1);
        UBS2.updateButton(upgradeButton2);
        UBS3.updateButton(upgradeButton3);


        Time.timeScale = 0f;
    }


    
}