using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions.Dictionaries;
using Extensions.LinkedList;
using System.IO;

namespace GeneticTsp
{
    class Program
    {
        static LinkedList<String> locs = new LinkedList<String>();
        //    static LinkedList<String> path = new LinkedList<>();
        //read lines from the file into a list of string
        static LinkedList<String> list = new LinkedList<string>();
        //creating a hashmap to hold all info on the locations
        static SortedDictionary<int, LinkedList<Double>> main_locations = new SortedDictionary<int, LinkedList<double>>();
        static SortedDictionary<int, LinkedList<Double>> init_main_locations = new SortedDictionary<int, LinkedList<double>>();
        //creating a hashmap to hold the final locations rearranged to reduce travel
        static SortedDictionary<int, List<Double>> distance = new SortedDictionary<int, List<double>>();
        static SortedDictionary<int, List<String>> random_locations_path = new SortedDictionary<int, List<string>>();
        static LinkedList<Double> random_locations_distance = new LinkedList<Double>();
        //holds the current generated paths 
        static LinkedList<String> path = new LinkedList<String>();
        //static List<Double> random_locations_distance = new ArrayList<>();
        static int start_location = 1;
        static Double start_location_x = 0.0;
        static Double start_location_y = 0.0;
        static Random sr = new Random();
        static int genertions = 1000000, generations_count = 0;
        static Boolean GASwap = false;
        static double best_generation_distance = 0.0;
        static String best_generation = "", best_genertion_path;
        private static LinkedList<string> unique = new LinkedList<string>();
        static int _10P = 0;
        static int g_c = 0;

        static void Main(string[] args)
        {
            //files are loc1,test1tsp,test2tsp,test3tsp,test2tsp new,test3tsp new
            foreach (string s in File.ReadLines(@"Assets/test3tsp.txt", Encoding.UTF8))
            {
                //System.out.println(s.length());
                //split the string
                int city_id = 0;
                LinkedList<Double> temp_list = new LinkedList<double>();
                String[] temp = s.Split(' ');
                //System.out.println(temp.length);
                int temp_counter = 0;
                for (int i = 0; i < temp.Length; i++)
                {
                    //System.out.println(temp[i]);
                    if (!temp[i].Equals(""))
                    {
                        switch (temp_counter)
                        {
                            case 0:
                                city_id = Convert.ToInt32(temp[i]);
                                break;
                            case 1:
                                temp_list.add(Convert.ToDouble(temp[i]));
                                break;
                            case 2:
                                temp_list.add(Convert.ToDouble(temp[i]));
                                break;
                            default:
                                break;
                        }
                        temp_counter++;
                    }
                }
                //check to avoid repeating the same location
                if (main_locations.Count > 0 && !main_locations.ContainsValue(temp_list))
                {
                    main_locations.put(city_id, new LinkedList<Double>());
                    main_locations.get(city_id).add(temp_list.get(0));
                    main_locations.get(city_id).add(temp_list.get(1));
                }
                else if (main_locations.Count == 0)
                {
                    main_locations.put(city_id, new LinkedList<double>());
                    main_locations.get(city_id).add(temp_list.get(0));
                    main_locations.get(city_id).add(temp_list.get(1));
                }
            }
            init_main_locations = new SortedDictionary<int, LinkedList<double>>(main_locations);
            //start location is city 1
            start_location = 1;
            _10P = (int)(0.1 * genertions);
            Random(10, 2);
        }
        
