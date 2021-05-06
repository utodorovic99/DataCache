﻿///////////////////////////////////////////////////////////
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
        private int hitRate;
        private DateTime hitTime;

        public CacheHit()
        {

        }

        ~CacheHit()
        {

        }

        public List<ConsumptionRecord> CRecord
        {
            get
            {
                return cRecord;
            }
            set
            {
                cRecord = value;
            }
        }

        public int HitRate
        {
            get
            {
                return hitRate;
            }
            set
            {
                hitRate = value;
            }
        }

        public DateTime HitTime
        {
            get
            {
                return hitTime;
            }
            set
            {
                hitTime = value;
            }
        }

    }//end CacheHit
}