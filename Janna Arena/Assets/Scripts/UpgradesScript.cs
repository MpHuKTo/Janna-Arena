using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradesScript : MonoBehaviour
{
    public List<Upgrade> allUpgrades;

    

    public class Upgrade
    {
        //Название и описание апгрейда
        public string Name;
        public string Description;
        public string UpgradeFor;


        //Переменные для апгрейда игрока
        public int playerHp;
        public float playerRegenHP;
        public float playerSpeed;




        //Переменные для апгрейда кнута
        public bool WhipHasWeapon = false;
        public float WhipCooldown = 0;
        public float WhipDamage = 0;
        //Сколько кнут остается на экране (лучше не менять)
        public float WhipAttackTime = 0;

        //Переменные для апгрейда посоха
        public bool StaffHasWeapon = false;
        public float StaffCooldown = 0;
        public float StaffDamage = 0;
        //Насколько долго живет пулька посоха
        public float StaffAttackTime = 0;
        //Количество выстреливаемых подряд пулек
        public int StaffProjectileAmmount = 0;
        //Скорость пулек
        public float StaffProjSpeed = 0;

        //Переменные для апгрейда лука
        public bool BowHasWeapon = false;
        public float BowCooldown = 0;
        public float BowDamage = 0;
        //Сколько живет пулька лука
        public float BowAttackTime = 0;
        public int BowProjectileAmmount = 0;
        public float BowProjSpeed = 0;

        //Апгрейды AoE атаки
        public bool AoeHasWeapon = false;
        public float AoeCooldown = 0;
        public float AoeDamage = 0;
        public float AoeScale = 0;
        

        PlayerController PlayerController = FindObjectOfType<PlayerController>();


        public void processUpgrade()
        {

            //Апгрейды игрока
            if (playerHp != 0) { PlayerController.health += playerHp; PlayerController.initialHealth += playerHp;  }
            if (playerRegenHP != 0) PlayerController.hpRegen += playerRegenHP;
            if (playerSpeed != 0) PlayerController.speed += playerSpeed;


                // Апгрейд кнута (Whip)
                if (WhipHasWeapon) PlayerController.Whip.HasWeapon = true;
                if (WhipCooldown != 0) PlayerController.Whip.Cooldown += WhipCooldown;
                if (WhipDamage != 0) PlayerController.Whip.Damage += WhipDamage;
                if (WhipAttackTime != 0) PlayerController.Whip.AttackTime += WhipAttackTime;

                // Апгрейд посоха (Staff)
                if (StaffHasWeapon) PlayerController.Staff.HasWeapon = true;
                if (StaffCooldown != 0) PlayerController.Staff.Cooldown += StaffCooldown;
                if (StaffDamage != 0) PlayerController.Staff.Damage += StaffDamage;
                if (StaffAttackTime != 0) PlayerController.Staff.AttackTime += StaffAttackTime;
                if (StaffProjectileAmmount != 0) PlayerController.Staff.ProjectileAmmount += StaffProjectileAmmount;
                if (StaffProjSpeed != 0) PlayerController.StaffProjSpeed += StaffProjSpeed;

                // Апгрейд лука (Bow)
                if (BowHasWeapon) PlayerController.Bow.HasWeapon = true;
                if (BowCooldown != 0) PlayerController.Bow.Cooldown += BowCooldown;
                if (BowDamage != 0) PlayerController.Bow.Damage += BowDamage;
                if (BowAttackTime != 0) PlayerController.Bow.AttackTime += BowAttackTime;
                if (BowProjectileAmmount != 0) PlayerController.Bow.ProjectileAmmount += BowProjectileAmmount;
                if (BowProjSpeed != 0) PlayerController.BowProjSpeed     += BowProjSpeed;

                //Апгрейды AoE
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

        

        //  **Разблокировка оружия**
        allUpgrades.Add(new Upgrade("Аркан", "Разблокирует кнут", whipHasWeapon: true, upgFor: "Whip"));
        allUpgrades.Add(new Upgrade("Том магии", "Разблокирует посох", staffHasWeapon: true, upgFor: "Staff"));
        allUpgrades.Add(new Upgrade("Охотничий лук", "Разблокирует лук", bowHasWeapon: true, upgFor: "Bow"));
        allUpgrades.Add(new Upgrade("Атака близости", "Разблокирует атаку по области", aoeHasWeapon: true, upgFor: "Aoe"));

        //Апгрейды Игрока
        allUpgrades.Add(new Upgrade("Силач", "Персонаж получает дополнительную ячейку здоровья", playerHP: 1, upgFor: "Man"));
        allUpgrades.Add(new Upgrade("Восстановление", "Персонаж восстанавливает здоровье с течением времени", playerRegen: 0.2f, upgFor: "Man"));
        allUpgrades.Add(new Upgrade("Скорость всему голова", "Ускоряет движение игрока", playerSpd: 0.2f, upgFor: "Man"));


        //  **Кнут (Whip)** 
        allUpgrades.Add(new Upgrade("Разрубатель", "Кнут наносит больше урона", whipDamage: 5, upgFor: "Whip"));
        allUpgrades.Add(new Upgrade("Кнут долгого удара", "Увеличивает время атаки кнута", whipAttackTime: +0.2f, upgFor: "Whip"));
        allUpgrades.Add(new Upgrade("Гибкий клинок", "Уменьшает перезарядку кнута", whipCooldown: -0.3f, upgFor: "Whip"));
        allUpgrades.Add(new Upgrade("Кровавый хлыст", "Кнут наносит больше урона, но атакует медленнее", whipDamage: 7, whipCooldown: 0.2f, upgFor: "Whip"));

        //  **Посох (Staff)**
        allUpgrades.Add(new Upgrade("Армагеддон", "Посох наносит больше урона", staffDamage: 7, upgFor: "Staff"));
        allUpgrades.Add(new Upgrade("Скоростной посох", "Снижает перезарядку посоха", staffCooldown: -0.5f, upgFor: "Staff"));
        allUpgrades.Add(new Upgrade("Двойной посох", "Позволяет посоху выпускать +1 сферы", staffProjectileAmmount: 1, upgFor: "Staff"));
        allUpgrades.Add(new Upgrade("Огненный шквал", "Увеличивает скорость полета снарядов посоха", staffProjSpeed: 2.5f, upgFor: "Staff"));
        allUpgrades.Add(new Upgrade("Магическая буря", "Посох выпускает +2 снаряд, но теряет скорость", staffProjectileAmmount: 2, staffProjSpeed: -1.0f, upgFor: "Staff"));

        //  **Лук (Bow)** 
        allUpgrades.Add(new Upgrade("Лук охотника", "Лук наносит больше урона", bowDamage: 4, upgFor: "Bow"));
        allUpgrades.Add(new Upgrade("Быстрый натяг", "Снижает задержку между выстрелами", bowCooldown: -0.3f, upgFor: "Bow"));
        allUpgrades.Add(new Upgrade("Двойной выстрел", "Стреляет двумя стрелами одновременно", bowProjectileAmmount: 1, upgFor: "Bow"));
        allUpgrades.Add(new Upgrade("Стрела ветра", "Увеличивает скорость стрел", bowProjSpeed: 3.0f, upgFor: "Bow"));
        allUpgrades.Add(new Upgrade("Тяжелые стрелы", "Лук наносит больше урона, но стрелы медленнее", bowDamage: 6, bowProjSpeed: -1.5f, upgFor: "Bow"));

        // AoE
        allUpgrades.Add(new Upgrade( name: "Р­РЅРµСЂРіРµС‚РёС‡РµСЃРєРёР№ РІР·СЂС‹РІ", description: "РЈРІРµР»РёС‡РёРІР°РµС‚ СѓСЂРѕРЅ Рё СЂР°РґРёСѓСЃ AoE-Р°С‚Р°РєРё, РЅРѕ СѓРІРµР»РёС‡РёРІР°РµС‚ РІСЂРµРјСЏ РїРµСЂРµР·Р°СЂСЏРґРєРё", aoeDamage: 15, aoeScale: 0.3f, aoeCooldown: 2f, upgFor: "Aoe"));
        allUpgrades.Add(new Upgrade(name: "Р‘С‹СЃС‚СЂС‹Р№ Р·Р°СЂСЏРґ", description: "РЈРјРµРЅСЊС€Р°РµС‚ РІСЂРµРјСЏ РїРµСЂРµР·Р°СЂСЏРґРєРё AoE-Р°С‚Р°РєРё", aoeCooldown: -0.5f, upgFor: "Aoe"));
        allUpgrades.Add(new Upgrade(name: "РЎС‚Р°Р±РёР»СЊРЅС‹Р№ РёРјРїСѓР»СЊСЃ", description: "РЈРІРµР»РёС‡РёРІР°РµС‚ СЂР°РґРёСѓСЃ AoE-Р°С‚Р°РєРё, РЅРѕ РЅРµРјРЅРѕРіРѕ СѓРјРµРЅСЊС€Р°РµС‚ СѓСЂРѕРЅ", aoeDamage: -5, aoeScale: 0.4f, upgFor: "Aoe"));
        allUpgrades.Add(new Upgrade(name: "РЈСЃРёР»РµРЅРЅС‹Р№ РёРјРїСѓР»СЊСЃ", description: "РќРµРјРЅРѕРіРѕ СѓРІРµР»РёС‡РёРІР°РµС‚ СЂР°РґРёСѓСЃ Рё СѓСЂРѕРЅ AoE-Р°С‚Р°РєРё", aoeDamage: 3, aoeScale: 0.2f, upgFor: "Aoe"));


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