        private static void Random(int run_times, int parent_no)
        {
            //hold city numbers
            LinkedList<int> cities_no = new LinkedList<int>();
            LinkedList<int> init_cities_no = new LinkedList<int>();

            Double temp_distance = 0.0, total_distance = 0.0;
            //Map.Entry<Integer, LinkedList<Double>> entry : main_locations.entrySet()
            
            foreach (int key in main_locations.Keys)
            {
                init_cities_no.add(key);
            }
            cities_no.addAll(init_cities_no);
            int recheck_counter = 0, success_counter = 0;
            //clear path on each run
            path.Clear();
            for (int x = 0; x < parent_no; x++)
            {
                Console.WriteLine("=====================");
                String t_path = "";
                start_location = 1;
                temp_distance = 0.0;
                if (recheck_counter == run_times || success_counter == parent_no)
                {
                    break;
                }

                if (cities_no.Count == 0)
                {
                    cities_no.addAll(init_cities_no);
                }

                for (;;)
                {
                    if (cities_no.Count == 1)
                    {
                        temp_distance += nn(start_location, 1);
                        Console.WriteLine("Travelling from city " + start_location + " to city " + 1);
                        t_path += start_location;
                        if (temp_distance < total_distance || total_distance == 0.0)
                        {
                            total_distance = temp_distance;
                            best_generation_distance = total_distance;
                            best_generation = "Parent";
                            best_genertion_path = t_path;
                            path.add(t_path);
                            success_counter += 1;

                        }
                        else
                        {
                            recheck_counter += 1;
                        }
                        Console.WriteLine("Total Distance is " + temp_distance);
                        cities_no.Clear();
                        break;
                    }

                    int random_city = sr.Next(cities_no.Count);
                    //make sure random city is not the current city
                    if (cities_no.get(random_city) == start_location)
                    {
                        for (;;)
                        {
                            random_city = sr.Next(cities_no.Count);
                            if (cities_no.get(random_city) != start_location)
                            {
                                break;
                            }
                        }
                    }
                    temp_distance += nn(start_location, cities_no.get(random_city));
                    t_path += start_location + ",";
                    Console.WriteLine("Travelling from city " + start_location + " to city " + cities_no.get(random_city));
                    int prev_start_loc = start_location;
                    start_location = cities_no.get(random_city);
                    cities_no.remove(cities_no.indexOf(prev_start_loc));

                }

            }
            foreach (String s in path)
            {
                Console.WriteLine("Parent : " + s);
            }
            if (path.Count != parent_no)
            {
                Random(run_times, parent_no);
            }
            else
            {
                for (int i = 0; i <= genertions; i++)
                {
                    //GA(path,"both");
                    GA(path, "single point");
                }

                Console.WriteLine("================================");
                Console.WriteLine("Shortest Distance " + best_generation_distance + " Gotten in " + best_generation + " With a City Travel Path of " + best_genertion_path);
                Console.WriteLine("================================");
            }
        }

        private static Double nn(int start_location, int key)
        {
            //calculate travel distance
            double distance_calc = Math.Sqrt(Math.Pow((main_locations.get(start_location).get(0) - (main_locations.get(key).get(0))), 2) + Math.Pow((main_locations.get(start_location).get(1) - (main_locations.get(key).get(1))), 2));
            return distance_calc;
        }

