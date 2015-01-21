using System.Collections.Generic;
using Script.Config;
/**
 * $Info
 * 自动生成，请务修改
 */
namespace Script.Config.ConfigObj
{
	public class $ClsName
	{
		$Prop
		static public readonly string urlKey = "$ClsName";
        static $DicType Dictionary;
		static public void Parse(BytesStream bytes)
		{
			Dictionary = new $DicType();
			while (bytes.bytesAvailable > 0)
			{
				$ClsName vo = new $ClsName();
				$Reader
				$DicAdd
			}
		}
		static public $ClsName Get($KeyType id)
        {
            $ifContain
            {
                return Dictionary[id];
            }
            return null;
        }
	}
}||/** $Info */
		public $Type $Prop;
		||vo.$Prop = $Reader;
				||Dictionary.Add(vo.$DicKey, vo);||Dictionary.Add(vo);||if (Dictionary.ContainsKey(id))||if (Dictionary.Count > id)