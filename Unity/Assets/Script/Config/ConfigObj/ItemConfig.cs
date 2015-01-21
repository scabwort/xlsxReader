using System.Collections.Generic;
using Script.Config;
/**
 * Data/道具表.xlsx - 道具总表
 * 自动生成，请务修改
 */
namespace Script.Config.ConfigObj
{
	public class ItemConfig
	{
		/** 道具ID */
		public int Id;
		/** 图标ID */
		public int Icon;
		/** 稀有度 */
		public int Quality;
		/** 品阶 */
		public int Level;
		/** 名称 */
		public string Name;
		/** 数量注明 */
		public short ItemNum;
		/** 类型 */
		public byte ItemType;
		/** 细分类 */
		public byte MaterialSort;
		/** 是否自动使用 */
		public byte AutoUse;
		/** 叠加上限 */
		public int MaxStack;
		/** 出售价格 */
		public int SellPrice;
		/** 道具说明 */
		public string Descript;
		
		static public readonly string urlKey = "ItemConfig";
        static Dictionary<int, ItemConfig> Dictionary;
		static public void Parse(BytesStream bytes)
		{
			Dictionary = new Dictionary<int, ItemConfig>();
			while (bytes.bytesAvailable > 0)
			{
				ItemConfig vo = new ItemConfig();
				vo.Id = bytes.ReadInt32();
				vo.Icon = bytes.ReadInt32();
				vo.Quality = bytes.ReadInt32();
				vo.Level = bytes.ReadInt32();
				vo.Name = bytes.ReadString(bytes.ReadInt16());
				vo.ItemNum = bytes.ReadInt16();
				vo.ItemType = bytes.ReadByte();
				vo.MaterialSort = bytes.ReadByte();
				vo.AutoUse = bytes.ReadByte();
				vo.MaxStack = bytes.ReadInt32();
				vo.SellPrice = bytes.ReadInt32();
				vo.Descript = bytes.ReadString(bytes.ReadInt16());
				
				Dictionary.Add(vo.Id, vo);
			}
		}
		static public ItemConfig Get(int id)
        {
            if (Dictionary.ContainsKey(id))
            {
                return Dictionary[id];
            }
            return null;
        }
	}
}