        private static void GA(LinkedList<String> parents, String type)
        {
            //        if(generations_count == genertions)
            //        {
            //            Console.WriteLine("================================");
            //            Console.WriteLine("Shortest Distance "+best_generation_distance+" Gotten in "+best_generation+" With a City Travel Path of "+best_genertion_path);
            //            Console.WriteLine("================================");
            //            System.exit(0);
            //        }
            //        Console.WriteLine("======Children Generation "+ generations_count +" ======");

            if (g_c == _10P)
            {
                Console.WriteLine("Generation Count " + generations_count);
                g_c = 0;
            }
            g_c += 1;

            String[] parent1 = parents.get(0).Split(',');

            String[] parent2 = parents.get(1).Split(',');
            LinkedList<String> children = new LinkedList<string>();
            switch (type.ToLower())
            {
                case "single point":
                    children.addAll(single_point(parent1, parent2));
                    break;
                case "two point":
                    children.addAll(two_point(parent1, parent2));
                    break;
                case "both":
                    if (GASwap)
                    {
                        children.addAll(single_point(parent1, parent2));
                        GASwap = false;
                    }
                    else
                    {
                        children.addAll(two_point(parent1, parent2));
                        GASwap = true;
                    }
                    break;
            }

            foreach (String s in children)
            {
                String[] splitted_child = s.Split(',');
                double temp_distance = 0.0, total_distance = 0.0;

                for (int i = 0; i < splitted_child.Length; i++)
                {

                    if (i == (splitted_child.Length - 1))
                    {
                        temp_distance += nn(Convert.ToInt32(splitted_child[i]), Convert.ToInt32(splitted_child[0]));
                        total_distance = temp_distance;
                        //                    Console.WriteLine("Travelling from city "+ splitted_child[i]+" To city "+splitted_child[0]);
                        //                    Console.WriteLine("Total Distance is "+total_distance);
                        if (total_distance <= best_generation_distance || best_generation_distance == 0)
                        {
                            best_generation_distance = total_distance;
                            best_generation = "Generation " + generations_count;
                            best_genertion_path = s;
                            Console.WriteLine("Shortest Distance So far is " + best_generation_distance + " Gotten in Child " + children.indexOf(s) + " of " + best_generation + " with a City Travel Path of " + best_genertion_path);

                        }
                    }
                    else
                    {
                        temp_distance += nn(Convert.ToInt32(splitted_child[i]), Convert.ToInt32(splitted_child[i + 1]));
                        //                    Console.WriteLine("Travelling from city "+ splitted_child[i]+" To city "+splitted_child[i+1]);
                    }
                }
            }
            generations_count += 1;
            path.Clear();
            path.addAll(children);
            //        GA(children, type);
        }

        private static LinkedList<String> single_point(String[] parent1, String[] parent2)
        {
            LinkedList<String> children_path = new LinkedList<String>();

            //get center of the parent for single point gene crossing
            int center = parent1.Length / 2;

            //create child from parents
            //child 1
            //for crossover method 4th parameter can be blank as the method only checks for two point
            String child1 = crossover(center, parent1, parent2, "");
            //Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            //Console.WriteLine(child1.Length+" "+child1);
            //Console.WriteLine("Child " + child1.Substring(0, child1.Length - 1));
            //substring to remove extra ','
            children_path.add(mutation(child1.Substring(0, child1.Length - 1), parent1.Length));
            //Console.WriteLine("++++++++++++++++++++++++++++++++++++");
            //Console.WriteLine(child1.Substring(0, child1.Length - 1).Length + " "+child1.Substring(0, child1.Length - 1));


            //child 2
            String child2 = crossover(center, parent2, parent1, "");

            //Console.WriteLine("Child " + child2.Substring(0, child2.Length - 1));
            children_path.add(mutation(child2.Substring(0, child2.Length - 1), parent2.Length));

            return children_path;
        }

        private static LinkedList<String> two_point(String[] parent1, String[] parent2)
        {
            LinkedList<String> children_path = new LinkedList<String>();

            //get center of the parent for single point gene crossing
            int center = parent1.Length / 3;

            //create child from parents
            //child 1
            //for crossover method 4th parameter can be blank as the method only checks for two point
            String child1 = crossover(center, parent1, parent2, "two point");
            //        Console.WriteLine("Child "+child1.substring(0, child1.length()-1));
            children_path.add(mutation(child1.Substring(0, child1.Length - 1), parent1.Length));

            //child 2
            String child2 = crossover(center, parent2, parent1, "two point");
            //        Console.WriteLine("Child "+child2.substring(0, child2.length()-1));
            children_path.add(mutation(child2.Substring(0, child2.Length - 1), parent2.Length));

            return children_path;
        }

