using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using recommendWeb.Models;
using System.Collections;

namespace recommendWeb.DataProcressers
{
    public class GroupDataProcesser
    {
        public static ArrayList GroupProcess(ArrayList groupList)
        {
            ArrayList processList = new ArrayList();

            for(int i = 0; i < groupList.Count; i++)
            {
                processList.Add(groupList[i]);
            }

            sort(processList, 0, processList.Count - 1);

            return processList;
        }

        private static int sortUnit(ArrayList groupList,int low,int high)
        {
            object key = groupList[low];
            while (low < high)
            {
                
                while((groupList[high] as Group).GroupLength<=(key as Group).GroupLength && high > low)
                {
                    --high;
                }
                groupList[low] = groupList[high];

                while((groupList[low]as Group).GroupLength>(key as Group).GroupLength&& high > low)
                {
                    ++low;
                }

                groupList[high] = groupList[low];
            }
            groupList[low] = key;

            return high;
        }
        
        private static void sort(ArrayList groupList,int low,int high)
        {
            if (low >= high)
            {
                return;
            }
            int index = sortUnit(groupList, low, high);

            sort(groupList, low, index - 1);
            sort(groupList, index + 1, high);
        }
        
        
    }
}