using RimWorld;
using Verse;

namespace VisiblePregnancyUtil
{
    public class VisiblyPregnantPawnThingDef : ThingDef
    {
        public float  visiblePregnancyMinSeverity = 0.6666f;
        public string visiblePregnancyTexPath = null;
    }

    public class VisiblyPregnantPawn : Pawn
    {
        #region Properties
        public VisiblyPregnantPawnThingDef Def
        {
            get
            {
                return this.def as VisiblyPregnantPawnThingDef;
            }
        }
        #endregion

        private Graphic prePregGraphic = null;

        public override void Tick()
        {
            base.Tick();

            var pregHediff = this.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Pregnant);
            //Verse.Log.Message("P: " + (preggers != null ? "Yes" : "No"));
            //Verse.Log.Message("TP: " + Def.preggersTexPath);
            //if (preggers != null)
            //{
            //    Verse.Log.Message("S: " + preggers.Severity.ToString());
            //}
            if (pregHediff != null && pregHediff.Severity >= Def.visiblePregnancyMinSeverity && (prePregGraphic == null || prePregGraphic == this.Drawer.renderer.graphics.nakedGraphic) && Def.visiblePregnancyTexPath != null)
            {
                prePregGraphic = this.Drawer.renderer.graphics.nakedGraphic;
                Graphic newGraphic = GraphicDatabase.Get<Graphic_Multi>(Def.visiblePregnancyTexPath,prePregGraphic.Shader,prePregGraphic.drawSize,prePregGraphic.Color,prePregGraphic.ColorTwo,prePregGraphic.data);
                this.Drawer.renderer.graphics.nakedGraphic = newGraphic;
            }
            else if (pregHediff == null && prePregGraphic != null)
            {
                this.Drawer.renderer.graphics.nakedGraphic = prePregGraphic;
                prePregGraphic = null;
            }
        }
    }
}
