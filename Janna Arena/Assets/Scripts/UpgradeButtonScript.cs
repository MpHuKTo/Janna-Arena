using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UpgradesScript;
using System;




public class UpgradeButtonScript : MonoBehaviour
{

    private List<Upgrade> allUpgrades;

    private Experience_Level_Controller experience_Level_Controller;
    public PlayerController player_Controller;
    private Upgrade buttonCurUpgrade;
    private Button curButton;

    [SerializeField]
    public GameObject UpgUI;

    private Upgrade curUpgrade;
    

    public TMP_Text UpgradeName, UpgradeDesc;
    private UpgradesScript upgScript;

    public Image weaponIcon;


    public TMP_Text[] upgradeDescs;
    public TMP_Text[] newValues;

    private void Start()
    {
        experience_Level_Controller = FindObjectOfType<Experience_Level_Controller>();
        player_Controller = FindObjectOfType<PlayerController>();

        
        curButton = GetComponent<Button>();
        upgScript = FindObjectOfType<UpgradesScript>();

        

    }

    public void updateButton(Upgrade pickedUpgrade)
    {
       upgScript = FindObjectOfType<UpgradesScript>(); 
        allUpgrades = upgScript.allUpgrades;

        curUpgrade = pickedUpgrade;
        buttonCurUpgrade = pickedUpgrade;


        UpgradeName.text = pickedUpgrade.Name;
        UpgradeDesc.text = pickedUpgrade.Description;

        
            player_Controller = FindObjectOfType<PlayerController>();

        if (buttonCurUpgrade.UpgradeFor == "Man") { weaponIcon.sprite = player_Controller.spriteRenderer.sprite; }
        if (buttonCurUpgrade.UpgradeFor == "Whip") { weaponIcon.sprite = player_Controller.Whip.Icon; }
        if (buttonCurUpgrade.UpgradeFor == "Staff") { weaponIcon.sprite = player_Controller.Staff.Icon; }
        if (buttonCurUpgrade.UpgradeFor == "Bow") { weaponIcon.sprite = player_Controller.Bow.Icon; }
        if (buttonCurUpgrade.UpgradeFor == "Aoe") { weaponIcon.sprite = player_Controller.AoeWpn.Icon; }


        for (int i = 0; i < 9; i++) 
        {
            upgradeDescs[i].text = "";
            newValues[i].text = "";

            

        }

        int iterator = 0;

        //Тексты описания апгрейдов справа


        //Апгрейды игрока
        if (pickedUpgrade.playerHp != 0)
        { upgradeDescs[iterator].text = "Здоровье игрока " + player_Controller.initialHealth + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.initialHealth + pickedUpgrade.playerHp);
            if (player_Controller.initialHealth < (player_Controller.initialHealth + pickedUpgrade.playerHp)) { newValues[iterator].color = Color.red; } iterator++; }
        if (pickedUpgrade.playerRegenHP != 0)
        {  upgradeDescs[iterator].text = "Восстановление здоровья " + player_Controller.hpRegen + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.hpRegen + pickedUpgrade.playerRegenHP);
            if (player_Controller.hpRegen > (player_Controller.hpRegen + pickedUpgrade.playerRegenHP)) { newValues[iterator].color = Color.red; }  iterator++; }
        if (pickedUpgrade.playerSpeed != 0)
        { upgradeDescs[iterator].text = "Скорость игрока " + player_Controller.speed + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.speed + pickedUpgrade.playerSpeed);
            if (player_Controller.speed > (player_Controller.speed + pickedUpgrade.playerSpeed)) { newValues[iterator].color = Color.red; } iterator++;}


        // Апгрейд кнута (Whip)
        if (pickedUpgrade.WhipHasWeapon) { upgradeDescs[iterator].text = "Владение оружием " + player_Controller.Whip.HasWeapon + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Whip.HasWeapon || pickedUpgrade.WhipHasWeapon); iterator++; }
        if (pickedUpgrade.WhipCooldown != 0) { upgradeDescs[iterator].text = "Скорость перезарядки " + player_Controller.Whip.Cooldown + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Whip.Cooldown + pickedUpgrade.WhipCooldown);
            if (player_Controller.Whip.Cooldown < (player_Controller.Whip.Cooldown + pickedUpgrade.WhipCooldown)) { newValues[iterator].color = Color.red; } iterator++; }
        if (pickedUpgrade.WhipDamage != 0) { upgradeDescs[iterator].text = "Урон от удара " + player_Controller.Whip.Damage + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Whip.Damage + pickedUpgrade.WhipDamage); 
            if(player_Controller.Whip.Damage > (player_Controller.Whip.Damage + pickedUpgrade.WhipDamage)) { newValues[iterator].color = Color.red; }  iterator++; }
        if (pickedUpgrade.WhipAttackTime != 0) { upgradeDescs[iterator].text = "Продолжительность удара " + player_Controller.Whip.AttackTime + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Whip.AttackTime + pickedUpgrade.WhipAttackTime);
            if (player_Controller.Whip.AttackTime > (player_Controller.Whip.AttackTime + pickedUpgrade.WhipAttackTime)) { newValues[iterator].color = Color.red; }iterator++; }


        // Апгрейд посоха (Staff)
        if (pickedUpgrade.StaffHasWeapon) { upgradeDescs[iterator].text = "Владение оружием " + player_Controller.Staff.HasWeapon + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Staff.HasWeapon || pickedUpgrade.StaffHasWeapon); iterator++; }
        if (pickedUpgrade.StaffCooldown != 0) { upgradeDescs[iterator].text = "Скорость перезарядки " + player_Controller.Staff.Cooldown + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Staff.Cooldown + pickedUpgrade.StaffCooldown);
            if (player_Controller.Staff.Cooldown < (player_Controller.Staff.Cooldown + pickedUpgrade.StaffCooldown)) { newValues[iterator].color = Color.red; } iterator++; }
        if (pickedUpgrade.StaffDamage != 0) {upgradeDescs[iterator].text = "Урон от снаряда " + player_Controller.Staff.Damage + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Staff.Damage + pickedUpgrade.StaffDamage);
            if (player_Controller.Staff.Damage > (player_Controller.Staff.Damage + pickedUpgrade.StaffDamage)) { newValues[iterator].color = Color.red; } iterator++; }
        if (pickedUpgrade.StaffAttackTime != 0) { upgradeDescs[iterator].text = "Время жизни снаряда " + player_Controller.Staff.AttackTime + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Staff.AttackTime + pickedUpgrade.StaffAttackTime);
            if (player_Controller.Staff.AttackTime > (player_Controller.Staff.AttackTime + pickedUpgrade.StaffAttackTime)) { newValues[iterator].color = Color.red; } iterator++;} 
        if (pickedUpgrade.StaffProjectileAmmount != 0) { upgradeDescs[iterator].text = "Количество снарядов " + player_Controller.Staff.ProjectileAmmount + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Staff.ProjectileAmmount + pickedUpgrade.StaffProjectileAmmount);
            if (player_Controller.Staff.ProjectileAmmount > (player_Controller.Staff.ProjectileAmmount + pickedUpgrade.StaffProjectileAmmount)) { newValues[iterator].color = Color.red; } iterator++; }
        if (pickedUpgrade.StaffProjSpeed != 0) { upgradeDescs[iterator].text = "Скорость снарядов " + player_Controller.StaffProjSpeed + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.StaffProjSpeed + pickedUpgrade.StaffProjSpeed);
            if (player_Controller.StaffProjSpeed > (player_Controller.StaffProjSpeed + pickedUpgrade.StaffProjSpeed)) { newValues[iterator].color = Color.red; } iterator++; }

        // Апгрейд лука (Bow)
        if (pickedUpgrade.BowHasWeapon) { upgradeDescs[iterator].text = "Владение оружием " + player_Controller.Bow.HasWeapon + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Bow.HasWeapon || pickedUpgrade.BowHasWeapon); iterator++; }
        if (pickedUpgrade.BowCooldown != 0) { upgradeDescs[iterator].text = "Скорость перезарядки " + player_Controller.Bow.Cooldown + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Bow.Cooldown + pickedUpgrade.BowCooldown);
            if (player_Controller.Bow.Cooldown < (player_Controller.Bow.Cooldown + pickedUpgrade.BowCooldown)) { newValues[iterator].color = Color.red; } iterator++; }
        if (pickedUpgrade.BowDamage != 0) { upgradeDescs[iterator].text = "Урон от стрелы " + player_Controller.Bow.Damage + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Bow.Damage + pickedUpgrade.BowDamage);
            if (player_Controller.Bow.Damage > (player_Controller.Bow.Damage + pickedUpgrade.BowDamage)) { newValues[iterator].color = Color.red; } iterator++; }
        if (pickedUpgrade.BowAttackTime != 0) { upgradeDescs[iterator].text = "Время жизни стрелы " + player_Controller.Bow.AttackTime + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Bow.AttackTime + pickedUpgrade.BowAttackTime);
            if (player_Controller.Bow.AttackTime > (player_Controller.Bow.AttackTime + pickedUpgrade.BowAttackTime)) { newValues[iterator].color = Color.red; } iterator++; }
        if (pickedUpgrade.BowProjectileAmmount != 0) { upgradeDescs[iterator].text = "Количество стрел " + player_Controller.Bow.ProjectileAmmount + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.Bow.ProjectileAmmount + pickedUpgrade.BowProjectileAmmount);
            if (player_Controller.Bow.ProjectileAmmount > (player_Controller.Bow.ProjectileAmmount + pickedUpgrade.BowProjectileAmmount)) { newValues[iterator].color = Color.red; }iterator++; }
        if (pickedUpgrade.BowProjSpeed != 0) { upgradeDescs[iterator].text = "Скорость стрел " + player_Controller.BowProjSpeed + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.BowProjSpeed + pickedUpgrade.BowProjSpeed); 
            if (player_Controller.BowProjSpeed > (player_Controller.BowProjSpeed + pickedUpgrade.BowProjSpeed)) { newValues[iterator].color = Color.red; } iterator++; }


        //Апгрейды атаки по области

        if (pickedUpgrade.AoeHasWeapon) { upgradeDescs[iterator].text = "Владение оружием " + player_Controller.AoeWpn.HasWeapon + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.AoeWpn.HasWeapon || pickedUpgrade.AoeHasWeapon); iterator++; }
        if (pickedUpgrade.AoeCooldown != 0) {
            upgradeDescs[iterator].text = "Скорость атаки " + player_Controller.AoeWpn.Cooldown + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.AoeWpn.Cooldown + pickedUpgrade.AoeCooldown);
            if (player_Controller.AoeWpn.Cooldown < (player_Controller.AoeWpn.Cooldown + pickedUpgrade.AoeCooldown)) { newValues[iterator].color = Color.red; }  iterator++;   }
        if (pickedUpgrade.AoeDamage != 0)  {
            upgradeDescs[iterator].text = "Урон от области " + player_Controller.AoeWpn.Damage + " -> "; newValues[iterator].text = Convert.ToString(player_Controller.AoeWpn.Damage + pickedUpgrade.AoeDamage);
            if (player_Controller.AoeWpn.Damage > (player_Controller.AoeWpn.Damage + pickedUpgrade.AoeDamage)) { newValues[iterator].color = Color.red; }  iterator++;   }
        if (pickedUpgrade.AoeScale != 0) { AoEScript Aoe = FindObjectOfType<AoEScript>();
            upgradeDescs[iterator].text = "Размер области атаки " + Aoe.transform.localScale.x + " -> "; newValues[iterator].text = Convert.ToString(Aoe.transform.localScale.x + pickedUpgrade.AoeScale);
            if (Aoe.transform.localScale.x > (Aoe.transform.localScale.x + pickedUpgrade.AoeScale)) { newValues[iterator].color = Color.red; }  iterator++;    }

    }



    
    

    public void clickTheButton()
    {
        curUpgrade.processUpgrade();
        Time.timeScale = 1f;
        UpgUI.SetActive(false);
    }
}
