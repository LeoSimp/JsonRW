using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonRW
{
    public class JRW_Helper
    {
        private static string JReadValue(string FileFullName, string PropertyName)
        {
            JObject jsonObj = FileToJ(FileFullName);
            if (jsonObj.ContainsKey(PropertyName))
            {
                return jsonObj[PropertyName].ToString();
            }
            return null;

        }

        private static string JReadValue(string FileFullName, string PropertyName, string CPropertyName)
        {
            JObject jsonObj = FileToJ(FileFullName);
            if (jsonObj.ContainsKey(PropertyName))
            {
                JObject jo = jsonObj[PropertyName] as JObject;
                if (jo.ContainsKey(CPropertyName))
                {
                    return jo[CPropertyName].ToString();
                }
            }
            return null;

        }

        private static string JReadValue(string FileFullName, string PropertyName, string CPropertyName, string CCPropertyName)
        {
            JObject jsonObj = FileToJ(FileFullName);
            if (jsonObj.ContainsKey(PropertyName))
            {
                JObject jo = jsonObj[PropertyName] as JObject;
                if (jo.ContainsKey(CPropertyName))
                {
                    JObject jo_C = jo[CPropertyName] as JObject;
                    if (jo_C.ContainsKey(CCPropertyName))
                    {
                        return jo_C[CCPropertyName].ToString();
                    }
                }
            }
            return null;

        }

        private static string JReadValue(string FileFullName, string PropertyName, string CPropertyName, string CCPropertyName, string CCCPropertyName)
        {
            JObject jsonObj = FileToJ(FileFullName);
            if (jsonObj.ContainsKey(PropertyName))
            {
                JObject jo = jsonObj[PropertyName] as JObject;
                if (jo.ContainsKey(CPropertyName))
                {
                    JObject jo_C = jo[CPropertyName] as JObject;
                    if (jo_C.ContainsKey(CCPropertyName))
                    {
                        JObject jo_CC = jo_C[CCPropertyName] as JObject;
                        if (jo_CC.ContainsKey(CCCPropertyName)) return jo_CC[CCCPropertyName].ToString();
                    }
                }
            }
            return null;

        }

        public static string JReadValue(string FileFullName, string[] PropertyNames)
        {
            JObject jsonObj = FileToJ(FileFullName);
            switch (PropertyNames.Length)
            {
                case 1: return JReadValue(FileFullName, PropertyNames[0]);
                case 2: return JReadValue(FileFullName, PropertyNames[0], PropertyNames[1]);
                case 3: return JReadValue(FileFullName, PropertyNames[0], PropertyNames[1], PropertyNames[2]);
                case 4: return JReadValue(FileFullName, PropertyNames[0], PropertyNames[1], PropertyNames[2], PropertyNames[3]);
                default: return null;
            }
        }

        public static void JWriteValue(string FileFullName, string[] PropertyNames, string Value)
        {
            JObject jsonObj = FileToJ(FileFullName);
            switch (PropertyNames.Length)
            {
                case 1: JWriteValue(FileFullName, PropertyNames[0], Value); break;
                case 2: JWriteValue(FileFullName, PropertyNames[0], PropertyNames[1], Value); break;
                case 3: JWriteValue(FileFullName, PropertyNames[0], PropertyNames[1], PropertyNames[2], Value); break;
                case 4: JWriteValue(FileFullName, PropertyNames[0], PropertyNames[1], PropertyNames[2], PropertyNames[3], Value); break;
            }
        }

        private static void JWriteValue(string FileFullName, string PropertyName, string CPropertyName, string CCPropertyName, string CCCPropertyName, string Value)
        {
            JObject jsonObj = FileToJ(FileFullName);
            if (!jsonObj.ContainsKey(PropertyName))
            {
                JObject jo_CCC = new JObject(new JProperty(CCCPropertyName, Value));
                JObject jo_CC = new JObject(new JProperty(CCPropertyName, jo_CCC));
                JObject jo_C = new JObject(new JProperty(CPropertyName, jo_CC));
                jsonObj.Add(new JProperty(PropertyName, jo_C));
            }
            else
            {
                JObject jo = jsonObj[PropertyName] as JObject;
                if (!jo.ContainsKey(CPropertyName))
                {
                    JObject jo_CCC = new JObject(new JProperty(CCCPropertyName, Value));
                    JObject jo_CC = new JObject(new JProperty(CCPropertyName, jo_CCC));
                    jo.Add(new JProperty(CPropertyName, jo_CC));
                }
                else
                {
                    JObject jo_C = jo[CPropertyName] as JObject;
                    if (!jo_C.ContainsKey(CCPropertyName))
                    {
                        JObject jo_CCC = new JObject(new JProperty(CCCPropertyName, Value));
                        jo_C.Add(new JProperty(CCPropertyName, jo_CCC));
                    }
                    else
                    {
                        JObject jo_CC = jo_C[CCPropertyName] as JObject;
                        if (!jo_CC.ContainsKey(CCCPropertyName))
                        {
                            jo_CC.Add(CCCPropertyName, Value);
                        }
                        else
                        {
                            jo_CC[CCCPropertyName] = Value;
                        }
                    }
                }
            }
            JToFile(jsonObj, FileFullName);

        }

        private static void JWriteValue(string FileFullName, string PropertyName, string CPropertyName, string CCPropertyName, string Value)
        {
            JObject jsonObj = FileToJ(FileFullName);
            if (!jsonObj.ContainsKey(PropertyName))
            {
                JObject jo_CC = new JObject(new JProperty(CCPropertyName, Value));
                JObject jo_C = new JObject(new JProperty(CPropertyName, jo_CC));
                jsonObj.Add(new JProperty(PropertyName, jo_C));
            }
            else
            {
                JObject jo = jsonObj[PropertyName] as JObject;
                if (!jo.ContainsKey(CPropertyName))
                {
                    JObject jo_CC = new JObject(new JProperty(CCPropertyName, Value));
                    jo.Add(new JProperty(CPropertyName, jo_CC));
                }
                else
                {
                    JObject jo_C = jo[CPropertyName] as JObject;
                    if (!jo_C.ContainsKey(CCPropertyName))
                    {
                        JObject jo_CC = new JObject(new JProperty(CCPropertyName, Value));
                        jo_C.Add(new JProperty(CCPropertyName, jo_CC));
                    }
                    else
                    {
                        jo_C[CCPropertyName] = Value;
                    }
                }
            }
            JToFile(jsonObj, FileFullName);

        }

        private static void JWriteValue(string FileFullName, string PropertyName, string CPropertyName, string Value)
        {
            JObject jsonObj = FileToJ(FileFullName);
            if (!jsonObj.ContainsKey(PropertyName))
            {
                JObject jo_C = new JObject(new JProperty(CPropertyName, Value));
                jsonObj.Add(new JProperty(PropertyName, jo_C));
            }
            else
            {
                JObject jo = jsonObj[PropertyName] as JObject;
                if (!jo.ContainsKey(CPropertyName))
                {
                    jo.Add(new JProperty(CPropertyName, Value));
                }
                else
                {
                    jo[CPropertyName] = Value;
                }
            }
            JToFile(jsonObj, FileFullName);
        }

        private static void JWriteValue(string FileFullName, string PropertyName, string Value)
        {
            JObject jsonObj = FileToJ(FileFullName);
            if (!jsonObj.ContainsKey(PropertyName))
            {
                jsonObj.Add(new JProperty(PropertyName, Value));
            }
            else
            {
                jsonObj[PropertyName] = Value;
            }
            JToFile(jsonObj, FileFullName);
        }

        public static void JToFile(JObject jo, string FileFullName)
        {
            string output = JsonConvert.SerializeObject(jo, Formatting.Indented);
            File.WriteAllText(FileFullName, output);
        }

        public static JObject FileToJ(string FileFullName)
        {
            string json = File.ReadAllText(FileFullName);
            return JObject.Parse(json);
        }

        /// <summary>
        /// []'这3种干扰字符如果出现在JObejec属性中，将会影响函数的返回值
        /// </summary>
        /// <param name="jo"></param>
        /// <param name="PropertyNamesLst"></param>
        /// <returns></returns>
        public static List<string[]> JGetNodeNamesLst(JObject jo)
        {
            string point_Replacer = "-->_point_<--";
            List<string[]> NodeNamesLst = new List<string[]>();
            string joStr = jo.ToString().Replace(".", point_Replacer); //替换干扰字符
            jo = JObject.Parse(joStr);
            var query = jo.Descendants();
            foreach (var v in query)
            {
                if(v.Count()==1) //为1的时候方便抓值，不为1的时候，也有值，但是都重复了
                {
                    string PathStr = v.Path.Replace("'", "").Replace("]", "").Replace("[", ".").TrimStart('.'); 
                    string[] PropertyNames = PathStr.Split('.');
                    for(int i=0;i< PropertyNames.Length;i++)
                    {
                        PropertyNames[i] = PropertyNames[i].Replace(point_Replacer, ".");       
                    }
                    if(!StrsLstContains(NodeNamesLst, PropertyNames)) NodeNamesLst.Add(PropertyNames);
                }               
            }
            return NodeNamesLst;

        }

        private static bool StrsEquals(string[] Strs1, string[] Strs2)
        {
            if(Strs1.Length == Strs2.Length)
            {
                for(int i=0;i< Strs1.Length;i++)
                {
                    if (Strs1[i] != Strs2[i]) return false;
                }
                return true;
            }else
            {
                return false;
            }
            
        }

        private static bool StrsLstContains(List<string[]> StrsLst, string[] Strs)
        {
            foreach (string[] Ss in StrsLst)
            {
                if (StrsEquals(Ss, Strs)) return true;
            }
            return false;
        }

    }
}