        private static String mutation(String child_path, int parent_length)
        {
            String result = "";
            int fail_counter = 0;

            for (;;)
            {


                //create a list to hold city_no to be used to prevent travelling to the same city twice
                LinkedList<int> cities = new LinkedList<int>();
                LinkedList<int> init_cities = new LinkedList<int>();
                LinkedList<int> cities_index_to_be_mutated = new LinkedList<int>();
                int cities_no_to_be_mutated_count = 0;
                for (int i = 1; i <= parent_length; i++)
                {
                    init_cities.add(i);
                }
                cities.addAll(init_cities);

                //converted mutation to method, so adjustments had to be made
                LinkedList<int> children = new LinkedList<int>();
                foreach (String s in child_path.Split(','))
                {
                    children.add(Convert.ToInt32(s));
                }
                //mutation phase
                //phase 1: get duplicates
                for (int i = 0; i < children.Count; i++)
                {

                    if (cities.Contains(children.get(i)))
                    {

                        cities.remove(cities.indexOf(children.get(i)));
                    }
                    else
                    {
                        cities_index_to_be_mutated.add(i);
                        //Console.WriteLine("Duplicate City " + children.get(i) + " At Index " + i);
                        cities_no_to_be_mutated_count += 1;
                    }
                }
                //Console.WriteLine("Cities size is " + cities.Count);

                //phase 2: replace duplicates
                for (int i = 0; i < cities_no_to_be_mutated_count; i++)
                {
                    int replace_city = sr.Next(cities.Count);

                    children.remove(cities_index_to_be_mutated.get(i));
                    children.add(cities_index_to_be_mutated.get(i), cities.get(replace_city));
                    cities.remove(cities.indexOf(cities.get(replace_city)));

                }

                //phase 3: Swapping
                //swap 4 indexes in the children randomly in 2s
                LinkedList<int> swapped_indexes = new LinkedList<int>();
                for (int i = 0; i < 2; i++)
                {
                    int swap_index_1 = sr.Next(parent_length - 2 + 1) + 1;
                    int swap_index_2 = sr.Next(parent_length - 4 + 1) + 1;
                    //keep on generating indexes until 2 different indexes that arent already swapped are created.
                    for (;;)
                    {
                        if ((swap_index_1 != swap_index_2 && (!swapped_indexes.Contains(swap_index_1) || !swapped_indexes.Contains(swap_index_2))))
                        {
                            break;
                        }
                        swap_index_1 = sr.Next(parent_length - 2 + 1) + 1;
                        swap_index_2 = sr.Next(parent_length - 4 + 1) + 1;
                    }
                    int prev_value_index1 = children.get(swap_index_1);
                    int prev_value_index2 = children.get(swap_index_2);

                    children.remove(swap_index_1);
                    children.add(swap_index_1, prev_value_index2);
                    //second random index
                    children.remove(swap_index_2);
                    children.add(swap_index_2, prev_value_index1);

                    swapped_indexes.add(swap_index_1);
                    swapped_indexes.add(swap_index_2);
                }

                result = "";
                //Console.WriteLine("Corrected Child :");
                foreach (int i in children)
                {
                    result += i + ",";
                    //Console.Write(i+" ");
                }
                //Console.WriteLine("");

                result = result.Substring(0, result.Length - 1);
                fail_counter += 1;

                if (!unique.Contains(result) || fail_counter == _10P)
                {
                    if (fail_counter != _10P)
                    {
                        unique.add(result);
                    }

                    break;
                }
                //Console.WriteLine(result.Substring(0, result.Length - 1));
                //Environment.Exit(0);
            }
            return result;
        }

        private static String crossover(int center, String[] parent1, String[] parent2, String type)
        {
            String child = "";
            //Console.WriteLine("P1 "+parent1.Length+" p2 "+parent2.Length+" center "+center);
            for (int i = 0; i < center; i++)
            {
                child += parent1[i] + ",";
            }
            //create genes for two point cross over
            
            if (type.Equals("two point"))
            {
                for (int i = center; i < (center * 2); i++)
                {
                    child += parent2[i] + ",";
                }

                for (int i = (center * 2); i < parent1.Length; i++)
                {
                    child += parent1[i] + ",";
                }
            }
            else
            {
                //genes for single point
                for (int i = center; i < parent1.Length; i++)
                {
                    child += parent2[i] + ",";
                }
            }
            return child;
        }
    }
}
