using UnityEngine;
using System.Collections.Generic;
using Script.Config.ConfigObj;
using System;

namespace Script.Config
{
    public class ConfigManager
    {
        Dictionary<string, Type> types;

        public ConfigManager()
        {
            types = new Dictionary<string, Type>();
            RegType<ItemConfig>();
            RegType<PartUpgradeTimeConfig>();
        }
        
        public void Parse(byte[] bytes)
        {
            BytesStream stream = new BytesStream(bytes);
            while (stream.bytesAvailable > 0)
            {
                string key = stream.ReadString(stream.ReadInt16());
                if (types.ContainsKey(key))
                {
                    Type type = types [key];
                    BytesStream newStream = new BytesStream(stream.ReadBytes(stream.ReadUInt16()));
                    type.GetMethod("Parse").Invoke(null, new object[1] { newStream });
                }
            }
        }

        void RegType<T>()
        {
            Type type = typeof(T);
            types.Add(type.GetField("urlKey").GetValue(null) as String, type);
        }

        public ItemConfig GetItem(int id)
        {
            return ItemConfig.Get(id);
        }
        
        public PartUpgradeTimeConfig GetUpgradeTime(int id)
        {
            return PartUpgradeTimeConfig.Get(id);
        }
    }
}
