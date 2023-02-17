//Equipmentmenucontroller setzt bei enable den swordbutton/grid als aktiv + Static.currentequipmentbutton = 0
//Static.currentequipmentbutton bestimmt den itemtype mit dem man gerade arbeitet. Wird ein Slotbutton ausgewählt ändert sich dieser Wert
//Equipcharselection setzt den Char(Static.currentequipmentchar)
//bei jedem Buttonswitch/Charswitch wird triggerbuttongecalled. Bei diesem call wird das resetonpointlayer getriggert damit die stats resetet werden. Außerdem wird die frage für das equipte item gesetzt
//wenn man im startmenu newgame klickt wird setstaticnull getriggert. Hier wird jedem itemslot ein nullobject zugewiesen
//im inventoryUI wird das item für Chooseitem/Chooseweapon zugewiesen
//In Chooseitem/Weapon werden statstext + onpointenter/onpointexit called und das itemequipt
//upgradUI + Upgradecontroller = itemupgrades
