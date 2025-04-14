using Robust.Shared.Prototypes;

namespace Content.Server.Roboisseur.Roboisseur;

[RegisterComponent]
public sealed partial class RoboisseurComponent : Component
{
    [ViewVariables]
    [DataField("accumulator")]
    public float Accumulator = 0f;

    [ViewVariables(VVAccess.ReadOnly)]
    [DataField("impatient")]
    public bool Impatient { get; set; } = false;

    [ViewVariables]
    [DataField("resetTime")]
    public TimeSpan ResetTime = TimeSpan.FromMinutes(10);

    [DataField("barkAccumulator")]
    public float BarkAccumulator = 0f;

    [DataField("barkTime")]
    public TimeSpan BarkTime = TimeSpan.FromMinutes(1);

    /// <summary>
    /// Antispam.
    /// </summary>

    public TimeSpan StateTime = default!;
    public DateTime TimerStartTime { get; set; }
    public TimeSpan TimerDuration { get; } = TimeSpan.FromMinutes(25);

    [DataField("stateCD")]
    public TimeSpan StateCD = TimeSpan.FromSeconds(5);

    [ViewVariables(VVAccess.ReadWrite)]
    public EntityPrototype DesiredPrototype = default!;

    [ViewVariables(VVAccess.ReadWrite)]
    public int DoneTotal = 0;

    [DataField("timerRunNow")]
    public IReadOnlyList<string> TimerRunNow = new[]
    {
        "roboisseur-timerenablenow-1",
        "roboisseur-timerenablenow-2",
        "roboisseur-timerenablenow-3",
        "roboisseur-timerenablenow-4",
        "roboisseur-timerenablenow-5"
    };

    [DataField("demandMessages")]
    public IReadOnlyList<string> DemandMessages = new[]
    {
        "roboisseur-request-1",
        "roboisseur-request-2",
        "roboisseur-request-3",
        "roboisseur-request-4",
        "roboisseur-request-5",
        "roboisseur-request-6"
    };

    [DataField("impatientMessages")]
    public IReadOnlyList<string> ImpatientMessages = new[]
    {
        "roboisseur-request-impatient-1",
        "roboisseur-request-impatient-2",
        "roboisseur-request-impatient-3",
    };

    [DataField("demandMessagesTier2")]
    public IReadOnlyList<string> DemandMessagesTier2 = new[]
    {
        "roboisseur-request-second-1",
        "roboisseur-request-second-2",
        "roboisseur-request-second-3"
    };

    [DataField("rewardMessages")]
    public IReadOnlyList<string> RewardMessages = new[]
    {
        "roboisseur-thanks-1",
        "roboisseur-thanks-2",
        "roboisseur-thanks-3",
        "roboisseur-thanks-4",
        "roboisseur-thanks-5"
    };

    [DataField("rewardMessagesTier2")]
    public IReadOnlyList<string> RewardMessagesTier2 = new[]
    {
        "roboisseur-thanks-second-1",
        "roboisseur-thanks-second-2",
        "roboisseur-thanks-second-3",
        "roboisseur-thanks-second-4",
        "roboisseur-thanks-second-5"
    };

    [DataField("rejectMessages")]
    public IReadOnlyList<string> RejectMessages = new[]
    {
        "roboisseur-deny-1",
        "roboisseur-deny-2",
        "roboisseur-deny-3"
    };

    [DataField("tier2Protos")]
    public List<string> Tier2Protos = new()
    {
        "FoodBurgerEmpowered",
        "FoodSoupClown",
        "FoodSoupChiliClown",
        "FoodBurgerSuper",
        "FoodNoodlesCopy",
        //"FoodMothMallow",
        //"FoodPizzaCorncob",
        //"FoodPizzDonkpocket",
        "FoodSoupMonkey",
        //"FoodMothSeedSoup",
        "FoodTartGrape",
        "FoodMealCubancarp",
        "FoodMealSashimi",
        "FoodBurgerCarp",
        //"FoodMealTaco",
        //"FoodMothMacBalls",
        "FoodSoupNettle",
        "FoodBurgerDuck",
        "FoodBurgerBaseball",
        "FoodTacoFish",
        "FoodBurgerClown",
    };

    [DataField("tier3Protos")]
    public List<string> Tier3Protos = new()
    {
        "FoodSaladWatermelonFruitBowl",
        "FoodBakedCannabisBrownieBatch",
        "FoodPizzaDank",
        //"FoodBurgerBear",
        "FoodBurgerMime",
        "FoodCakeSuppermatter",
        "FoodSoupChiliCold",
        "FoodSoupBisque",
        "FoodCakeSlime",
        "FoodBurgerCrazy",
        "FoodPieFrosty",
        "FoodTartMime",
        "FoodSoupBungo",
        "FoodTartGapple",
        "FoodCakeLemoon"
    };

