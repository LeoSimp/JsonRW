using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonRW
{
    class Program
    {
        static void Main(string[] args)
        {

            string FileFullName = "testlog.json";
            string[] PropertyNames = new string[4] { "General information", "Serial number" , "Production date" , "Year" };
            string Value = "2025";
            /*
            JObject jo_CCC = new JObject(new JProperty(PropertyNames[PropertyNames.Length-1], Value));
            JObject jo_CC = new JObject(new JProperty(PropertyNames[PropertyNames.Length - 2], jo_CCC));
            JObject jo_C = new JObject(new JProperty(PropertyNames[PropertyNames.Length - 3], jo_CC));
            JObject jo = new JObject( new JProperty(PropertyNames[PropertyNames.Length - 4], jo_C));
            JRW_Helper.JToFile(jo, FileFullName); //只能完全覆盖，不能部分修改（相当于完全新建）
            */            
            List<string[]> NodeNamesLst = JRW_Helper.JGetNodeNamesLst(JRW_Helper.FileToJ(FileFullName));
            int n = NodeNamesLst.Max(x => x.Length);
            Console.WriteLine("n:" + n);//当n>PropertyNames.Length,则有可能将要修改的数据正好被缩短，可以加如下再判断
            bool EventOcurr = false;
            if (n> PropertyNames.Length)
            {
                foreach(string[] Ss in NodeNamesLst)
                {
                    if(Ss.Length > PropertyNames.Length)
                    {
                        for(int i=0;i<Ss.Length;i++)
                        {
                            if (Ss[i] != PropertyNames[i]) continue;
                            EventOcurr = true;break;
                        }
                    }
                    if (EventOcurr) break;
                }
            }
            if (EventOcurr) Console.WriteLine("The Event of Note Number be shorted Ocurred");
            JRW_Helper.JWriteValue(FileFullName, PropertyNames, Value); //部分修改，不存在则部分新建,有可能节点数被缩短
            string value1 = JRW_Helper.JReadValue(FileFullName, PropertyNames);
            Console.WriteLine("value1:" + value1);
            Console.ReadKey();
        }

    }
}
