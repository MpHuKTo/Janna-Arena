using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradesScript : MonoBehaviour
{
    public List<Upgrade> allUpgrades;

    

    public class Upgrade
    {
        //�������� � �������� ��������
        public string Name;
        public string Description;
        public string UpgradeFor;


        //���������� ��� �������� ������
        public int playerHp;
        public float playerRegenHP;
        public float playerSpeed;




        //���������� ��� �������� �����
        public bool WhipHasWeapon = false;
        public float WhipCooldown = 0;
        public float WhipDamage = 0;
        //������� ���� �������� �� ������ (����� �� ������)
        public float WhipAttackTime = 0;

        //���������� ��� �������� ������
        public bool StaffHasWeapon = false;
        public float StaffCooldown = 0;
        public float StaffDamage = 0;
        //��������� ����� ����� ������ ������
        public float StaffAttackTime = 0;
        //���������� �������������� ������ �����
        public int StaffProjectileAmmount = 0;
        //�������� �����
        public float StaffProjSpeed = 0;

        //���������� ��� �������� ����
        public bool BowHasWeapon = false;
        public float BowCooldown = 0;
        public float BowDamage = 0;
        //������� ����� ������ ����
        public float BowAttackTime = 0;
        public int BowProjectileAmmount = 0;
        public float BowProjSpeed = 0;

        //�������� AoE �����
        public bool AoeHasWeapon = false;
        public float AoeCooldown = 0;
        public float AoeDamage = 0;
        public float AoeScale = 0;
        

        PlayerController PlayerController = FindObjectOfType<PlayerController>();


        public void processUpgrade()
        {

            //�������� ������
            if (playerHp != 0) { PlayerController.health += playerHp; PlayerController.initialHealth += playerHp;  }
            if (playerRegenHP != 0) PlayerController.hpRegen += playerRegenHP;
            if (playerSpeed != 0) PlayerController.speed += playerSpeed;


                // ������� ����� (Whip)
                if (WhipHasWeapon) PlayerController.Whip.HasWeapon = true;
                if (WhipCooldown != 0) PlayerController.Whip.Cooldown += WhipCooldown;
                if (WhipDamage != 0) PlayerController.Whip.Damage += WhipDamage;
                if (WhipAttackTime != 0) PlayerController.Whip.AttackTime += WhipAttackTime;

                // ������� ������ (Staff)
                if (StaffHasWeapon) PlayerController.Staff.HasWeapon = true;
                if (StaffCooldown != 0) PlayerController.Staff.Cooldown += StaffCooldown;
                if (StaffDamage != 0) PlayerController.Staff.Damage += StaffDamage;
                if (StaffAttackTime != 0) PlayerController.Staff.AttackTime += StaffAttackTime;
                if (StaffProjectileAmmount != 0) PlayerController.Staff.ProjectileAmmount += StaffProjectileAmmount;
                if (StaffProjSpeed != 0) PlayerController.StaffProjSpeed += StaffProjSpeed;

                // ������� ���� (Bow)
                if (BowHasWeapon) PlayerController.Bow.HasWeapon = true;
                if (BowCooldown != 0) PlayerController.Bow.Cooldown += BowCooldown;
                if (BowDamage != 0) PlayerController.Bow.Damage += BowDamage;
                if (BowAttackTime != 0) PlayerController.Bow.AttackTime += BowAttackTime;
                if (BowProjectileAmmount != 0) PlayerController.Bow.ProjectileAmmount += BowProjectileAmmount;
                if (BowProjSpeed != 0) PlayerController.BowProjSpeed     += BowProjSpeed;

                //�������� AoE
                if (AoeHasWeapon) PlayerController.AoeWpn.HasWeapon = true;
                if (AoeCooldown != 0) PlayerController.AoeWpn.Cooldown += AoeCooldown;
                if (AoeDamage != 0) PlayerController.AoeWpn.Damage += AoeDamage;
            if (AoeScale != 0)
            {
                AoEScript Aoe;
                Aoe = FindObjectOfType<AoEScript>();
                Aoe.transform.localScale = new Vector3(Aoe.transform.localScale.x + AoeScale, Aoe.transform.localScale.y + AoeScale, Aoe.transform.localScale.z);   }
                    }

       

        


        public Upgrade() { }

        public Upgrade(string name, string description, string upgFor, int playerHP = 0, float playerRegen = 0, float playerSpd = 0,
                   bool whipHasWeapon = false, float whipCooldown = 0, float whipDamage = 0, float whipAttackTime = 0, bool aoeHasWeapon = false, float aoeCooldown = 0, float aoeDamage = 0, float aoeScale = 0,
                   bool staffHasWeapon = false, float staffCooldown = 0, float staffDamage = 0, float staffAttackTime = 0, int staffProjectileAmmount = 0, float staffProjSpeed = 0,
                   bool bowHasWeapon = false, float bowCooldown = 0, float bowDamage = 0, float bowAttackTime = 0, int bowProjectileAmmount = 0, float bowProjSpeed = 0)
        {
            Name = name;
            Description = description;
            UpgradeFor = upgFor;

            playerHp = playerHP;
            playerRegenHP = playerRegen;
            playerSpeed = playerSpd;



            WhipHasWeapon = whipHasWeapon;
            WhipCooldown = whipCooldown;
            WhipDamage = whipDamage;
            WhipAttackTime = whipAttackTime;

            StaffHasWeapon = staffHasWeapon;
            StaffCooldown = staffCooldown;
            StaffDamage = staffDamage;
            StaffAttackTime = staffAttackTime;
            StaffProjectileAmmount = staffProjectileAmmount;
            StaffProjSpeed = staffProjSpeed;

            BowHasWeapon = bowHasWeapon;
            BowCooldown = bowCooldown;
            BowDamage = bowDamage;
            BowAttackTime = bowAttackTime;
            BowProjectileAmmount = bowProjectileAmmount;
            BowProjSpeed = bowProjSpeed;

            AoeHasWeapon = aoeHasWeapon;
            AoeCooldown = aoeCooldown;
            AoeDamage = aoeDamage;
            AoeScale = aoeScale;


        }
    }






    


    public void GenerateUpgrades()
    {
        allUpgrades = new List<Upgrade>();

        

        //  **������������� ������**
        allUpgrades.Add(new Upgrade("�����", "������������ ����", whipHasWeapon: true, upgFor: "Whip"));
        allUpgrades.Add(new Upgrade("��� �����", "������������ �����", staffHasWeapon: true, upgFor: "Staff"));
        allUpgrades.Add(new Upgrade("��������� ���", "������������ ���", bowHasWeapon: true, upgFor: "Bow"));
        allUpgrades.Add(new Upgrade("����� ��������", "������������ ����� �� �������", aoeHasWeapon: true, upgFor: "Aoe"));

        //�������� ������
        allUpgrades.Add(new Upgrade("�����", "�������� �������� �������������� ������ ��������", playerHP: 1, upgFor: "Man"));
        allUpgrades.Add(new Upgrade("��������������", "�������� ��������������� �������� � �������� �������", playerRegen: 0.2f, upgFor: "Man"));
        allUpgrades.Add(new Upgrade("�������� ����� ������", "�������� �������� ������", playerSpd: 0.2f, upgFor: "Man"));


        //  **���� (Whip)** 
        allUpgrades.Add(new Upgrade("�����������", "���� ������� ������ �����", whipDamage: 5, upgFor: "Whip"));
        allUpgrades.Add(new Upgrade("���� ������� �����", "����������� ����� ����� �����", whipAttackTime: +0.2f, upgFor: "Whip"));
        allUpgrades.Add(new Upgrade("������ ������", "��������� ����������� �����", whipCooldown: -0.3f, upgFor: "Whip"));
        allUpgrades.Add(new Upgrade("�������� �����", "���� ������� ������ �����, �� ������� ���������", whipDamage: 7, whipCooldown: 0.2f, upgFor: "Whip"));

        //  **����� (Staff)**
        allUpgrades.Add(new Upgrade("����������", "����� ������� ������ �����", staffDamage: 7, upgFor: "Staff"));
        allUpgrades.Add(new Upgrade("���������� �����", "������� ����������� ������", staffCooldown: -0.5f, upgFor: "Staff"));
        allUpgrades.Add(new Upgrade("������� �����", "��������� ������ ��������� +1 �����", staffProjectileAmmount: 1, upgFor: "Staff"));
        allUpgrades.Add(new Upgrade("�������� �����", "����������� �������� ������ �������� ������", staffProjSpeed: 2.5f, upgFor: "Staff"));
        allUpgrades.Add(new Upgrade("���������� ����", "����� ��������� +2 ������, �� ������ ��������", staffProjectileAmmount: 2, staffProjSpeed: -1.0f, upgFor: "Staff"));

        //  **��� (Bow)** 
        allUpgrades.Add(new Upgrade("��� ��������", "��� ������� ������ �����", bowDamage: 4, upgFor: "Bow"));
        allUpgrades.Add(new Upgrade("������� �����", "������� �������� ����� ����������", bowCooldown: -0.3f, upgFor: "Bow"));
        allUpgrades.Add(new Upgrade("������� �������", "�������� ����� �������� ������������", bowProjectileAmmount: 1, upgFor: "Bow"));
        allUpgrades.Add(new Upgrade("������ �����", "����������� �������� �����", bowProjSpeed: 3.0f, upgFor: "Bow"));
        allUpgrades.Add(new Upgrade("������� ������", "��� ������� ������ �����, �� ������ ���������", bowDamage: 6, bowProjSpeed: -1.5f, upgFor: "Bow"));

        // AoE
        allUpgrades.Add(new Upgrade( name: "Энергетический взрыв", description: "Увеличивает урон и радиус AoE-атаки, но увеличивает время перезарядки", aoeDamage: 15, aoeScale: 0.3f, aoeCooldown: 2f, upgFor: "Aoe"));
        allUpgrades.Add(new Upgrade(name: "Быстрый заряд", description: "Уменьшает время перезарядки AoE-атаки", aoeCooldown: -0.5f, upgFor: "Aoe"));
        allUpgrades.Add(new Upgrade(name: "Стабильный импульс", description: "Увеличивает радиус AoE-атаки, но немного уменьшает урон", aoeDamage: -5, aoeScale: 0.4f, upgFor: "Aoe"));
        allUpgrades.Add(new Upgrade(name: "Усиленный импульс", description: "Немного увеличивает радиус и урон AoE-атаки", aoeDamage: 3, aoeScale: 0.2f, upgFor: "Aoe"));


    }


    void Update()
    {
        
    }

    private void Start()
    {
        

        GenerateUpgrades();
    

            
        //allUpgrades[23].processUpgrade();

    }
}
