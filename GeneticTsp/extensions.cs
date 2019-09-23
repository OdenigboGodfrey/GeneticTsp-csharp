using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    namespace LinkedList
    {
        public static class List
        {
            public static Double get(this LinkedList<double> list, int index)
            {
                return list.ElementAt(index);
            }
            public static int get(this LinkedList<int> list, int index)
            {
                return list.ElementAt(index);
            }
            public static string get(this LinkedList<string> list, int index)
            {
                return list.ElementAt(index);
            }
            public static object get(this LinkedList<object> list, int index)
            {
                return list.ElementAt(index);
            }

            //Addition
            public static bool add(this LinkedList<object> list, object value)
            {
                list.AddLast(value);
                if (list.Contains(value))
                {
                    return true;
                }

                return false;
            }
            public static bool add(this LinkedList<String> list, String value)
            {
                list.AddLast(value);
                if (list.Contains(value))
                {
                    return true;
                }

                return false;
            }
            public static bool add(this LinkedList<double> list, double value)
            {
                list.AddLast(value);
                if (list.Contains(value))
                {
                    return true;
                }
                return false;
            }
            public static bool add(this LinkedList<int> list, int value)
            {
                list.AddLast(value);
                if (list.Contains(value))
                {
                    return true;
                }
                return false;
            }

            //add based on index
            public static bool add(this LinkedList<int> list, int index, int value)
            {
                LinkedList<int> tbr = new LinkedList<int>();
                bool isAdded = false;
                for (int i = 0; i < list.Count;)
                {
                    if (i == index && !isAdded)
                    {
                        tbr.add(value);
                        isAdded = true;
                    }
                    else
                    {
                        tbr.add(list.get(i));
                        i++;
                    }
                }
                //add new entry if list size is not large enough
                if ((list.Count - 1) < index)
                {
                    tbr.add(value);
                }

                list.Clear();
                list.addAll(tbr);
                if (list.Count > 0)
                {
                    return true;
                }
                return false;
            }

            public static bool add(this LinkedList<string> list, int index, string value)
            {
                LinkedList<String> tbr = new LinkedList<string>();
                bool isAdded = false;
                for (int i = 0; i < list.Count;)
                {
                    if (i == index && !isAdded)
                    {
                        tbr.add(value);
                        isAdded = true;
                    }
                    else
                    {
                        tbr.add(list.get(i));
                        i++;
                    }
                }


                if ((list.Count - 1) < index)
                {
                    tbr.add(value);
                }

                list.Clear();
                list.addAll(tbr);
                if (list.Count > 0)
                {
                    return true;
                }
                return false;
            }

            public static bool add(this LinkedList<double> list, int index, double value)
            {
                LinkedList<double> tbr = new LinkedList<double>();
                bool isAdded = false;
                for (int i = 0; i < list.Count;)
                {
                    if (i == index && !isAdded)
                    {
                        tbr.add(value);
                        isAdded = true;
                    }
                    else
                    {
                        tbr.add(list.get(i));
                        i++;
                    }
                }

                if ((list.Count - 1) < index)
                {
                    tbr.add(value);
                }

                list.Clear();
                list.addAll(tbr);
                if (list.Count > 0)
                {
                    return true;
                }
                return false;
            }

            public static bool add(this LinkedList<object> list, int index, object value)
            {
                LinkedList<object> tbr = new LinkedList<object>();
                bool isAdded = false;
                for (int i = 0; i < list.Count;)
                {
                    if (i == index && !isAdded)
                    {
                        tbr.add(value);
                        isAdded = true;
                    }
                    else
                    {
                        tbr.add(list.get(i));
                        i++;
                    }
                }

                if ((list.Count - 1) < index)
                {
                    tbr.add(value);
                }

                list.Clear();
                list.addAll(tbr);
                if (list.Count > 0)
                {
                    return true;
                }
                return false;
            }



            //AddAll
            public static bool addAll(this LinkedList<int> list, LinkedList<int> value)
            {
                foreach (int i in value)
                {
                    list.add(i);
                }


                if (list.Count == value.Count)
                {
                    return true;
                }

                return false;
            }
            public static bool addAll(this LinkedList<string> list, LinkedList<string> value)
            {
                foreach (string i in value)
                {
                    list.add(i);
                }


                if (list.Count == value.Count)
                {
                    return true;
                }

                return false;
            }

            public static bool addAll(this LinkedList<double> list, LinkedList<double> value)
            {
                foreach (double i in value)
                {
                    list.add(i);
                }


                if (list.Count == value.Count)
                {
                    return true;
                }

                return false;
            }

            public static bool addAll(this LinkedList<object> list, LinkedList<object> value)
            {
                foreach (object i in value)
                {
                    list.add(i);
                }


                if (list.Count == value.Count)
                {
                    return true;
                }

                return false;
            }
            //IndexOf
            public static int indexOf(this LinkedList<int> list, int value)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list.get(i) == value)
                    {
                        return i;
                    }
                }

                return 0;
            }

            public static int indexOf(this LinkedList<string> list, string value)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list.get(i) == value)
                    {
                        return i;
                    }
                }

                return 0;
            }

            public static int indexOf(this LinkedList<double> list, double value)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list.get(i) == value)
                    {
                        return i;
                    }
                }

                return 0;
            }

            public static int indexOf(this LinkedList<object> list, object value)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list.get(i) == value)
                    {
                        return i;
                    }
                }

                return 0;
            }

            //remove at index
            public static bool remove(this LinkedList<int> list, int index)
            {
                LinkedList<int> tbr = new LinkedList<int>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != index)
                    {
                        tbr.add(list.get(i));
                    }
                }

                list.Clear();
                list.addAll(tbr);
                if (list.Count > 0)
                {
                    return true;
                }

                return false;
            }

            public static bool remove(this LinkedList<string> list, int index)
            {
                LinkedList<String> tbr = new LinkedList<string>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != index)
                    {
                        tbr.add(list.get(i));
                    }
                }

                list.Clear();
                list.addAll(tbr);
                if (list.Count > 0)
                {
                    return true;
                }

                return false;
            }

            public static bool remove(this LinkedList<double> list, int index)
            {
                LinkedList<double> tbr = new LinkedList<double>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != index)
                    {
                        tbr.add(list.get(i));
                    }
                }

                list.Clear();
                list.addAll(tbr);
                if (list.Count > 0)
                {
                    return true;
                }

                return false;
            }

            public static bool remove(this LinkedList<object> list, int index)
            {
                LinkedList<object> tbr = new LinkedList<object>();
                for (int i = 0; i < list.Count; i++)
                {
                    if (i != index)
                    {
                        tbr.add(list.get(i));
                    }
                }

                list.Clear();
                list.addAll(tbr);
                if (list.Count > 0)
                {
                    return true;
                }

                return false;
            }


        }
    }

    namespace Dictionaries
    {
        public static class Dictionaries
        {
            public static LinkedList<Double> get(this SortedDictionary<int, LinkedList<double>> dict, int value)
            {
                return dict[value];
            }
            public static LinkedList<string> get(this SortedDictionary<int, LinkedList<string>> dict, int value)
            {
                return dict[value];
            }
            public static LinkedList<int> get(this SortedDictionary<int, LinkedList<int>> dict, int value)
            {
                return dict[value];
            }

            //Addition
            public static bool put(this SortedDictionary<int, LinkedList<int>> dict, int key, LinkedList<int> value)
            {
                dict.Add(key, value);

                if (dict.ContainsKey(key))
                {
                    return true;
                }

                return false;
            }
            public static bool put(this SortedDictionary<int, LinkedList<string>> dict, int key, LinkedList<string> value)
            {
                dict.Add(key, value);

                if (dict.ContainsKey(key))
                {
                    return true;
                }

                return false;
            }
            public static bool put(this SortedDictionary<int, LinkedList<double>> dict, int key, LinkedList<double> value)
            {
                dict.Add(key, value);

                if (dict.ContainsKey(key))
                {
                    return true;
                }

                return false;
            }
            public static bool put(this SortedDictionary<int, LinkedList<object>> dict, int key, LinkedList<object> value)
            {
                dict.Add(key, value);

                if (dict.ContainsKey(key))
                {
                    return true;
                }

                return false;
            }
        }
    }
}
