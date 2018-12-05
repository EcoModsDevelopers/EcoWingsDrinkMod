using Eco.Gameplay.Components;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Networking;
using Eco.Shared.Serialization;

namespace EcoWingsDrinkMod
{

    [Serialized]
    [Weight(100)]
    public partial class EcoWingsDrinkItem : FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("EcoWings Drink"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("EcoWingsDrink thats what you need."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 15, Fat = 8, Protein = 3, Vitamins = 0 };
        public override float Calories { get { return 600; } }
        public override Nutrients Nutrition { get { return nutrition; } }

        public override void OnUsed(Player player, ItemStack itemStack)
        {
            base.OnUsed(player, itemStack);
            player.RPC("ToggleFly", player.User.Client);
        }
    }

    [RequiresSkill(typeof(MolecularGastronomySkill), 1)]
    public partial class EcoWingsDrinkRecipe : Recipe
    {
        public EcoWingsDrinkRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<EcoWingsDrinkItem>(),

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WheatItem>(typeof(MolecularGastronomyEfficiencySkill), 30, MolecularGastronomyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<TallowItem>(typeof(MolecularGastronomyEfficiencySkill), 3, MolecularGastronomyEfficiencySkill.MultiplicativeStrategy)
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(EcoWingsDrinkRecipe), Item.Get<EcoWingsDrinkItem>().UILink(), 5, typeof(MolecularGastronomySpeedSkill));
            this.Initialize(Localizer.DoStr("EcoWings Drink"), typeof(EcoWingsDrinkRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}
