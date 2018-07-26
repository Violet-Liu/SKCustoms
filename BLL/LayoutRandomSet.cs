using Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class LayoutRandomSet
    {
        public Boolean IsOpen { get; set; }

        public float RPercent { get; set; } = 1;

        public int Pass { get; set; }

        public int ValidCount { get; set; } = 1;

        public int ValidDays { get; set; } = 1;

        public string Channel { get; set; } = string.Empty;
    }

    public class LayoutRandomSetLock
    {
        /// <summary>
        /// ConcurrentDictionary 适合并行，但是考虑到这里的场景，读写频率不高，一次写入多项，还是用原始的读写锁加dictionary比较实际
        /// </summary>
        public static Dictionary<string, LayoutRandomSet> layoutRs { get; set; } = new Dictionary<string, LayoutRandomSet>();  



        public static string filePath = AppDomain.CurrentDomain.BaseDirectory + "layoutRS.txt";

        private static ReaderWriterLockSlim LogWriteLock = new ReaderWriterLockSlim();
        public static void Get()
        {
            LogWriteLock.EnterReadLock();

            try
            {
                if (File.Exists(filePath))
                {
                    var contents = File.ReadAllLines(filePath);

                    foreach (var item in contents)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var arr = item.Split('|');
                            if (arr.Length == 6)
                            {

                                if (layoutRs.ContainsKey(arr[5].ToString()))
                                    layoutRs.Remove(arr[5].ToString());
                                var set = new LayoutRandomSet();
                                set.IsOpen = arr[0].ToInt() == 0 ? false : true;
                                set.Pass = arr[1].ToInt();
                                if (float.TryParse(arr[2].ToString(), out float result))
                                {
                                    set.RPercent = result;
                                }

                                set.ValidCount = arr[3].ToInt();
                                set.ValidDays = arr[4].ToInt();
                                set.Channel = arr[5].ToString();
                                layoutRs.Add(arr[5].ToString(), set);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                LogWriteLock.ExitReadLock();

            }



        }

        public static void Set()
        {
            try
            {
                LogWriteLock.EnterWriteLock();
                if (File.Exists(filePath))
                    File.Delete(filePath);

                var contents = new List<string>();
                foreach(var value in LayoutRandomSetLock.layoutRs.Values)
                {
                    var open = value.IsOpen ? "1" : "0";
                     contents.Add($"{open}|{value.Pass}|{value.RPercent}|{value.ValidCount}|{value.ValidDays}|{value.Channel}");
                }

                File.WriteAllLines(filePath, contents);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                LogWriteLock.ExitWriteLock();
            }
        }
    }
}
