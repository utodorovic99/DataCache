///////////////////////////////////////////////////////////
//  CacheHit.cs
//  Implementation of the Class CacheHit
//  Generated by Enterprise Architect
//  Created on:      28-Apr-2021 10:30:22 AM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common_Project.Classes;

namespace CacheControler_Project.Classes
{
    public class CacheHit
    {

        private List<ConsumptionRecord> cRecord;
        private DateTime hitTime;

        public CacheHit(List<ConsumptionRecord> cRecord)
        {
            hitTime = DateTime.Now;
            this.cRecord = cRecord;
        }

        ~CacheHit()
        {

        }

        public List<ConsumptionRecord> CRecord
        {
            get { return cRecord;  }
            set { cRecord = value; }
        }

        public DateTime HitTime
        {
            get { return hitTime;  }
        }

    }//end CacheHit
}