using System;
using YTS.Engine.ShineUpon;

namespace YTS.Engine.Config
{
    public abstract class AbsConfig : AbsShineUponIni
    {
        public AbsConfig() : base() { }

        public override string GetPathFolder() {
            return string.Format("{0}/ini_Config", base.GetPathFolder());
        }
    }
}
