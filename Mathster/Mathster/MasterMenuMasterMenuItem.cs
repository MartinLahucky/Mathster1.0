using System;

namespace Mathster
{

    public class MasterMenuMasterMenuItem
    {
        public MasterMenuMasterMenuItem()
        {
            TargetType = typeof(MasterMenuMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}