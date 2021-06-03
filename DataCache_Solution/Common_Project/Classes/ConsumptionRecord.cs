﻿///////////////////////////////////////////////////////////
//  ConsumptionRecord.cs
//  Implementation of the Class ConsumptionRecord
//  Generated by Enterprise Architect
//  Created on:      28-Apr-2021 10:30:22 AM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Common_Project.Classes
{
    [Serializable]
    public class ConsumptionRecord
    {

        private string gID;
        private int mWh;
        private string timeStamp;

        public ConsumptionRecord()
        {
            gID = "";
            mWh = -1;
            timeStamp = "";
        }

        public ConsumptionRecord(string gID, int mWh, string timeStamp)
        {
            this.gID = gID;
            this.mWh = mWh;
            this.timeStamp= timeStamp;
        }

        ~ConsumptionRecord()
        {

        }

        public bool Equals(ConsumptionRecord mirror)
        {
            return  (this.gID==mirror.gID) && 
                    (this.mWh==mirror.mWh) && 
                    (this.timeStamp==mirror.timeStamp);
        }
        public string GID
        {
            get { return gID;   }
            set { gID = value;  }
        }

        public int MWh
        {
            get { return mWh;  }
            set { mWh = value; }
        }

        public string TimeStamp
        {
            get { return timeStamp;  }
            set { timeStamp = value; }
        }

        public override string ToString()
        {
            return String.Format("Consumption record GID: {0},\tHOUR: {1}, \tLOAD: {2}", gID, timeStamp, mWh);
        }

        public bool IsWholeEmpty()
        {
            return mWh == -1 && gID == "" && timeStamp == "";
        }

        public bool HasUsableStatus()
        {
            return timeStamp != "" && gID != "";
        }

        public int GetHour()
        {
            char[] criteria = {'-'};
            List<string> parts = timeStamp.Split(criteria , StringSplitOptions.RemoveEmptyEntries).ToList();
            if (parts.Count < 4) return -1;
            int retVal=0;

            if (Int32.TryParse(parts[3], out retVal)) return retVal;
            else return -1;
        }

        public bool CheckTimeRelationMine(string timePoint, string relation, bool ignoreHours)
        {
            List<string> olderParts;
            List<string> newerParts;
            char[] criteria = { '-' };

            if (relation == ">=" || relation == ">")                           //Am I ">=" from timePoint
            {
                olderParts = timePoint.Split(criteria, StringSplitOptions.RemoveEmptyEntries).ToList();
                newerParts = TimeStamp.Split(criteria, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else if (relation == "<=" || relation == "<")
            {
                olderParts = TimeStamp.Split(criteria, StringSplitOptions.RemoveEmptyEntries).ToList();
                newerParts = timePoint.Split(criteria, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else if (relation == "=")
            {
                olderParts = timePoint.Split(criteria, StringSplitOptions.RemoveEmptyEntries).ToList();
                newerParts = timeStamp.Split(criteria, StringSplitOptions.RemoveEmptyEntries).ToList();

                bool timeRel =  (olderParts[0] == newerParts[0]) && 
                                (olderParts[1] == newerParts[1]) && 
                                (olderParts[2] == newerParts[2]);

                if (ignoreHours)    return timeRel;
                else                return timeRel && (olderParts[3] == newerParts[3]);
            }
            else throw new ArithmeticException();


            int tmpOlder, tmpNewer;                                                                             //Correct format is granted by DB Management
            if ((tmpOlder = Int32.Parse(olderParts[0])) == (tmpNewer = Int32.Parse(newerParts[0])))             //Same year
            {
                if ((tmpOlder = Int32.Parse(olderParts[1])) == (tmpNewer = Int32.Parse(newerParts[1])))         //Same same month
                {
                    if ((tmpOlder =Int32.Parse(olderParts[2])) <= (tmpNewer =Int32.Parse(newerParts[2])))       //Day before or eq 
                    {

                        if(ignoreHours)                                                                         //Ignore hours, final check on days
                        {
                            if ((relation == "<" || relation == ">") && tmpOlder < tmpNewer)   return true;
                            if ((relation == "<=" || relation == ">=") && tmpOlder <= tmpNewer) return true;
                            return false;
                        }

                        // Not ignoring hours & when hour check is needed
                        if (tmpOlder == tmpNewer && (tmpOlder=Int32.Parse(olderParts[3])) <= (tmpNewer =Int32.Parse(newerParts[3])))           
                        {
                            if ((relation == "<" || relation == ">") && tmpOlder < tmpNewer) 
                                return true;

                            if ((relation == "<=" || relation == ">=") && tmpOlder <= tmpNewer) 
                                return true;
                        }

                        return false;
                    }
                    else
                        return false;
                }
                else if (tmpOlder < tmpNewer)
                    return true;
                else
                    return false;
            }
            else if (tmpOlder < tmpNewer)
                return true;
            else return false;
        }

    }//end ConsumptionRecord
}