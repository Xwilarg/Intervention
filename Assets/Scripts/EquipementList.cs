public static class EquipementList
{
    /// ---------- SPECIALIZATIONS
    public static readonly Specialization speNone = new Specialization("None", "No specialization. Start with a Handgun.", 0);
    public static readonly Specialization speMed = new Specialization("Medicin", "Medical specialization, can make better use of First Aid Kit. Start with a Handgun and a First Aid Kit.", 150);
    public static readonly Specialization speEngi = new Specialization("Engineer", "Engineer specialization, can make small cover with the surronding furniture. Start with a Handgun and a Retractable Shovel.", 150);
    public static readonly Specialization speMage = new Specialization("Mage", "Mage specialization, can throw spells. Can only use handguns. Start with an elemental gem.", 250);
    public static readonly Specialization speSniper = new Specialization("Sniper", "Sniper specialization, will not go inside the building and provide cover from a near building. May only shoot if the building have windows.", 250);

    /// ---------- SPELLS
    public static readonly Spell spellWall = new Spell("Wall", "Raise the floor to create a wall . Only work at the lowest floor.", 0);
    public static readonly Spell spellStabilize = new Spell("Stabilize", "Stabilize the bleeding of a soldier.", 0);
    public static readonly Spell spellHeal = new Spell("Heal", "Increase the recover speed of a soldier.", 0);
    public static readonly Spell spellProjectile = new Spell("Projectile", "Create a projectile from the surronding environment and throw it towards the enemy at high speed.", 0);
    public static readonly Spell spellDispell = new Spell("Dispell", "Neutralize an enemy spell.", 0);

    /// ---------- TROOPS
    public static readonly Unit recruit = new Unit("Recruit", "Freshly recruited soldier, have no experience on the field.\nDon't expect too much from them.", 50, Unit.Grade.PV1);
    public static readonly Unit secondClass = new Unit("Second Class", "Soldier that only did a few missions on the field.", 150, Unit.Grade.PV2);
    public static readonly Unit firstClass = new Unit("First Class", "Soldier that already have a lot of experience on the field. Second class soldier and higher can have a specialization.", 200, Unit.Grade.PFC);
    public static readonly Unit corporal = new Unit("Corporal", "Along with being an experienced soldier, have some knowledge in leadership.", 300, Unit.Grade.CPL);
    public static readonly Unit sergeant = new Unit("Sergeant", "Veterant soldier who also have a lot of experience in leadership.", 400, Unit.Grade.SGT);

    /// ---------- EQUIPEMENTS
    public static readonly Weapon stungun = new Weapon("Stungun", "An incapacitating weapon that must be used in close contact.", 10);
    public static readonly Weapon tazer = new Weapon("Stungun", "An incapacitating gun that must be used in short range.", 25);
    public static readonly Weapon handgun = new Weapon("Handgun", "A standard pistol", 0);
    public static readonly Weapon assaultGun = new Weapon("Assault Rifle", "A standard rifle with a good rate of fire. Usually the basic weapon given to a soldier.", 50);
    public static readonly Weapon smg = new Weapon("Submachine Gun", "A light automatic gun, less powerful than an assault gun. Is however a good weapon for closer quarter combat since they are easier to manipulate.", 40);
    public static readonly Weapon shotgun = new Weapon("Shotgun", "A powerful weapon that shoot many projectile. Really useful for close quarter combat.", 75);
    public static readonly Weapon hmg = new Weapon("Heavy Machine Gun", "A heavy gun that need to be deployed before being used. Really useful to defend a position.", 150);

    public static readonly Protection baslisticVest = new Protection("Combat Vest", "Protect the torso against light guns and others small projectiles.", 25);
    public static readonly Protection baslisticHelmet = new Protection("Combat Helmet", "Protect the head against light guns and others small projectiles.", 35);
    public static readonly Protection balisticShield = new Protection("Balistic Shield", "A shield that protect against bullets. May only be used with a handgun.", 70);

    public static readonly Utilitary explosiveGrenade = new Utilitary("Explosive Grenade", "A grenade that can do explosive damage in a small zone.", 20);
    public static readonly Utilitary flashGrenade = new Utilitary("Flash Grenade", "A grenade that temporarily blind people in a small zone.", 25);
    public static readonly Utilitary antiMagicGrenade = new Utilitary("Anti Magic Grenade", "A grenade that do an small antic magic area where thrown.", 100);
    public static readonly Utilitary claymore = new Utilitary("Claymore", "A small device that shoot metal balls when someone trip on a wire.", 150);

    public static readonly Utilitary firstAidKit = new Utilitary("First Aid Kit", "A medical kit that can stabilize in critical state.", 20);
    public static readonly Utilitary medicalKit = new Utilitary("Medical Kit", "A full medical kit that can treat even the worse injuries. May only be use by a medecin", 50);
    public static readonly Utilitary reanimationKit = new Utilitary("Reanimation Kit", "A one use reanimation kit, allow to save someone whose heart stop beating. May only be use by a medecin", 75);
    public static readonly Utilitary defuseKit = new Utilitary("Defuse Kit", "Allow to desarm bombs. May only be used by an engineer.", 20);
    public static readonly Utilitary doorCamera = new Utilitary("Under Door Camera", "Small camera you put under a door to observe the inside of a room.", 150);
    public static readonly Utilitary retractableShovel = new Utilitary("Retractable Shovel", "Allow to remove debris or break furniture in small parts.", 20);
    public static readonly Utilitary nightVision = new Utilitary("Night Vision", "Improve vision in the dark.", 100);
    public static readonly Utilitary magicAnalyzer = new Utilitary("Magic Analyzer", "Analyze the magic concentration in the near air.", 200);

    /// ---------- LOGISTICS
    public static readonly Squad basicSquad = new Squad("Intervention Squad", "A basic intervention squad", new AEquipement[] { corporal, secondClass, secondClass, secondClass, assaultGun, assaultGun, assaultGun, assaultGun });
    public static readonly Squad trainedSquad = new Squad("Trained Intervention Squad", "An intervention squad with high level soldiers.", new AEquipement[] { sergeant, firstClass, firstClass, firstClass, assaultGun, assaultGun, assaultGun, assaultGun });
    //public static readonly Squad defenseSquad = new Squad("Defense Squad", "A squad formed to defend a position", new AEquipement[] { carporal, secondClass, secondClass, secondClass, assaultGun, assaultGun, assaultGun, hmg, claymore, claymore });
    //public static readonly Squad offensiveSquad = new Squad("Offensive Squad", "A squad formed for highly offensive attack", new AEquipement[] { carporal, secondClass, secondClass, secondClass, smg, smg, smg, shotgun, explosiveGrenade, explosiveGrenade, flashGrenade, flashGrenade });
}
