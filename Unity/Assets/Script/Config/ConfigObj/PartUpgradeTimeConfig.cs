using System.Collections.Generic;
using Script.Config;
/**
 * Data/道具表.xlsx - 升阶时间
 * 自动生成，请务修改
 */
namespace Script.Config.ConfigObj
{
	public class PartUpgradeTimeConfig
	{
		/** 部件ID */
		public int Id;
		/** 名称 */
		public string PartName;
		/** 升2时间 */
		public int Advanced2;
		/** 升3时间 */
		public int Advanced3;
		/** 升4时间 */
		public int Advanced4;
		/** 升5时间 */
		public int Advanced5;
		/** 升6时间 */
		public int Advanced6;
		/** 升7时间 */
		public int Advanced7;
		/** 升8时间 */
		public int Advanced8;
		/** 升9时间 */
		public int Advanced9;
		
		static public readonly string urlKey = "PartUpgradeTimeConfig";
        static Dictionary<int, PartUpgradeTimeConfig> Dictionary;
		static public void Parse(BytesStream bytes)
		{
			Dictionary = new Dictionary<int, PartUpgradeTimeConfig>();
			while (bytes.bytesAvailable > 0)
			{
				PartUpgradeTimeConfig vo = new PartUpgradeTimeConfig();
				vo.Id = bytes.ReadInt32();
				vo.PartName = bytes.ReadString(bytes.ReadInt16());
				vo.Advanced2 = bytes.ReadInt32();
				vo.Advanced3 = bytes.ReadInt32();
				vo.Advanced4 = bytes.ReadInt32();
				vo.Advanced5 = bytes.ReadInt32();
				vo.Advanced6 = bytes.ReadInt32();
				vo.Advanced7 = bytes.ReadInt32();
				vo.Advanced8 = bytes.ReadInt32();
				vo.Advanced9 = bytes.ReadInt32();
				
				Dictionary.Add(vo.Id, vo);
			}
		}
		static public PartUpgradeTimeConfig Get(int id)
        {
            if (Dictionary.ContainsKey(id))
            {
                return Dictionary[id];
            }
            return null;
        }
	}
}