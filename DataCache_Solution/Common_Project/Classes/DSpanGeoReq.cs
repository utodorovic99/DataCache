﻿///////////////////////////////////////////////////////////
//  DSpanGeoReq.cs
//  Implementation of the Class DSpanGeoReq
//  Generated by Enterprise Architect
//  Created on:      28-Apr-2021 10:30:23 AM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Common_Project.Classes
{
    public class DSpanGeoReq
    {

        private string from;
        private string gName;
        private string till;

        public DSpanGeoReq()
        {

        }

        ~DSpanGeoReq()
        {

        }

        public string From
        {
            get
            {
                return from;
            }
            set
            {
                from = value;
            }
        }

        public string GName
        {
            get
            {
                return gName;
            }
            set
            {
                gName = value;
            }
        }

        public string Till
        {
            get
            {
                return till;
            }
            set
            {
                till = value;
            }
        }

    }//end DSpanGeoReq
}