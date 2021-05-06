﻿///////////////////////////////////////////////////////////
//  ConnectionControler.cs
//  Implementation of the Class ConnectionControler
//  Generated by Enterprise Architect
//  Created on:      28-Apr-2021 10:30:22 AM
//  Original author: Ugljesa
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Common_Project.Classes;
using Common_Project.DistributedServices;
using CacheControler_Project.Enums;

namespace ConnectionControler_Project.Classes
{
    public class ConnectionControler : IDBReq
    {

        public ConnectionControler()
        {

        }

        ~ConnectionControler()
        {

        }

        public ConsumptionUpdate OstvConsumptionDBWrite(List<ConsumptionRecord> cRecords)
        {
            throw new NotImplementedException();
        }

        public List<ConsumptionRecord> ConsumptionReqPropagate(DSpanGeoReq dSpanGeoReq)
        {
            throw new NotImplementedException();
        }

        public List<AuditRecord> ReadAuditContnet()
        {
            throw new NotImplementedException();
        }

        public EUpdateGeoStatus GeoEntityUpdate(string oldID, string newID)
        {
            throw new NotImplementedException();
        }

        public bool GeoEntityWrite(GeoRecord gRecord)
        {
            throw new NotImplementedException();
        }

        public List<GeoRecord> ReadGeoContent()
        {
            throw new NotImplementedException();
        }
    }//end ConnectionControler
}