    [DataField("robossuierRewards")]
    public IReadOnlyList<string> RobossuierRewards = new[]
    {
        //"DrinkIceCreamGlass",
        //"FoodFrozenPopsicleOrange",
        //"FoodFrozenPopsicleBerry",
        //"FoodFrozenPopsicleJumbo",
        //"FoodFrozenSnowconeBerry",
        //"FoodFrozenSnowconeFruit",
        //"FoodFrozenSnowconeClown",
        //"FoodFrozenSnowconeMime",
        //"FoodFrozenSnowconeRainbow",
        //"FoodFrozenCornuto",
        //"FoodFrozenSundae",
        //"FoodFrozenFreezy",
        //"FoodFrozenSandwichStrawberry",
        //"FoodFrozenSandwich",
        "ClothingNeckCloakBotanistCloak",
        "ClothingNeckBlackCloak",
        "ClothingNeckCloakRose",
        "ClothingUniformJumpsuitBlueGalaxy",
        "ClothingUniformJumpsuitRedGalaxy",
        //"ClothingUniformJumpsuitStrangeBunny",
        "ClothingUniformReallyBlackSuitSkirt",
        "ClothingNeckRegalMantle",
        "ClothingHeadHatRedRog",
        "ClothingHeadHatCueball",
        "CrateHydroponicsSeedsExotic",
        "CrateVendingMachineRestockChefvendFilled",
        //"CrateFunATV",
        "CrateNPCDuck",
        "ClothingOuterHardsuitLing",
        "ClothingHeadHatFancyCrown",
        "MaterialHideBear",
        "ClothingOuterSuitIan",
        "ClothingOuterRedRacoon",
        "TeslaToy",
        "PlushieGhostRevenant",
        "Chainsaw",
        "MobMousi",
        "MechRipleyBattery",
        "ClothingBackpackDuffelHolding",
        "ClothingShoesBootsSpeed",
        "SheetPlasma",
        "CognizineChemistryBottle",
        "CombatMedipen",
        //"CloningConsoleComputerCircuitboard",
        //"CloningPodMachineCircuitboard",
        "FloorTileItemAstroGrass30",
        "FloorTileItemAstroIce30",
    };

    [DataField("blacklistedProtos")]
    public IReadOnlyList<string> BlacklistedProtos = new[]
    {
        //"FoodMothPesto",
        "FoodBurgerSpell",
        "FoodBreadBanana",
        //"FoodMothSqueakingFry",
        //"FoodMothFleetSalad",
        "FoodBreadMeatSpider",
        "FoodBurgerHuman",
        "FoodNoodlesBoiled",
        //"FoodMothOatStew",
        "FoodMeatLizardtailKebab",
        "FoodSoupTomato",
        "FoodDonkpocketGondolaWarm",
        "FoodDonkpocketBerryWarm",
        //"LockboxDecloner",
        "FoodBreadButteredToast",
        //"FoodMothCottonSoup",
        "LeavesTobaccoDried",
        "FoodSoupEyeball",
        //"FoodMothKachumbariSalad",
        "FoodMeatHumanKebab",
        "FoodMeatRatdoubleKebab",
        "FoodBurgerCorgi",
        "FoodBreadPlain",
        "FoodMeatKebab",
        "FoodBreadBun",
        "FoodBurgerCat",
        "FoodSoupTomatoBlood",
        //"FoodMothSaladBase",
        "FoodPieXeno",
        "FoodDonkpocketTeriyakiWarm",
        //"FoodMothBakedCheese",
        //"FoodMothTomatoSauce",
        //"FoodMothPizzaCotton",
        "AloeCream",
        "FoodSnackPopcorn",
        "FoodBurgerSoy",
        //"FoodMothToastedSeeds",
        //"FoodMothCornmealPorridge",
        //"FoodMothBakedCorn",
        "FoodBreadMoldySlice",
        "FoodRiceBoiled",
        //"FoodMothEyeballSoup",
        "FoodMeatRatKebab",
        "FoodBreadCreamcheese",
        "FoodSoupOnion",
        "FoodBurgerAppendix",
        "FoodBurgerRat",
        "RegenerativeMesh",
        "FoodCheeseCurds",
        "FoodDonkpocketHonkWarm",
        "FoodOatmeal",
        "FoodBreadJellySlice",
        //"FoodMothCottonSalad",
        "FoodBreadMoldy",
        "FoodDonkpocketSpicyWarm",
        "FoodCannabisButter",
        "FoodNoodles",
        "FoodBreadMeat",
        "LeavesCannabisDried",
        "FoodBurgerCheese",
        "FoodDonkpocketDankWarm",
        "FoodSpaceshroomCooked",
        "FoodMealFries",
        "MedicatedSuture",
        "FoodDonkpocketWarm",
        "FoodCakePlain",
        "DisgustingSweptSoup",
        "FoodBurgerPlain",
        "FoodBreadGarlicSlice",
        "FoodSoupMushroom",
        "FoodSoupWingFangChu",
        "FoodBreadMeatXeno",
        "FoodCakeBrain",
        "FoodBurgerBrain",
        //"FoodSaladCaesar",
        "MobBreadDog",
        "MobCatCake",
        "FoodBurgerXeno",
        "FoodPizzaDonkpocket",
        "TrashBakedBananaPeel",
        "FoodBurgerGhost",
        "FoodDonkpocketPizzaWarm",
        "FoodBurgerMothRoach",
        "FoodBurgerBear",
        "FoodSaladOlivie",
        "SalCoat",
        "SalCrab",
        "SalMime",
        "FoodTinCondMilk",
        "FoodTinPorrige",
        "FoodTinErp"  
    };
}
