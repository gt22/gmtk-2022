using Resources;

namespace GameActions.CustomActions
{
    public class ForestGatherAction : SimpleAction
    {
        protected override ResourceStack Cost { get; } = new()
        {
            Manpower = 1
        };

        protected override ResourceStack Effect { get; } = new()
        {
            Materials = 3
        };

        public override string Label { get; } = "Gather";
        public override string ReqDescription { get; } = "-1 Manpower";
        public override string EffectDescription { get; } = "+3 Materials";
    }
}